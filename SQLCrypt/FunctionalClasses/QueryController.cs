using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SQLCrypt.FunctionalClasses
{
    public class QueryController
    {
        public static MySql.MySql hSqlQuery { get; set; }
        public static int sql_spid { get; set; }
        public static bool CancelQuery { get; set; }
        public static string DataBase { get; set; }
        public static bool InQuery { get; set; }

        public static void Prepare()
        {
            hSqlQuery = null;
            sql_spid = 0;
            CancelQuery = false;
            DataBase = "";
            InQuery = false;
        }
    }
}
