using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQLCrypt.FunctionalClasses.MySql;

namespace SQLCrypt
{
    public partial class frmParam : Form
    {
        
        public Dictionary<string, string> OutParameters {get; set;}
        public MySql.strList Parametros {get; set;}



        private IList<Label> mLabel = new List<Label>();
        private IList<TextBox> mTextBox = new List<TextBox>();

        private const Int16 separacion = 25;

        public frmParam()
        {
            InitializeComponent();
            OutParameters = new Dictionary<string, string>();
        }

        private void frmParam_Load(object sender, EventArgs e)
        {
            int x;

            for (x = 1; x <= Parametros.Count; ++x)
            {

                mLabel.Add(new System.Windows.Forms.Label());
                mLabel[x - 1].Left = 6;
                mLabel[x - 1].Top = 13 + ((x - 1) * separacion);
                mLabel[x - 1].Height = 15;
                mLabel[x - 1].Width = 110;
                mLabel[x - 1].Visible = true;
                mLabel[x - 1].Text = Parametros[x-1];
                mLabel[x - 1].BringToFront();
                this.Controls.Add(mLabel[x - 1]);

                //--------------------------

                mTextBox.Add(new System.Windows.Forms.TextBox());
                mTextBox[x - 1].Left = 120;
                mTextBox[x - 1].Top = 13 + ((x - 1) * separacion);
                mTextBox[x - 1].BorderStyle = BorderStyle.FixedSingle;
                mTextBox[x - 1].Width = 200;
                mTextBox[x - 1].Visible = true;
                mTextBox[x - 1].Multiline = true;
                mTextBox[x - 1].BringToFront();
                this.Controls.Add(mTextBox[x - 1]);
            }

            this.Height = mTextBox[x - 2].Top + 115;
            panel.Top = mTextBox[x-2].Top  + 36;

            Application.DoEvents();

            if (Parametros.Count > 0)
                mTextBox[0].Focus();
        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < Parametros.Count; ++x)
            {
                OutParameters.Add(Parametros[x], mTextBox[x].Text);
            }
            this.Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            OutParameters.Clear();
            this.Close();
        }

        private void frmParam_Activated(object sender, EventArgs e)
        {
            if (Parametros.Count > 0)
                mTextBox[0].Focus();
        }
    }
}