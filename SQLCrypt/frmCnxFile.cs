using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Crypto;
using System.Data.Odbc;

namespace SQLCrypt
{
    public partial class frmCnxFile : Form
    {

        /// <summary>
        /// Full Path a Archivo de Conexión creado.
        /// </summary>
        public string ConectionFile { get; private set; }


        //-----------------------
        //frmCnxFile
        //-----------------------
        public frmCnxFile()
        {
            InitializeComponent();
        }

        //-----------------------
        //button2_Click
        //-----------------------
        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //-----------------------
        //btCrearArchivo_Click
        //-----------------------
        private void btCrearArchivo_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            String sFileName;
            String ConnString;

            if (textBox1.Text.ToString() == "")
            {
                MessageBox.Show(this, "Debe ingresar la cadena de conexión", "Atención", MessageBoxButtons.OK);
                return;
            }

            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "Config files (*.cfg)|*.cfg";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFileName = saveFileDialog1.FileName;
                ConnString = textBox1.Text.ToString();
                Cryptus.EncryptStringtoFile(ConnString, sFileName);
                MessageBox.Show(this, "Archivo creado", "OK", MessageBoxButtons.OK);
                ConectionFile = sFileName;  //Nombre de Archivo de Salida a Exportar.
            }
            else
                MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);
        }


        //-----------------------
        //btTestConn_Click
        //-----------------------
        private void btTestConn_Click(object sender, EventArgs e)
        {
            string sError;
            System.Data.Odbc.OdbcConnection Conn;

            //Capturar el Error de Cadena de Conexión mal formada
            try
            {
                Conn = new System.Data.Odbc.OdbcConnection(textBox1.Text.ToString());
            }
            catch (System.Data.Odbc.OdbcException er)
            {
                sError = "Error: " + er.Message;
                MessageBox.Show(this, sError, "Error", MessageBoxButtons.OK);
                return;
            }

            //Capturar el error al conectarse a la Base de Datos
            try
            {
                Conn.Open();
            }
            catch (System.Data.Odbc.OdbcException er)
            {
                sError = "Error: " + er.Message;
                MessageBox.Show(this, sError, "Error", MessageBoxButtons.OK);
                return;
            }

            //Conectado con EXITO
            Conn.Close();
            Conn.Dispose();
            MessageBox.Show(this, "Se ha conectado a la Base de Datos!!\nCadena de conexión probada exitosamente", "Prueba exitosa", MessageBoxButtons.OK);
        }


        //---------------------------------
        //sqlServerToolStripMenuItem_Click
        //---------------------------------
        private void sqlServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "server=MySQLServerName;initial catalog=MyDatabaseName;" +
                            "user id=MyUsername;password=MyPassword";
        }


        //---------------------------------
        //mySqlRemotoToolStripMenuItem1_Click
        //---------------------------------
        private void mySqlRemotoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Driver={mySQL};Server=db1.database.com;Port=3306;" +
                            "Option=131072;Stmt=;Database=mydb;Uid=myUsername;" +
                            "Pwd=myPassword";
        }


        //---------------------------------
        //mySqlLocalToolStripMenuItem_Click
        //---------------------------------
        private void mySqlLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Driver={mySQL};Server=MyServerName;Option=16834;" +
                            "Database=mydb";
        }


        //-------------------------------------
        //mSOdbcAntiguoToolStripMenuItem_Click
        //-------------------------------------
        private void mSOdbcAntiguoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Driver={Microsoft ODBC Driver for Oracle};" +
                            "ConnectString=OracleServer.world;" +
                            "Uid=myUsername;" +
                            "Pwd=myPassword";
        }


        //-------------------------------------
        //mSOdbcNuevoToolStripMenuItem_Click
        //-------------------------------------
        private void mSOdbcNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Driver={Microsoft ODBC for Oracle};" +
                            "Server=OracleServer.world;" +
                            "Uid=myUsername;" +
                            "Pwd=myPassword";
        }


        //-------------------------------------
        //oracleODBCToolStripMenuItem_Click
        //-------------------------------------
        private void oracleODBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Driver={Oracle ODBC Driver};" +
                            "Dbq=myDBName;" +
                            "Uid=myUsername;" +
                            "Pwd=myPassword";
        }


        //-------------------------------------
        //oDBCToolStripMenuItem_Click
        //-------------------------------------
        private void oDBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Dsn=myDsn;" +
                            "Uid=myUsername;" +
                            "Pwd=myPassword";
        }


        //---------------------------------
        //frmCnxFile_Load
        //---------------------------------
        private void frmCnxFile_Load(object sender, EventArgs e)
        {
            ConectionFile = string.Empty;
        }

        private void sqlServerNativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "server=(local);user id=ab;password= a!Pass113;initial catalog=AdventureWorks\n";
            textBox1.Text += "Server=.\n";
            textBox1.Text += "Connect Timeout = 1000\n";
            textBox1.Text += "Trusted_Connection = true";
        }
    }
}

//Application.Exit(); 
