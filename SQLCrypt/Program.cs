using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SQLCrypt.FunctionalClasses.MySql;


namespace SQLCrypt
{
    static class Program
    {

        public static MySql hSql = new MySql();
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSqlCrypt(hSql, fileName));
        }
    }
}