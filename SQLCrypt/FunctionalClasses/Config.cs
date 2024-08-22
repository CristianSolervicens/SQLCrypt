using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;


namespace SQLCrypt.FunctionalClasses
{

    internal class Config
    {
        private static string CONFIG_FILE = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "config.json";

        public string WorkingDirectory { get; set; }
        public string CurrentConnection { get; set; }

        public List<string> LastOpenedFiles { get; set; }


        public Config()
        {
            WorkingDirectory = Directory.GetCurrentDirectory();
            CurrentConnection = "";
            LastOpenedFiles = new List<string>();
        }


        public void AddOpenedFile(string filename)
        {
            LastOpenedFiles.Add(filename);
            
            while (LastOpenedFiles.Count > 15)
                LastOpenedFiles.RemoveAt(LastOpenedFiles.Count - 1);
        }


        public void SaveToJson()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(CONFIG_FILE, jsonString);
        }

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
