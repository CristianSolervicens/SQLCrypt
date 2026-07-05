using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLCrypt.frmUtiles
{
    public partial class frmFindColName : Form
    {
        public string ColumnName { get; private set; }
        public frmFindColName()
        {
            InitializeComponent();
        }      
        

        private void txtColumnName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ColumnName = "";
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                ColumnName = txtColumnName.Text.Length > 0 ? txtColumnName.Text : "";
                this.Close();
            }
        }
    }
}
