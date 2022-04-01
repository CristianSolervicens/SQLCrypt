using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLCrypt
{
    internal class myFile
    {
        public myFile(string name)
        {
            this.name = name;
            this.result = "";
        }

        public string name { get; set; }
        public string result { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
