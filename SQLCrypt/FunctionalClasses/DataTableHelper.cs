using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;


namespace SQLCrypt.FunctionalClasses
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Save DataTable content to Json File
        /// </summary>
        /// <param name="dt">DataTable to save</param>
        /// <param name="fileName">Target file path</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool SaveToFile(this DataTable dt, string fileName)
        {
            try
            {
                if (dt == null)
                    throw new ArgumentNullException(nameof(dt), "DataTable cannot be null");

                if (string.IsNullOrWhiteSpace(fileName))
                    throw new ArgumentException("File name cannot be null or empty", nameof(fileName));

                string jsonContent = JsonConvert.SerializeObject(dt, Formatting.Indented);
                File.WriteAllText(fileName, jsonContent);
                return true;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine($"Argument error in SaveToFile: {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"IO error saving file '{fileName}': {ex.Message}");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine($"Access denied saving file '{fileName}': {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error in SaveToFile: {ex.GetType().Name} - {ex.Message}");
                return false;
            }
        }

        // UNUSED METHOD
        ///// <summary>
        ///// Load Json File into DataTable
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public static int LoadFromFile(this DataTable dt, string fileName)
        //{
        //    try
        //    {
        //        string data = File.ReadAllText(fileName);
        //        dt = JsonConvert.DeserializeObject<DataTable>(data);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //    return dt.Rows.Count;
        //}

        // UNUSED METHOD
        ///// <summary>
        ///// DataTable to Json String
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static string ToJsonString(this DataTable dt)
        //{
        //    return JsonConvert.SerializeObject(dt, Formatting.Indented);
        //}

    }
}
