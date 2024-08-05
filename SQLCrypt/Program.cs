using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Windows.Forms;
using SQLCrypt.FunctionalClasses.MySql;


namespace SQLCrypt
{
    static class Program
    {

        public static MySql hSql = new MySql();

        //Para manejar Cancelación de Procesos.
        public static MySql hSqlQuery = null;
        public static int sql_spid = 0;
        public static bool CancelQuery = false;


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