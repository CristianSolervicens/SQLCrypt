using SQLCrypt.FunctionalClasses.MySql;
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
    public partial class frmPassWord : Form
    {
        public frmPassWord()
        {
            InitializeComponent();
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            txClaveEncriptada.Text = string.Empty;

            if ( string.IsNullOrEmpty( txClaveClara.Text) )
            {
                return;
            }

            txClaveEncriptada.Text = MySql.EncryptStringToString(txClaveClara.Text);
            Clipboard.Clear();
            Clipboard.SetText(txClaveEncriptada.Text); 
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txClaveEncriptada.Text=string.Empty;

            if (string.IsNullOrEmpty(txClaveClara.Text))
            {
                return;
            }

            txClaveEncriptada.Text= MySql.DecryptStringToString(txClaveClara.Text);
            Clipboard.Clear();
            Clipboard.SetText(txClaveEncriptada.Text);
        }
    }
}
