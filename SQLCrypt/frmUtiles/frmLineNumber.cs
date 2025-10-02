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
    public partial class frmLineNumber : Form
    {
        public int LineNumber { get; private set; }
        public frmLineNumber()
        {
            InitializeComponent();
        }

        private void txLineNumber_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LineNumber = -1;
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                var v = dt.Compute(txLineNumber.Text, "-1");
                LineNumber = txLineNumber.Text.Length > 0 ? Convert.ToInt32(v) : -1;
                this.Close();
            }
        }
    }
}
