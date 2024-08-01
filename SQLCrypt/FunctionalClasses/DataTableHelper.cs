using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;


namespace SQLCrypt.FunctionalClasses
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Save DataTable content to Json File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveToFile(this DataTable dt, string fileName)
        {
            try
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(dt, Formatting.Indented));
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Load Json File into DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int LoadFromFile(this DataTable dt, string fileName)
        {
            try
            {
                string data = File.ReadAllText(fileName);
                dt = JsonConvert.DeserializeObject<DataTable>(data);
            }
            catch
            {
                return 0;
            }
            return dt.Rows.Count;
        }

        /// <summary>
        /// DataTable to Json String
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJsonString(this DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }

    }
}
