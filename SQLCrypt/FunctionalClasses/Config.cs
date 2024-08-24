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


namespace SQLCrypt.FunctionalClasses
{

    internal class Config
    {
        private static string CONFIG_FILE = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config.json";
        private static int WIN_MIN_WIDTH = 1173;
        private static int WIN_MIN_HEIGHT = 664;

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
            WinLocation = new Point(0,0);
            WinWidth = WIN_MIN_WIDTH;
            WinHeight = WIN_MIN_HEIGHT;
        }


        /// <summary>
        /// Append File to Recent File List (Limited to 15 entries)
        /// </summary>
        /// <param name="filename"></param>
        public void AddOpenedFile(string filename)
        {
            //Si el archivo ya existe en la lista, lo saco y quedará en primer lugar
            while (LastOpenedFiles.Contains(filename))
                LastOpenedFiles.Remove(filename);

            //Inserto en pos 0
            LastOpenedFiles.Insert(0, filename);
            
            //Saco el último elemento de la lista si es que hay más de 15
            while (LastOpenedFiles.Count > 15)
                LastOpenedFiles.RemoveAt(LastOpenedFiles.Count-1);
        }


        /// <summary>
        /// Save Configuration to Json File Format
        /// </summary>
        public void SaveToJson()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(CONFIG_FILE, jsonString);
        }


        /// <summary>
        /// Sets Form Location/Size from Config
        /// </summary>
        /// <param name="frm"></param>
        public void SetFormLocation(Form frm)
        {
            frm.Location = (WinLocation.X < 0 || WinLocation.Y < 0)? new Point(0,0) : WinLocation;
            frm.Width = WinWidth < WIN_MIN_WIDTH? WIN_MIN_WIDTH: WinWidth;
            frm.Height = WinHeight < WIN_MIN_HEIGHT? WIN_MIN_HEIGHT: WinHeight;
        }



        /// <summary>
        /// Get For Location/Size from Configuration File
        /// </summary>
        /// <param name="frm"></param>
        public void GetFormLocation(Form frm)
        {
            WinLocation = frm.Location;
            WinWidth = frm.Width;
            WinHeight = frm.Height;
        }



        /// <summary>
        /// Loads Class from Json File.
        /// </summary>
        /// <returns></returns>
        public static Config LoadFromJson()
        {
            if (!File.Exists(CONFIG_FILE))
            {
                return new Config();
            }
            string jsonString = File.ReadAllText(CONFIG_FILE);
            return JsonConvert.DeserializeObject<Config>(jsonString);

        }
    }
}
