using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SQLCrypt.FunctionalClasses.MySql;


namespace SQLCrypt
{
    public partial class frmConexion:Form
    {
        
        private Conexiones2 conx;
        private int ConxIdx;
        public string ArchivoConexiones;
        private bool NewEntry = false;

        private ToolTip ttip;


        public string Descripcion
        {
          get { return cbDescripcion.Text; }
        }

        public string Servidor
        {
            get { return txServer.Text; }
        }

        public string BaseDeDatos
        {
            get { return cbBases.Text; }
        }


        public string ConnectionString
        {
            get;
            set;
        }

        public frmConexion()
        {
            InitializeComponent();

            ttip = new ToolTip();
            ttip.SetToolTip(btAceptar, "Connect With current settings");
            ttip.SetToolTip(btUpdate, "Save current connection");
            ttip.SetToolTip(pbTest, "Just test current connection");
            ttip.SetToolTip(btDelConexion, "Delete current connection.");
            ttip.SetToolTip(btCancelar, "Exit, Do not connect to a Database.");
            ttip.SetToolTip(btNew, "New Connection (Wipes current info.)");
            ttip.SetToolTip(this, "Use Up & Dn arrows to select connections");

            ArchivoConexiones = Path.GetDirectoryName(Application.ExecutablePath) + "\\Conexiones.xml";
            this.conx = new Conexiones2();
            this.ConxIdx = -1;

            if (File.Exists(ArchivoConexiones))
                this.conx.LoadElementsFromFile(ArchivoConexiones);

            chkSavePasswd.Checked = true;
            this.LoadConnections();
            if (cbDescripcion.Items.Count > 0)
                cbDescripcion.SelectedIndex = 0;
        }


        private void LoadConnections()
        {
            cbDescripcion.Items.Clear();
            foreach (var obj in this.conx)
            {
                cbDescripcion.Items.Add(obj.Descripcion);
            }
        }


        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.ConnectionString = string.Empty;
            this.Close();
        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            CreateConnectionString();
            this.Close();
        }

        
        private void CreateConnectionString()
        {
            string post;

            post = "";
            //server=(local);user id=ab;password= a!Pass113;initial catalog=AdventureWorks\n
            this.ConnectionString = "";
            if (txServer.Text == "")
                this.ConnectionString += "server=.";
            else
                this.ConnectionString += "server=" + txServer.Text;

            if ( !(cbBases.Text == "" || string.IsNullOrWhiteSpace(cbBases.Text)) )
               this.ConnectionString += ";initial catalog=" + cbBases.Text;
            
            if (!( string.IsNullOrEmpty(txUser.Text) || string.IsNullOrWhiteSpace(txUser.Text) ))
               this.ConnectionString += ";user id=" + txUser.Text;
            else
               post = ";Trusted_Connection = true";

            if (!(string.IsNullOrEmpty(txPass.Text) || string.IsNullOrWhiteSpace(txPass.Text)))
                this.ConnectionString += ";password=" + txPass.Text;


            this.ConnectionString +=   ";Application Name=" + Application.ProductName + post;
        }

        private void pbTest_Click(object sender, EventArgs e)
        {
            CreateConnectionString();

            MySql hSql = new MySql();

            hSql.ConnectionString = this.ConnectionString;
            hSql.ConnectToDB();

            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Prueba Erronea");
                return;
            }

            hSql.ExecuteSqlData("select name from sys.databases order by name");

            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error Obteniendo Bases");
                return;
            }

            cbBases.Items.Clear();

            while (hSql.Data.Read())
            {
                cbBases.Items.Add(hSql.Data.GetString(0));
            }


            MessageBox.Show(this, "Se ha conectado a la Base de Datos!!\nCadena de conexión probada exitosamente", "Prueba exitosa", MessageBoxButtons.OK);

            cbDescripcion.Focus();
        }


        private void btDelConexion_Click(object sender, EventArgs e)
        {
            if (cbDescripcion.SelectedIndex != -1)
                this.conx.RemoveAt(cbDescripcion.SelectedIndex);
            
            this.conx.SaveElementstoFile(ArchivoConexiones);

            this.LoadConnections();
            if (cbDescripcion.Items.Count != 0)
                cbDescripcion.SelectedIndex = 0;
            else
                cbDescripcion.SelectedIndex = -1;
            
            cbDescripcion.Focus();
        }


        private void btUpdate_Click(object sender, EventArgs e)
        {
            if (cbDescripcion.Text == "")
            {
                MessageBox.Show("Connection Name can't be empty");
                return;
            }

            if (this.NewEntry)
            {
                string myPasswd = "";
                if (chkSavePasswd.Checked)
                    myPasswd = txPass.Text;
                
                int idx = this.conx.Add(cbDescripcion.Text, txServer.Text, cbBases.Text, txUser.Text, myPasswd);

                if (idx != -1)
                {
                    ConxIdx = idx;
                    this.conx.SaveElementstoFile(ArchivoConexiones);
                    this.LoadConnections();
                    cbDescripcion.SelectedIndex = ConxIdx;
                }
                else
                {
                    MessageBox.Show("Can't create current entry. Description Name already exists");
                    idx = this.conx.findObjectIndexByName(cbDescripcion.Text);
                    if (idx != -1)
                    {
                        ConxIdx = idx;
                        cbDescripcion.SelectedIndex = ConxIdx;
                    }
                }

                this.NewEntry = false;
                return;
            }

            int Idx = cbDescripcion.SelectedIndex;
            if ( Idx == -1)
            {
                if (MessageBox.Show("Desea Actualizar el Nombre la Conexión ?", "Atención", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            conx[ConxIdx].Descripcion = cbDescripcion.Text;
            conx[ConxIdx].Server = txServer.Text;
            conx[ConxIdx].Database = cbBases.Text;
            conx[ConxIdx].User = txUser.Text;
            if (chkSavePasswd.Checked)
                conx[ConxIdx].Password = txPass.Text;
            else
                conx[ConxIdx].Password = "";

            this.conx.SaveElementstoFile(ArchivoConexiones);
            ConxIdx = cbDescripcion.SelectedIndex;
            this.LoadConnections();
            cbDescripcion.SelectedIndex = ConxIdx;

            cbDescripcion.Focus();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            cbDescripcion.Text = "";
            txServer.Text = "";
            cbBases.Text = "";
            txUser.Text = "";
            txPass.Text = "";

            this.NewEntry = true;
            cbDescripcion.Focus();
        }


        private void cbDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConxIdx = cbDescripcion.SelectedIndex;
            if (ConxIdx == -1)
            {
                txServer.Text = "";
                cbBases.Text = "";
                txUser.Text = "";
                txPass.Text = "";
                this.NewEntry = true;
            }

            txServer.Text = conx[ConxIdx].Server;
            cbBases.Text = conx[ConxIdx].Database;
            txUser.Text = conx[ConxIdx].User;
            txPass.Text = conx[ConxIdx].Password;
        }
    }
}
