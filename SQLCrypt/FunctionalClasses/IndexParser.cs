using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLCrypt.FunctionalClasses
{
    public class IndexParser
    {
        public string Error { get; private set; }
        public string TableName { get; private set; }
        public string IndexName { get; private set; }
        public List<string> IndexColumns { get; private set; }
        public List<string> IncludeColumns { get; private set; }

        public IndexParser(string indexSentence)
        {
            this.Error = "";
            this.IndexColumns = new List<string>();
            this.IncludeColumns = new List<string>();

            string[] kw_create = {"create index ",
                                  "create unique index ",
                                  "create unique clustered index ",
                                  "create unique nonclustered index ",
                                  "create clustered index ",
                                  "create nonclustered index " };
            int kw_create_len = -1;
            string kw_on = " on ";
            string kw_include = " include";
            string kw_o_parenthesis = "(";
            string kw_c_parenthesis = ")";
            string kw_dot = ".";

            int pos_create = -1;
            foreach(var val in kw_create)
            {
                pos_create = indexSentence.ToLower().IndexOf(val);
                if (pos_create != -1)
                {
                    kw_create_len = val.Length;
                    break;
                }
            }
            
            if (pos_create == -1)
            {
                this.Error = "Sentence \"Create Index\" NOT FOUND";
                return;
            }

            int pos_on = indexSentence.ToLower().IndexOf(kw_on);
            if (pos_on == -1)
            {
                this.Error = "Cant find \"ON\" Keyword to identify the Table name";
                return;
            }

            int start = pos_create + kw_create_len;
            this.IndexName = indexSentence.Substring(start, pos_on - start).Trim();

            var index_text = indexSentence.Substring(pos_on + kw_on.Length).Trim();


            int pos_o_p = index_text.IndexOf(kw_o_parenthesis);
            this.TableName = index_text.Substring(0, pos_o_p).Trim();

            if (this.TableName.IndexOf(kw_dot) == -1)
            {
                this.Error += (this.Error != ""? "\n": "") + "Warning: Table name must contain the Schema (Ex: dbo.table)";
                this.TableName = $"dbo.{this.TableName}";
            }

            index_text = index_text.Substring(pos_o_p + 1);
            int pos_c_p = index_text.ToLower().IndexOf(kw_c_parenthesis);
            index_text = index_text.Substring(0, pos_c_p);

            var columnas = index_text.Split(',');

            foreach (var col in columnas)
                this.IndexColumns.Add(col.Trim());

            int pos_include = indexSentence.ToLower().IndexOf(kw_include);

            if (pos_include == -1)
                return;

            int pos_s_include_cols = indexSentence.ToLower().IndexOf(kw_o_parenthesis, pos_include + kw_include.Length);
            int pos_e_include_cols = indexSentence.ToLower().IndexOf(kw_c_parenthesis, pos_include + kw_include.Length);

            index_text = indexSentence.Substring(pos_s_include_cols, pos_e_include_cols - pos_s_include_cols);
            index_text = index_text.Replace(kw_o_parenthesis, "").Replace(kw_c_parenthesis, "").Trim();
            var include_cols = index_text.Split(',');
            foreach (var col in include_cols)
                this.IncludeColumns.Add(col.Trim());

        }
    }
}
