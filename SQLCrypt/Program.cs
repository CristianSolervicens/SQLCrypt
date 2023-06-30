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
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new frmSqlCrypt(hSql));
      }
   }
}