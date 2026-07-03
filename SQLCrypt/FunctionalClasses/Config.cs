using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Diagnostics;


namespace SQLCrypt.FunctionalClasses
{

    internal class Config
    {
        // Usar constantes de AppSettings
        private static string CONFIG_FILE = AppSettings.ConfigFilePath;

        public string WorkingDirectory { get; set; }
        public string CurrentConnection { get; set; }

        // Recent Files List.
        public List<string> LastOpenedFiles { get; set; }

        //Posicionamiento -  GetFormLocation - SetFormLocation
        public Point WinLocation { get; set; }
        public int WinWidth { get; set; }
        public int WinHeight { get; set; }
        //-----------------------------------------------------


        /// <summary>
        /// Constructor
        /// </summary>
        public Config()
        {
            WorkingDirectory = Directory.GetCurrentDirectory();
            CurrentConnection = "";
            LastOpenedFiles = new List<string>();
            WinLocation = new Point(0, 0);
            WinWidth = AppSettings.WindowMinWidth;
            WinHeight = AppSettings.WindowMinHeight;
        }


        /// <summary>
        /// Append File to Recent File List (Limited to MaxRecentFiles entries)
        /// </summary>
        /// <param name="filename">Full path to the file to add</param>
        /// <exception cref="ArgumentException">If filename is null or empty</exception>
        public void AddOpenedFile(string filename)
        {
            // Validación de parámetros
            if (string.IsNullOrWhiteSpace(filename))
            {
                Debug.WriteLine("AddOpenedFile: filename is null or empty");
                throw new ArgumentException("Filename cannot be null or empty", nameof(filename));
            }

            // Validar que el archivo existe
            if (!File.Exists(filename))
            {
                Debug.WriteLine($"AddOpenedFile: File not found: {filename}");
                // No lanzar excepción, solo registrar warning
                // throw new FileNotFoundException($"File not found: {filename}");
                return; // Silently ignore non-existent files
            }

            // Normalizar la ruta
            string normalizedPath = Path.GetFullPath(filename);

            // Remover todas las instancias existentes (case-insensitive)
            LastOpenedFiles.RemoveAll(f => 
                string.Equals(f, normalizedPath, StringComparison.OrdinalIgnoreCase));

            // Inserto en pos 0
            LastOpenedFiles.Insert(0, normalizedPath);

            // Limitar a MaxRecentFiles
            while (LastOpenedFiles.Count > AppSettings.MaxRecentFiles)
                LastOpenedFiles.RemoveAt(LastOpenedFiles.Count - 1);
        }


        /// <summary>
        /// Save Configuration to Json File Format
        /// </summary>
        /// <exception cref="IOException">If unable to write to config file</exception>
        public void SaveToJson()
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, jsonString);
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Error saving config: {ex.Message}");
                throw new IOException($"Unable to save configuration to {CONFIG_FILE}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine($"Access denied saving config: {ex.Message}");
                throw new UnauthorizedAccessException($"Access denied to {CONFIG_FILE}", ex);
            }
        }


        /// <summary>
        /// Sets Form Location/Size from Config
        /// </summary>
        /// <param name="frm">Form to configure</param>
        /// <exception cref="ArgumentNullException">If frm is null</exception>
        public void SetFormLocation(Form frm)
        {
            if (frm == null)
                throw new ArgumentNullException(nameof(frm), "Form cannot be null");

            frm.Location = (WinLocation.X < 0 || WinLocation.Y < 0) 
                ? new Point(0, 0) 
                : WinLocation;

            frm.Width = Math.Max(WinWidth, AppSettings.WindowMinWidth);
            frm.Height = Math.Max(WinHeight, AppSettings.WindowMinHeight);
        }



        /// <summary>
        /// Get Form Location/Size from Form and store in Config
        /// </summary>
        /// <param name="frm">Form to read from</param>
        /// <exception cref="ArgumentNullException">If frm is null</exception>
        public void GetFormLocation(Form frm)
        {
            if (frm == null)
                throw new ArgumentNullException(nameof(frm), "Form cannot be null");

            WinLocation = frm.Location;
            WinWidth = frm.Width;
            WinHeight = frm.Height;
        }



        /// <summary>
        /// Loads Class from Json File.
        /// </summary>
        /// <returns>Config instance loaded from file, or new instance if file doesn't exist</returns>
        public static Config LoadFromJson()
        {
            try
            {
                if (!File.Exists(CONFIG_FILE))
                {
                    Debug.WriteLine($"Config file not found, creating new: {CONFIG_FILE}");
                    return new Config();
                }

                string jsonString = File.ReadAllText(CONFIG_FILE);

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    Debug.WriteLine("Config file is empty, creating new");
                    return new Config();
                }

                var config = JsonConvert.DeserializeObject<Config>(jsonString);

                // Validar que la deserialización fue exitosa
                if (config == null)
                {
                    Debug.WriteLine("Failed to deserialize config, creating new");
                    return new Config();
                }

                // Asegurar que LastOpenedFiles no sea null
                if (config.LastOpenedFiles == null)
                    config.LastOpenedFiles = new List<string>();

                return config;
            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"JSON error loading config: {ex.Message}");
                return new Config();
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"IO error loading config: {ex.Message}");
                return new Config();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error loading config: {ex.Message}");
                return new Config();
            }
        }
    }
}
