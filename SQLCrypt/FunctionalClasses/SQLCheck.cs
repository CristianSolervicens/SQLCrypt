using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SQLCrypt.FunctionalClasses
{
    public class SQLCheck
    {

        public List<SQLParseError> RunCheck(string sqlCommand)
        {
            
            List<SQLParseError> m_error = new List<SQLParseError>();


            using (TextReader txtRdr = new StringReader(sqlCommand))
            {
                TSql160Parser parser = new TSql160Parser(true);
                IList<ParseError> errors;

                // TSqlFragment deriva de TSqlScript
                TSqlFragment sqlFragment = parser.Parse(txtRdr, out errors);

                foreach (ParseError error in errors)
                {
                    m_error.Add(new SQLParseError(error.Line, error.Column, error.Message));
                }

            }

            return m_error;
        }

    }

}
