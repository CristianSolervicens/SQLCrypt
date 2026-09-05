using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using SQLCrypt.FunctionalClasses.MySql;
using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace SQLCrypt
{
    static class Program
    {

        public static MySql hSql = new MySql();
        public static Form mainForm = null;

        private enum DPI_AWARENESS_CONTEXT
        {
            DPI_AWARENESS_CONTEXT_UNAWARE = -1,
            DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2,
            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDpiAwarenessContext(IntPtr value);

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            var fileName = "";
            if (args.Length == 1)
            {
                fileName = args[0];
                if (!System.IO.File.Exists(fileName)) {
                    fileName = "";
                }

            }

            try
            {
                // Lock the application into PerMonitorV2 BEFORE loading any forms or WPF references
                SetProcessDpiAwarenessContext((IntPtr)DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            }
            catch
            {
                // Fallback for older Windows versions if the API call is missing
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSqlCrypt(hSql, fileName));
        }

    }
}