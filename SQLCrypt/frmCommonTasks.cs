using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SQLCrypt
{
    public partial class frmCommonTasks : Form
    {

        public bool Cancelado { get; internal set; }
        public string SelectedFile { get; internal set; }

        string sPath = "";


        /// <summary>
        /// frmCommonTasks()
        /// </summary>
        public frmCommonTasks()
        {
            InitializeComponent();
            this.SelectedFile = string.Empty;
            this.Cancelado = true;
        }


        /// <summary>
        /// btSalir_Click()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Cancelado = true;
            this.SelectedFile = string.Empty;
            this.Close();
        }


        /// <summary>
        /// btOk_Click()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            if ( lsTask.SelectedIndex == -1)
            {
                this.btOk.Enabled = false;
                return;
            }

            this.Cancelado = false;
            this.SelectedFile = sPath + ((string)this.lsTask.Items[lsTask.SelectedIndex]) + ".sqc";
            this.Close();
        }


        /// <summary>
        /// CargaComandos()
        /// </summary>
        private void CargaComandos()
        {
            sPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\CommonTasks\\";

            string[] fileEntries = Directory.GetFiles(sPath );
            foreach (string fileName in fileEntries)
            {
                if (string.Compare(Path.GetExtension(fileName), ".sqc", true) == 0)
                {
                    string fName = Path.GetFileNameWithoutExtension(fileName);
                    lsTask.Items.Add(fName);
                }
            }
            lsTask.SelectedIndex = -1;
        }
        

        /// <summary>
        /// frmCommonTasks_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCommonTasks_Load(object sender, EventArgs e)
        {
            this.btOk.Enabled = false;
            CargaComandos();
        }


        /// <summary>
        /// lsTask_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsTask.SelectedIndex == -1)
            {
                this.btOk.Enabled = false;
                return;
            }

            this.btOk.Enabled = true;
        }


        private void lsTask_DoubleClick(object sender, EventArgs e)
        {
            if (lsTask.SelectedIndex == -1)
                return;

            btOk_Click(null, null);

        }

    }
}
