using Microsoft.SqlServer.TransactSql.ScriptDom;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using ScintillaNET;
using SQLCrypt.FunctionalClasses;
using SQLCrypt.FunctionalClasses.MySql;
using SQLCrypt.StructureClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using static ScintillaNET.Style;


namespace SQLCrypt.frmUtiles
{
    
    public partial class frmIndexes : Form
    {

        private MySql hSql;
        private ToolTip ttip = new ToolTip();

        List<string> drop_indexes_statements = new List<string>();
        
        public frmIndexes(MySql hSql)
        {
            InitializeComponent();

            //lsExistingIndexes.MouseWheel += new System.Windows.Forms.MouseEventHandler(LsExistingIndexes_MouseWheel);
            lsExistingIndexes.DrawItem += new DrawItemEventHandler(lsExistingIndexes_DrawItem);

            this.hSql = hSql;
            laStatus.Text = "";
            lsExistingIndexesContextMenu();
        }


        public frmIndexes(MySql hSql, string tableName) 
        {
            InitializeComponent();

            //lsExistingIndexes.MouseWheel += new System.Windows.Forms.MouseEventHandler(LsExistingIndexes_MouseWheel);
            lsExistingIndexes.DrawItem += new DrawItemEventHandler(lsExistingIndexes_DrawItem);

            this.hSql = hSql;
            laStatus.Text = "";
            txtTableName.Text = tableName;
            
            lsExistingIndexesContextMenu();

            lsCurrentIndex.Items.Clear();
            lsExistingIndexes.Items.Clear();

            if (String.IsNullOrEmpty(txtTableName.Text))
                return;

            LoadExistingIndexes(txtTableName.Text);
        }
        

        private void lsExistingIndexesContextMenu()
        {
            lsExistingIndexes.ContextMenu = null;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ExistingIndexesToClipboard));
            cm.MenuItems.Add("Select All", new EventHandler(ExistingIndexesSelectAll));
            cm.MenuItems.Add("Invert Selection", new EventHandler(ExistingIndexesInvertSelection));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("DROP INDEX", new EventHandler(ExistingIndexDrop));
            lsExistingIndexes.ContextMenu = cm;
        }


        private void ExistingIndexDrop(object sender, EventArgs e)
        {
            for (int x = 0; x < lsExistingIndexes.Items.Count; x++)
            {
                if (lsExistingIndexes.SelectedIndices.Contains(x))
                {
                    var drop_stmt = drop_indexes_statements[x];
                    var res = MessageBox.Show($"Confirm action: {drop_stmt} ?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                        return;
                    if (res == DialogResult.No)
                        continue;
                    hSql.ExecuteSql(drop_stmt);
                    if (hSql.ErrorExiste || hSql.Messages != "")
                    {
                        var msg = $"{hSql.ErrorString}\n{hSql.Messages}";
                        MessageBox.Show(msg, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        hSql.ErrorClear();
                        hSql.ClearMessages();
                    }
                    break;
                }
                
            }
            LoadExistingIndexes(txtTableName.Text);
        }


        private void ExistingIndexesSelectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < lsExistingIndexes.Items.Count; i++)
                lsExistingIndexes.SetSelected(i, true);
        }


        private void ExistingIndexesInvertSelection(object sender, EventArgs e)
        {
            for (int i = 0; i < lsExistingIndexes.Items.Count; i++)
                lsExistingIndexes.SetSelected(i, !lsExistingIndexes.GetSelected(i));
        }



        private void btPaste_Click(object sender, EventArgs e)
        {
            laStatus.Text = "Starting...";
            this.Update();
            this.Refresh();

            txtIndex.Text = Clipboard.GetText();
            if (! string.IsNullOrEmpty(txtIndex.Text))
            {
                ProcessIndexStatement();
            }
            laStatus.Text = "Analysis done";
        }

        private void btParse_Click(object sender, EventArgs e)
        {
            laStatus.Text = "Starting...";
            this.Update();
            this.Refresh();
            
            ProcessIndexStatement();
            laStatus.Text = "Analysis done";
        }

        private void ProcessIndexStatement()
        {
            lsCurrentIndex.Items.Clear();
            lsExistingIndexes.Items.Clear();

            IndexParser ip = new IndexParser(txtIndex.Text);
            laStatus.Text = ip.Error;            
            txtTableName.Text = ip.TableName;

            int rows = CountAllRows(ip.TableName);

            lsCurrentIndex.Items.Add($"Rows in Table {ip.TableName:40} = {rows}");
            foreach (var col in ip.IndexColumns)
            {
                int col_rows = CountColumnRows(ip.TableName, col);
                lsCurrentIndex.Items.Add($"Distinct values for the Column: {col:40} = {col_rows}");
            }

            LoadExistingIndexes(ip.TableName, ip.IndexColumns);
        }


        private void LoadExistingIndexes(string tabla, List<string> columnas = null)
        {
            ttip.SetToolTip(lsExistingIndexes, "");
            lsExistingIndexes.Items.Clear();
            drop_indexes_statements.Clear();

            var table_name = tabla.Replace("[", "").Replace("]", "");

            var dt = hSql.GetTableIndexes(table_name);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"SQL Error finding Indexes {hSql.ErrorString}\n{hSql.Messages}");
                hSql.ErrorClear();
                return;
            }

            IndexParser mainIndex = null;
            if (txtIndex.Text != "")
                mainIndex = new IndexParser(txtIndex.Text);
                

            foreach( DataRow dr in dt.Rows)
            {
                string paso = dr["index_create_statement"].ToString();
                bool existe = false;

                IndexParser ip = new IndexParser(paso);
                if (ip.Error != "")
                    ip = null;

                if (mainIndex != null && ip != null)
                {
                    var result = ip.IndexColumns.Intersect<string>(mainIndex.IndexColumns);
                    if (result.Count() > 0)
                        existe = true;
                }

                if (existe)
                {
                    lsExistingIndexes.Items.Add(new MyListBoxItem(Color.DarkRed, paso));
                }
                else
                {
                    lsExistingIndexes.Items.Add(new MyListBoxItem(Color.Black, paso));
                }

                if (!string.IsNullOrEmpty( dr["index_name"].ToString()) )
                    drop_indexes_statements.Add($"DROP INDEX {table_name}.{dr["index_name"].ToString()};");
                else
                    drop_indexes_statements.Add("--NOTHING TO DO");
            }

        }


        private void lsExistingIndexes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttip.SetToolTip(lsExistingIndexes, "");

            if (lsExistingIndexes.SelectedItems.Count != 1)
                return;

            IndexParser mainIndex = null;
            if (txtIndex.Text != "")
                mainIndex = new IndexParser(txtIndex.Text);

            if (mainIndex == null)
                return;

            var item = lsExistingIndexes.Items[lsExistingIndexes.SelectedIndex] as MyListBoxItem;
            IndexParser ip = new IndexParser(item.Message);
            if (ip.Error != "")
                return;

            string ttipContent = "";

            // Index Columns
            var result = ip.IndexColumns.Intersect<string>(mainIndex.IndexColumns);
            ttipContent = $"Matches {result.Count()} of {mainIndex.IndexColumns.Count} over {ip.IndexColumns.Count}\n";
            ttipContent += "\nIndex Columns:\n";
            foreach (var col in ip.IndexColumns)
                ttipContent += $"  {col}\n";
            if (ip.IndexColumns.Count == 0)
                ttipContent += $"  None\n";

            // Included Columns
            var resInclude = ip.IncludeColumns.Intersect<string>(mainIndex.IncludeColumns);
            ttipContent += $"\nMatches {resInclude.Count()} of {mainIndex.IncludeColumns.Count} over {ip.IncludeColumns.Count}\n";
            ttipContent += "\nInclude Columns:\n";
            foreach (var col in ip.IncludeColumns)
                ttipContent += $"  {col}\n";
            if (ip.IncludeColumns.Count == 0)
                ttipContent += $"  None\n";

            ttip.SetToolTip(lsExistingIndexes, ttipContent);
        }


        private int CountColumnRows(string tabla, string columna)
        {
            int count = 0;
            string comando = $"SELECT COUNT( distinct {columna}) FROM {tabla}";
            hSql.ExecuteSqlData(comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"SQL Error: {hSql.ErrorString}");
                hSql.ErrorClear();
                return -1;
            }

            if (!hSql.Data.HasRows)
                return 0;

            hSql.Data.Read();
            count = hSql.Data.GetInt32(0);

            return count;
        }


        private int CountAllRows(string tabla)
        { 
            int count = 0;
            string comando = $"SELECT COUNT(*) FROM {tabla}";
            hSql.ExecuteSqlData(comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"SQL Error: {hSql.ErrorString}");
                hSql.ErrorClear();
                return -1;
            }

            if (!hSql.Data.HasRows)
                return 0;

            hSql.Data.Read();
            count = hSql.Data.GetInt32(0);

            return count;
        }


        private void btCreateIndex_Click(object sender, EventArgs e)
        {
            hSql.ExecuteSql(txtIndex.Text);
            if (hSql.ErrorExiste || hSql.Messages != "")
            {
                MessageBox.Show($"Error Creatind Index:\n{hSql.ErrorString} {hSql.Messages}");
                hSql.ErrorClear();
                hSql.ClearMessages();
                return;
            }
            IndexParser ip = new IndexParser(txtIndex.Text);
            if (ip.Error == "")
                LoadExistingIndexes(txtTableName.Text, ip.IndexColumns);
            else
                LoadExistingIndexes(txtTableName.Text);

            laStatus.Text = "Index Created!";
        }


        private void btGetIndexes_Click(object sender, EventArgs e)
        {
            lsCurrentIndex.Items.Clear();
            lsExistingIndexes.Items.Clear();

            if (String.IsNullOrEmpty(txtTableName.Text))
                return;
            
            LoadExistingIndexes(txtTableName.Text);
        }


        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //private void LsExistingIndexes_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e )
        //{
        //    if (Control.ModifierKeys == Keys.Control)
        //    {
        //        var currFontSize = lsExistingIndexes.Font.Size;

        //        int delta = (e.Delta) / 120;

        //        var newFontSize = currFontSize + delta;

        //        if (newFontSize <= 9.5 || newFontSize > 15)
        //            return;

        //        lsExistingIndexes.Font = new Font(lsExistingIndexes.Font.FontFamily, newFontSize);

        //        lsExistingIndexes.ResumeLayout(false);
        //    }
        //}

        void lsExistingIndexes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            bool isSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            
            MyListBoxItem item = lsExistingIndexes.Items[e.Index] as MyListBoxItem; // Get the current item and cast it to MyListBoxItem  
            if (item != null)
            {
                SolidBrush backgroundBrush = new SolidBrush(isSelected ? SystemColors.Highlight: SystemColors.Window);
                Color tColor = isSelected ? (item.ItemColor == Color.LightSkyBlue ? Color.Red : Color.White) : item.ItemColor;

                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
                e.Graphics.DrawString(item.Message, e.Font, new SolidBrush(tColor), e.Bounds, StringFormat.GenericDefault);
                
                //e.Graphics.DrawString(
                //    item.Message, // The message linked to the item  
                //    lsExistingIndexes.Font, // Take the font from the listbox  
                //    new SolidBrush(item.ItemColor), // Set the color   
                //    0, // X pixel coordinate
                //    e.Index * lsExistingIndexes.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.  
                //);
            }
            else
            {
                // something to do
            }
        }


        private void ExistingIndexesToClipboard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            if (lsExistingIndexes.SelectedItems.Count == 0)
            {
                laStatus.Text = "NOTING SELECTED !!!";
                return;
            }

            foreach (var a in lsExistingIndexes.SelectedItems)
            {
                MyListBoxItem item = a as MyListBoxItem;
                if (item != null)
                    Elementos += (Elementos != "" ? "\n" : "") + item.Message;
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);

        }

        
    }


    public class MyListBoxItem
    {
        public MyListBoxItem(Color c, string m)
        {
            ItemColor = c;
            Message = m;
        }
        public Color ItemColor { get; set; }
        public string Message { get; set; }
    }
}
