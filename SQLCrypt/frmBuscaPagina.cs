using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLCrypt
{
    public partial class frmBuscaPagina : Form
    {
        public bool Cancelado;
        public string BaseId;
        public string FileId;
        public string PageId;

        public frmBuscaPagina()
        {
            InitializeComponent();
            Cancelado = true;
        }

        private void btConsultar_Click(object sender, EventArgs e)
        {
            if (txBaseId.Text != "" && txFileId.Text != "" && txPageID.Text != "")
            {
                Cancelado = false;
                BaseId = txBaseId.Text;
                FileId = txFileId.Text;
                PageId = txPageID.Text;
                Close();
            }
            else
            {
                Cancelado = true;
                MessageBox.Show("Ingrese la información requerida en los espacios de texto.");
            }

        }
    }
}
