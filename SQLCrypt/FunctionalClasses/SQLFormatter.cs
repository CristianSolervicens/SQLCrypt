using Microsoft.SqlServer.TransactSql.ScriptDom;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SQLCrypt.FunctionalClasses
{
    public class SQLFormatter
    {

        private int INDENTATION_SIZE = 4;
        public string FormattedString { get; private set; }


        public List<SQLParseError> Format_TSQL(string inputString)
        {

            var generator = new Sql160ScriptGenerator();
            generator.Options.IncludeSemicolons = true;
            generator.Options.AlignClauseBodies = true;
            generator.Options.AlignColumnDefinitionFields = true;
            generator.Options.AlignSetClauseItem = true;

            generator.Options.AsKeywordOnOwnLine = true;
            generator.Options.IndentationSize = INDENTATION_SIZE;
            generator.Options.IndentSetClause = true;
            generator.Options.IndentViewBody = true;
            generator.Options.KeywordCasing = KeywordCasing.Uppercase;

            generator.Options.MultilineInsertSourcesList = true;
            generator.Options.MultilineInsertTargetsList = true;
            generator.Options.MultilineSelectElementsList = true;
            generator.Options.MultilineSetClauseItems = true;
            generator.Options.MultilineViewColumnsList = true;
            generator.Options.MultilineWherePredicatesList = true;
            
            generator.Options.NewLineBeforeWindowClause = true;
            generator.Options.NewLineBeforeCloseParenthesisInMultilineList = true;
            generator.Options.NewLineBeforeFromClause = true;
            generator.Options.NewLineBeforeGroupByClause = true;
            generator.Options.NewLineBeforeHavingClause = true;
            generator.Options.NewLineBeforeJoinClause = true;
            generator.Options.NewLineBeforeOffsetClause = true;
            generator.Options.NewLineBeforeOpenParenthesisInMultilineList = true;
            generator.Options.NewLineBeforeOrderByClause = true;
            generator.Options.NewLineBeforeOutputClause = true;
            generator.Options.NewLineBeforeWhereClause = true;

            //Recognize syntax specific to engine type - to be safe use 0
            generator.Options.SqlEngineType = SqlEngineType.Standalone; // 0 All 1 Engine 2 Azure
            generator.Options.SqlVersion = SqlVersion.Sql160;

            List<SQLParseError> out_errors = new List<SQLParseError>();


            using (TextReader txtRdr = new StringReader(inputString) )
            {
                var parser = new TSql160Parser(true, SqlEngineType.Standalone);
                if (parser == null)
                {
                    throw new Exception($"Can't create object 'TSql160Parser'");
                }

                IList<ParseError> errors;

                TSqlFragment fragment = parser.Parse(txtRdr, out errors);

                if (errors.Count > 0)
                {
                    foreach (var err in errors)
                    {
                        out_errors.Add(new SQLParseError(err.Line, err.Column, err.Message));
                    }

                    return out_errors;
                }

                string formattedoutput = "";
                generator.GenerateScript(fragment, out formattedoutput);
                this.FormattedString = formattedoutput.ToString();

            }

            return out_errors;

        }

    }

}
