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

        private ToolTip ttip;


        public string Descripcion
        {
          get { return txDescripcion.Text; }
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
            ttip.SetToolTip(btAceptar, "Conectarse a Servicor de Base de Datos, Si la conexión no existe la agrega a la Lista de Conexiones");
            ttip.SetToolTip(btUpdate, "Actualiza la entrada en la lista de Conexiones recordadas");
            ttip.SetToolTip(pbTest, "Prueba la Conexión y se Desconecta (no la graba)");
            ttip.SetToolTip(btDelConexion, "Elimina la Entrada actual de Conexiones recordadas.");
            ttip.SetToolTip(btCancelar, "Sale de la ventana de Conexión, no se conecta a Base de Datos.");


            ArchivoConexiones = Path.GetDirectoryName(Application.ExecutablePath) + "\\Conexiones.xml";
            this.conx = new Conexiones2();
            this.ConxIdx = -1;

            if (File.Exists(ArchivoConexiones))
                this.conx.LoadElementsFromFile(ArchivoConexiones);

            if (this.conx.Count > 0)
            {
                ConxIdx = 0;
                SetConx(ConxIdx);
            }
        }


        private void SetConx(int index)
        {
            txDescripcion.Text = conx[index].Descripcion;
            txServer.Text = conx[index].Server;
            cbBases.Text = conx[index].Database;
            txUser.Text = conx[index].User;
            txPass.Text = conx[index].Password;
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.ConnectionString = string.Empty;
            this.Close();
        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            CreateConnectionString();

            int indice = this.conx.Add(txDescripcion.Text, txServer.Text, cbBases.Text, txUser.Text, txPass.Text);

            if ( indice != -1)
                ConxIdx = indice;

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

            txDescripcion.Focus();
        }


        private void frmConexion_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.conx.SaveElementstoFile(ArchivoConexiones);
        }

        private void txServer_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.conx.Count == 0)
                return;

            if (e.KeyCode == Keys.Up)
            {
                --this.ConxIdx;
                if (this.ConxIdx < 0)
                    ConxIdx = 0;

                SetConx(ConxIdx);
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                ++this.ConxIdx;
                if (this.ConxIdx >= this.conx.Count)
                    ConxIdx = this.conx.Count - 1;

                SetConx(ConxIdx);
                return;

            }
        }

        private void btDelConexion_Click(object sender, EventArgs e)
        {
            if (ConxIdx != -1 && this.conx.Count > 0)
                this.conx.RemoveAt(ConxIdx);

            if (ConxIdx != 0)
                --ConxIdx;

            SetConx(ConxIdx);

            txDescripcion.Focus();
        }

        private void txDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.conx.Count == 0)
                return;

            if (e.KeyCode == Keys.Up)
            {
                --this.ConxIdx;
                if (this.ConxIdx < 0)
                    ConxIdx = 0;

                SetConx(ConxIdx);
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                ++this.ConxIdx;
                if (this.ConxIdx >= this.conx.Count)
                    ConxIdx = this.conx.Count - 1;

                SetConx(ConxIdx);
                return;

            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            conx[ConxIdx].Descripcion = txDescripcion.Text;
            conx[ConxIdx].Server = txServer.Text;
            conx[ConxIdx].Database = cbBases.Text;
            conx[ConxIdx].User = txUser.Text;
            conx[ConxIdx].Password = txPass.Text;

            txDescripcion.Focus();
        }


    }
}
