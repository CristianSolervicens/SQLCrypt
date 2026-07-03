using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace SQLCrypt
{
    /// <summary>
    /// Conexión a Base de Datos con descripción
    /// </summary>
    [Serializable]
    public class Conexion
    {
        private string _password;

        public string Descripcion { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }

        /// <summary>
        /// Password stored encrypted in JSON, but accessible as plain text in code
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string Password
        {
            get
            {
                // Decrypt when reading
                return PasswordEncryption.Decrypt(_password);
            }
            set
            {
                // Encrypt when setting
                _password = PasswordEncryption.Encrypt(value);
            }
        }

        /// <summary>
        /// Internal storage for encrypted password (used by JSON serializer)
        /// </summary>
        [Newtonsoft.Json.JsonProperty("Password")]
        private string PasswordEncrypted
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Gets the raw encrypted password value for checking
        /// </summary>
        public string GetPasswordEncrypted()
        {
            return _password;
        }

        public Conexion()
        {

        }

        public Conexion(string Descripcion, string Server, string Database, string User, string Password)
        {
            this.Descripcion = Descripcion;
            this.Server = Server;
            this.Database = Database;
            this.User = User;
            this.Password = Password; // Will be encrypted automatically
        }
    }


    /// <summary>
    /// Lista de Conexiones con soporte de descripción
    /// </summary>
    [Serializable]
    public class Conexiones : List<Conexion>
    {
        /// <summary>
        /// Agrega una nueva conexión a la lista
        /// </summary>
        public int Add(string Descripcion, string Server, string Database, string User, string Password)
        {
            if (string.IsNullOrWhiteSpace(Descripcion))
                throw new ArgumentException("Descripción no puede estar vacía", nameof(Descripcion));

            var obj = this.FirstOrDefault(a => a.Descripcion == Descripcion);
            if (obj != null)
            {
                return -1; // Ya existe
            }

            Conexion Obj = new Conexion(Descripcion, Server, Database, User, Password);
            this.Add(Obj);
            return this.Count - 1;
        }

        public int findObjectInList(Conexion obj)
        {
            if (obj == null)
                return -1;
            return this.FindIndex(a => a.Descripcion == obj.Descripcion);
        }

        public int findObjectIndexByName(string Description)
        {
            if (string.IsNullOrWhiteSpace(Description))
                return -1;
            return this.FindIndex(a => a.Descripcion == Description);
        }


        /// <summary>
        /// Carga conexiones desde archivo JSON
        /// </summary>
        public int LoadElementsFromFile(string MyFileName)
        {
            this.Clear();

            if (!File.Exists(MyFileName))
                return 0;

            bool needsMigration = false;

            try
            {
                string jsonContent = File.ReadAllText(MyFileName);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    return 0;
                }

                var connections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Conexion>>(jsonContent);
                if (connections != null)
                {
                    // Check if any passwords need encryption
                    foreach (var conn in connections)
                    {
                        // Access the password property to trigger decryption/detection
                        string pwd = conn.Password;

                        // If password was plain text, accessing it doesn't encrypt it yet
                        // We need to check if it needs migration
                        if (!string.IsNullOrEmpty(pwd) && !PasswordEncryption.IsEncrypted(conn.GetPasswordEncrypted()))
                        {
                            needsMigration = true;
                        }
                    }

                    this.AddRange(connections);
                }

                System.Diagnostics.Debug.WriteLine($"Successfully loaded {this.Count} connection(s) from JSON.");

                // Auto-save to encrypt plain text passwords
                if (needsMigration)
                {
                    System.Diagnostics.Debug.WriteLine("Plain text passwords detected. Encrypting and saving...");
                    this.SaveElementstoFile(MyFileName);
                    System.Diagnostics.Debug.WriteLine("Passwords encrypted and saved.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading connections: {ex.Message}");
                if (ex.InnerException != null)
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                return 0;
            }

            return this.Count;
        }


        public int SaveElementstoFile(string MyFileName)
        {
            try
            {
                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(MyFileName, jsonContent);
                System.Diagnostics.Debug.WriteLine($"Successfully saved {this.Count} connection(s) to JSON.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving connections: {ex.Message}");
                throw new IOException($"Unable to save connections to {MyFileName}", ex);
            }

            return this.Count;
        }
    }
}

