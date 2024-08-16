using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLCrypt.FunctionalClasses
{
    public class SQLParseError
    {
        public SQLParseError(int line, int column, string ErrorMessage)
        {
            this.line = line;
            this.column = column;
            this.ErrorMessage = ErrorMessage;
        }

        public int line
        { get; private set; }

        public int column
        { get; private set; }

        public string ErrorMessage
        { get; private set; }
    }
}
