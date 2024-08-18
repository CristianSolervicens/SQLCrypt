using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SQLCrypt.frmUtiles
{
    public partial class frmSnippets : Form
    {
        private string sPath = string.Empty;
        public string snippet = string.Empty;
        private ToolTip toolTip = new ToolTip();

        public frmSnippets()
        {
            InitializeComponent();

            sPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Snippets\\";
            snippet = string.Empty;
            CargaComandos();
            if (lsSnippets.Items.Count == 0)
            {
                MessageBox.Show("There are No Snippets!", "Information");
                this.Close();
            }
            toolTip.SetToolTip(lsSnippets, "Esc: Exit\nEnter or Double Click: Returns selected Snippet");
        }


        private void CargaComandos()
        {
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);

            string[] fileEntries = Directory.GetFiles(sPath);
            foreach (string fileName in fileEntries)
            {
                if (string.Compare(Path.GetExtension(fileName), ".sql", true) == 0)
                {
                    string fName = Path.GetFileNameWithoutExtension(fileName);
                    lsSnippets.Items.Add(fName);
                }
            }
            lsSnippets.SelectedIndex = -1;
        }


        private void lsSnippets_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                lsSnippets_MouseDoubleClick(sender, null);
            }
        }


        private void lsSnippets_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsSnippets.SelectedIndex != -1)
            {
                snippet = File.ReadAllText($"{sPath}{lsSnippets.Items[lsSnippets.SelectedIndex]}.sql");
                this.Close();
            }
        }

    }


}