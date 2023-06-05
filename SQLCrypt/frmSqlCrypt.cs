using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQLCrypt.StructureClasses;
using SQLCrypt.FunctionalClasses.MySql;

namespace SQLCrypt
{
    public partial class frmSqlCrypt : Form
    {
        DbObjects Objetos;
        ToolTip MytoolTip = new ToolTip();
        public string ConnectionFile = "";

        string   CurrentFile = "";
        bool     IsEncrypted = false;

        public string WorkPath = "";
        private string Server = "";
        private int TextLimit = 0;

        private string PanelTipoObjetos = "";

        private string sTabla;
        private TableDef Table;
        private int lsColumnasTop = 585;

        private MySql hSql;


        public frmSqlCrypt(MySql hSql)
        {
            InitializeComponent();

            laTablas.Text = "";
            tssLaFile.Text = "";
            tssLaPos.Text = string.Empty;
            tssLaStat.Text = string.Empty;

            txtSql.AllowDrop = true;
            this.txtSql.DragEnter += new DragEventHandler(this.txtSql_DragEnter);
            this.txtSql.DragDrop += new DragEventHandler(this.txtSql_DragDrop);

            this.hSql = hSql;

            splitC.Panel1Collapsed = true;
            sTabla = string.Empty;

            ContextMenu colm = new ContextMenu();
            colm.MenuItems.Add("Selección al Clipboard", new EventHandler(colmSelectionToClipBoard));
            colm.MenuItems.Add("Nombre al Clipboard", new EventHandler(colmSelectionNameToClipBoard));

            lsColumnas.ContextMenu = colm;

            ContextMenu txm = new ContextMenu();
            txm.MenuItems.Add("Seleccionar Todo", new EventHandler(txmSelAll));
            txm.MenuItems.Add("Deseleccionar Todo", new EventHandler(txmDeSelAll));
            txm.MenuItems.Add("-");
            txm.MenuItems.Add("Cut", new EventHandler(txmCut));
            txm.MenuItems.Add("Copy", new EventHandler(txmCopy));
            txm.MenuItems.Add("Paste", new EventHandler(txmPaste));
            txm.MenuItems.Add("-");
            txm.MenuItems.Add("Ejecutar Todo/Selección", new EventHandler(ejecutarComandoToolStripMenuItem_Click));
            txm.MenuItems.Add("Ver/Ocultar Panel de Tablas", new EventHandler(verPanelDeObjetosToolStripMenuItem_Click));
            txtSql.ContextMenu = txm;

        }


        private void ObjectSelectCount(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("Esta opción es sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int Count = DBObj.GetCountRows();

            MessageBox.Show(string.Format("La Tabla [{0}].[{1}] tiene {2} Filas.", DBObj.schema_name, DBObj.name, Count), "Filas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void txmCut(object sender, EventArgs e)
        {
            Clipboard.Clear();
            if (string.IsNullOrEmpty(txtSql.SelectedText))
                return;
            Clipboard.SetText(txtSql.SelectedText);
            txtSql.SelectedText = "";
        }

        private void txmCopy(object sender, EventArgs e)
        {
            Clipboard.Clear();
            if (string.IsNullOrEmpty(txtSql.SelectedText))
                return;

            Clipboard.SetText( txtSql.SelectedText);
        }

        private void txmPaste(object sender, EventArgs e)
        {
            txtSql.SelectionLength = 0;
            txtSql.SelectedText = Clipboard.GetText();
        }

        private void txmDeSelAll(object sender, EventArgs e)
        {
            txtSql.SelectionLength = 0;
        }


        private void txmSelAll(object sender, EventArgs e)
        {
            txtSql.SelectAll();
        }



        private void txtSql_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Move;
        }


        private void txtSql_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool encriptado = false;

            if (files.Length > 1)
            {
                MessageBox.Show("Sólo puede arrastrar un archivo.", "Atención");
                return;
            }

            if (MessageBox.Show("Open as Encrypted File (yes/no)?", "Seleccione", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification) == DialogResult.Yes )
                encriptado = true;

            if (string.Compare(System.IO.Path.GetExtension(files[0]), ".sqc", true) == 0 || encriptado)
                OpenCryptoFile(files[0]);
            else
            {
                IsEncrypted = false;
                CurrentFile = files[0];
                WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
                tssLaPath.Text = WorkPath;

                this.Text = "SQLCrypt - " + CurrentFile;
                txtSql.Text = System.IO.File.ReadAllText(files[0]);
                grabarToolStripMenuItem.Enabled = true;
                cerrarToolStripMenuItem.Enabled = true;
            }

            this.TopMost = true;
            System.Threading.Thread.Sleep(500);
            this.TopMost = false;
            this.BringToFront();
        }


        private void OpenCryptoFile(string sFileName)
        {
            CurrentFile = sFileName;
            IsEncrypted = true;
            WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
            tssLaPath.Text = WorkPath;

            txtSql.Text = hSql.DecryptFiletoString(CurrentFile);

            this.Text = "SQLCrypt - " + CurrentFile;
            grabarToolStripMenuItem.Enabled = true;
            cerrarToolStripMenuItem.Enabled = true;
        }


        //Menú AcercaDe...
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 Abt = new AboutBox1();
            Abt.ShowDialog();
        }


        //Menú Archivo-Salir
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hSql.ConnectionStatus == true)
                hSql.CloseDBConn();

            this.Close();
        }


        //WorkingDir
        private void directorioDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          FolderBrowserDialog fbd = new FolderBrowserDialog();
          fbd.SelectedPath = Application.StartupPath;
          if (fbd.ShowDialog() == DialogResult.OK)
            {
                WorkPath = fbd.SelectedPath;
                tssLaPath.Text = WorkPath;
            }
            else
                MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);
        }



        private void frmSqlCrypt_Load(object sender, EventArgs e)
        {
            tssLaFile.Text = "";
            tssLaPath.Text = "";
            grabarToolStripMenuItem.Enabled = false;
            WorkPath = Application.StartupPath;
        }


        //Ejecutar !!! ejecutarComandoToolStripMenuItem_Click
        private void ejecutarComandoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nParam = 0;
            hSql.ErrorClear();
            string sSqlCommand = "";

            if (txtSql.Text.Trim().ToString() == "")
            {
                MessageBox.Show(this, "No hay sentencia SQL para ejecutar", "Atención", MessageBoxButtons.OK);
                return;
            }

            if (hSql.ConnectionStatus == false)
            {
                MessageBox.Show(this, "Debe estar conectado(a) a una Base de Datos.", "Atención", MessageBoxButtons.OK);
                return;
            }

            if (chkToText.Checked)
            {
                frmDespliegueTxt frm = new frmDespliegueTxt(hSql);
                frm.Text = "Resultados : " + databasesToolStripMenuItem.Text;
                frm.Show();

                frm.Top = this.Top;
                frm.Left = this.Left;

                if (txtSql.SelectedText != "")
                    frm.ExecuteSQLStatement(txtSql.SelectedText.ToString(), TextLimit);
                else
                    frm.ExecuteSQLStatement(txtSql.Text.ToString(), TextLimit);

                return;
            }

            //PARAMETROS DE EJECUCION DEL SCRIPT.....
            //MessageBox.Show( this, "Parámetros # = " + paramCount().ToString(), "Atención", MessageBoxButtons.OK);

            MySql.strList param = new MySql.strList();
            Dictionary<string, string> DictParam = null;

            if (txtSql.SelectedText != "")
                param = hSql.GetParameters(txtSql.SelectedText.ToString());
            else
                param = hSql.GetParameters(txtSql.Text.ToString());

            nParam = param.Count;

            if (nParam != 0)
            {
                if (CurrentFile == "")
                {
                    MessageBox.Show(this, "Debe grabar el Archivo para ejecutar este tipo de comando", "Comando con Parámetros", MessageBoxButtons.OK);
                    return;
                }

                frmParam fmp = new frmParam();
                fmp.numValues = nParam;
                fmp.Parametros = param;
                fmp.ShowDialog();
                if (fmp.OutParameters.Count == 0)
                    return;

                DictParam = fmp.OutParameters;
            }

            if (txtSql.SelectedText != "")
                sSqlCommand = txtSql.SelectedText.ToString();
            else
                sSqlCommand = txtSql.Text.ToString();

            if (nParam != 0)
            {
                hSql.sPathToCommands = "";
                hSql.ExecStoredCmdData(CurrentFile, DictParam);
            }
            else
                hSql.ExecuteSqlData(sSqlCommand);

            if (hSql.Data == null)
            {
                if (hSql.ErrorExiste)
                {
                    MessageBox.Show(this, "Error SQL " + hSql.ErrorString, "Atención", MessageBoxButtons.OK);
                    hSql.ErrorClear();
                    return;
                }
                MessageBox.Show("No hay resultados para su consulta\n\n *** Mensajes ***\n\n" + hSql.Messages);
                Clipboard.Clear();
                Clipboard.SetText(hSql.Messages, TextDataFormat.Text);
                return;
            }
            frmDespliegue Despliegue = new frmDespliegue();
            Despliegue.Text = "Resultados : " + databasesToolStripMenuItem.Text;
            Despliegue.Show();
            Despliegue.Top = this.Top;
            Despliegue.Left = this.Left;

        }

        //archivoDeConexiónToolStripMenuItem_Click
        private void archivoDeConexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = WorkPath;
            ofd.FileName = "ConnectionString.cfg";
            ofd.Filter = "Config files (*.cfg)|*.cfg";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                ConnectionFile = ofd.FileName;
                if (hSql.ConnectionStatus == true)
                {
                    hSql.CloseDBConn();
                }

                hSql.ConnectionFile = ConnectionFile;

                try
                {
                    hSql.ConnectToDB();
                    tssLaFile.Text = ConnectionFile;
                }
                catch
                {
                    tssLaFile.Text = "";
                    MessageBox.Show(this, "Error conectándose a Base de Datos \n" + hSql.ErrorString, "No conectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ConnectionFile = "";
                }

                //Normalmente entra acá en caso de no conectarse.
                if (hSql.ConnectionStatus == false)
                {
                    tssLaFile.Text = "";
                    MessageBox.Show(this, "Error conectándose a Base de Datos \n" + hSql.ErrorString, "No conectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ConnectionFile = "";
                }
            }
            else
                MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);

        }

        //Grabar archivo encriptado.
        private void grabarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == "")
            {
                tssLaStat.Text = "No hay archivo Abierto";
                return;
            }

            SaveFileStd(false);
        }

        

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentFile = "";
            IsEncrypted = false;
            txtSql.Text = "";
            this.Text = "SQLCrypt";
            grabarToolStripMenuItem.Enabled = false;
            cerrarToolStripMenuItem.Enabled = true;
            tssLaStat.Text = "Archivo Cerrado...";
        }


        private void grabarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileStd(true);
        }


        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSql.Text = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = WorkPath;
            ofd.Filter = "Sql Crypt Files (*.sqc)|*.sqc|Text Files (*.txt)|*.txt|Sql Files (*.sql)|*.sql|Config Files (*.cfg)|*.cfg";
            ofd.FilterIndex = 1;
            ofd.FileName = CurrentFile;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if ( string.Compare(System.IO.Path.GetExtension(ofd.FileName), ".sqc", true) == 0 || string.Compare(System.IO.Path.GetExtension(ofd.FileName), ".cfg", true) == 0 )
                {
                    CurrentFile = ofd.FileName;
                    IsEncrypted = true;
                    OpenCryptoFile(CurrentFile);
                    this.Text = "SQLCrypt - " + CurrentFile;
                    grabarToolStripMenuItem.Enabled = true;  //Grabar Encriptado
                    grabarComoToolStripMenuItem.Enabled = true;
                    WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
                }
                else
                {
                    CurrentFile = ofd.FileName;
                    IsEncrypted = false;
                    txtSql.Text = System.IO.File.ReadAllText(CurrentFile);
                    this.Text = CurrentFile;
                    this.Text = "SQLCrypt - " + CurrentFile;
                    grabarToolStripMenuItem.Enabled = true;   //Grabar Encriptado
                    grabarComoToolStripMenuItem.Enabled = true;
                    WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
                }
            }
            else
            {
                MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);
                return;
            }
            tssLaStat.Text = "Archivo Abierto...";

        }

        private int paramCount()
        {
            int i;
            bool found = false;
            int fn;

            for (i = 1; ; ++i)
            {
                fn = txtSql.Find("#" + i.ToString() + "#");
                if (fn <= 0)
                    break;
                else
                    found = true;
            }

            if (found == false)
                return 0;
            else
                return i - 1;
        }

        private void crearArchivoDeConexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCnxFile frmC = new frmCnxFile();
            frmC.ShowDialog();

            if (string.IsNullOrWhiteSpace(frmC.ConectionFile))
            {
                return;
            }

            ConnectionFile = frmC.ConectionFile;
            hSql.ConnectionFile = ConnectionFile;

            hSql.ConnectToDB();

            //Normalmente entra acá en caso de no conectarse.
            if (hSql.ConnectionStatus == false)
            {
                tssLaFile.Text = "";
                MessageBox.Show(this, "Error conectándose a Base de Datos \n" + hSql.ErrorString, "No conectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConnectionFile = "";
            }

            //laFile.Text = ConnectionFile; //Sólo si se Conecta...
            tssLaFile.Text = ConnectionFile; //Sólo si se Conecta...

        }


        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(toolStripTextBox1.Text))
                return;

            int pos = txtSql.SelectionStart + txtSql.SelectionLength;
            if (pos >= txtSql.TextLength)
                pos = 0;

            try
            {
                pos = txtSql.Text.IndexOf(toolStripTextBox1.Text, pos, txtSql.TextLength - pos, StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return;
            }

            //pos = txtSql.Find(toolStripTextBox1.Text, pos, txtSql.TextLength, RichTextBoxFinds.None);
            if (pos == -1)
            {
                txtSql.SelectionStart = 0;
                txtSql.SelectionLength = 0;
            }
            else
            {
                txtSql.SelectionStart = pos;
                txtSql.SelectionLength = toolStripTextBox1.Text.Length;
            }

            txtSql.Refresh();
            Application.DoEvents();

        }

        private void reemplazarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(toolStripTextBox1.Text) || string.IsNullOrWhiteSpace(replaceToolStripMenuItem.Text))
                return;

            txtSql.Text = txtSql.Text.Replace(toolStripTextBox1.Text, replaceToolStripMenuItem.Text);
            tssLaStat.Text = string.Empty;
        }


        private void txtSql_TextChanged(object sender, EventArgs e)
        {
            tssLaStat.Text = string.Empty;
        }


        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0) dif = sValue.Length;

            return sValue + new String(' ', dif);
        }

        private void txtSql_SelectionChanged(object sender, EventArgs e)
        {
            tssLaStat.Text = string.Empty;
            try
            {
                int line = txtSql.GetLineFromCharIndex(txtSql.SelectionStart);
                int column = txtSql.SelectionStart - txtSql.GetFirstCharIndexFromLine(line);

                tssLaPos.Text = StringComplete(string.Format("Fila: {0}", line), 13) + " " + StringComplete(string.Format("Col: {0}", column), 13);

            }
            catch
            {
                //Do Nothing
            }
        }

        

        private void ConectTSM_Click(object sender, EventArgs e)
        {
            if (hSql.ConnectionStatus)
                hSql.CloseDBConn();

            frmConexion frmC = new frmConexion();
            frmC.ShowDialog();

            if (frmC.ConnectionString == string.Empty)
                return;

            hSql.ConnectionString = frmC.ConnectionString;

            hSql.ErrorClear();
            hSql.CloseDBConn();
            databasesToolStripMenuItem.Items.Clear();
            hSql.ConnectToDB();

            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error conectándose a la Base de Datos");
                hSql.ErrorClear();
                return;
            }

            hSql.ExecuteSqlData("select name from sys.databases order by name");

            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error Obteniendo Bases");
                hSql.ErrorClear();
                return;
            }

            databasesToolStripMenuItem.Items.Clear();

            while (hSql.Data.Read())
            {
                databasesToolStripMenuItem.Items.Add(hSql.Data.GetString(0));
            }

            hSql.ExecuteSqlData("select db_name()");
            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error Obteniendo Bases");
                hSql.ErrorClear();
                return;
            }

            string DBName = string.Empty;

            if (hSql.Data.Read())
                DBName = hSql.Data.GetString(0);

            databasesToolStripMenuItem.Text = DBName;

            this.Server = frmC.Servidor;
            tssLaFile.Text = frmC.Servidor + " / " + DBName;

            Objetos = new DbObjects(hSql);
            Table = new TableDef(hSql);
        }


        private void buscarEnBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Debe establecer una conexión a Base de Datos", "Atención");
                return;
            }

            frmObjects frm = new frmObjects(hSql);

            frm.Show();
            frm.Top = this.Top;
            frm.Left = this.Left;
        }



        private void databasesToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (databasesToolStripMenuItem.SelectedIndex == -1)
                return;

            hSql.ExecuteSqlData("USE " + databasesToolStripMenuItem.Text);

            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error Abriendo Base de Datos");
                return;
            }

            tssLaFile.Text = this.Server + " / " + databasesToolStripMenuItem.Text;

            if (splitC.Panel1Collapsed == false)
                Load_lstObjetos("U");

        }



        private void txTextLimit_Leave(object sender, EventArgs e)
        {
            try
            {
                TextLimit = Convert.ToInt32(txTextLimit.Text);
            }
            catch
            {
                txTextLimit.Text = "0";
            }

        }



        private void txtSql_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                try
                {
                    Clipboard.SetText(Clipboard.GetText());
                }
                catch (Exception)
                {
                }
            }
        }



        private void encriptarClavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPassWord frmPass = new frmPassWord();
            frmPass.Show();
        }




        /// <summary>
        /// btConsultas_Click
        /// Consultas predefinidas de ejecución rápida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConsultas_Click(object sender, EventArgs e)
        {
            int nParam = 0;
            hSql.ErrorClear();

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención");
                return;
            }

            frmCommonTasks fcmntsk = new frmCommonTasks();
            fcmntsk.ShowDialog();

            if (fcmntsk.Cancelado)
                return;

            string ArchivoComando = fcmntsk.SelectedFile;

            //-----------------

            MySql.strList param = new MySql.strList();
            Dictionary<string, string> DictParam = null;

            string sSqlCommand = hSql.DecryptFiletoString(ArchivoComando);

            param = hSql.GetParameters(sSqlCommand);

            nParam = param.Count;

            if (nParam != 0)
            {

                frmParam fmp = new frmParam();
                fmp.numValues = nParam;
                fmp.Parametros = param;
                fmp.ShowDialog();
                if (fmp.OutParameters.Count == 0)
                    return;

                DictParam = fmp.OutParameters;
            }

            if (string.IsNullOrEmpty(sSqlCommand) || string.IsNullOrWhiteSpace(sSqlCommand))
            {
                MessageBox.Show("Comando vacío");
                return;
            }

            if (nParam != 0)
            {
                List<string> keys = new List<string>(DictParam.Keys);
                for (int x = 0; x < DictParam.Count; ++x)
                {
                    sSqlCommand = sSqlCommand.Replace(keys[x], DictParam[keys[x]]);
                }
            }

            if (chkToText.Checked)
            {
                frmDespliegueTxt frm = new frmDespliegueTxt(hSql);
                frm.Show(this);
                frm.ExecuteSQLStatement(sSqlCommand, TextLimit);
            }
            else
            {
                hSql.ExecuteSqlData(sSqlCommand);

                if (hSql.Data == null)
                    if (hSql.ErrorExiste)
                    {
                        MessageBox.Show(this, "Error SQL " + hSql.ErrorString, "Atención", MessageBoxButtons.OK);
                        hSql.ErrorClear();
                        return;
                    }

                frmDespliegue Despliegue = new frmDespliegue();
                Despliegue.Show();
            }

            databasesToolStripMenuItem_SelectedIndexChanged(null, null);
        }



        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSql.SelectAll();
        }



        private void comandosInmediatosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btConsultas_Click(null, null);
        }



        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            btConsultas_Click(null, null);
        }



        private void ejecutarTodasLasBasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < databasesToolStripMenuItem.Items.Count; ++x)
            {   
                
                databasesToolStripMenuItem.SelectedIndex = x;
                //Hubo Error al cambiarse de BD?
                if ( hSql.ErrorExiste )
                {
                    hSql.ErrorClear();
                    continue;
                }

                if (databasesToolStripMenuItem.Text == "master" || databasesToolStripMenuItem.Text == "model" || databasesToolStripMenuItem.Text == "msdb" || databasesToolStripMenuItem.Text == "tempdb")
                    continue;

                ejecutarComandoToolStripMenuItem_Click(null, null);
            }
        }



        private void IndexacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hSql.ErrorClear();

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención");
                return;
            }

            frmIndexes frmIdx= new frmIndexes( hSql);
            frmIdx.ShowDialog();

        }



        private void SaveFileStd( bool bSaveAs)
        {
            string sqlString;

            if (CurrentFile == "" || bSaveAs)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = WorkPath;
                sfd.Filter = "Sql Crypt Files (*.sqc)|*.sqc";
                sfd.Filter = "Sql Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|Sql Crypt Files (*.sqc)|*.sqc";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (sfd.FilterIndex == 1 || sfd.FilterIndex == 2)
                        IsEncrypted = false;
                    else
                        IsEncrypted = true;

                    CurrentFile = sfd.FileName;
                    this.Text = "SQLCrypt - " + CurrentFile;
                    grabarToolStripMenuItem.Enabled = true;
                    cerrarToolStripMenuItem.Enabled = true;
                }
                else
                {
                    MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);
                    return;
                }
                
            }

            if (IsEncrypted)
            {
                sqlString = txtSql.Text.ToString();
                hSql.EncryptStringtoFile(sqlString, CurrentFile);
            }
            else
            {
                txtSql.SaveFile(CurrentFile, RichTextBoxStreamType.PlainText);
            }

            tssLaStat.Text = "Archivo Grabado...";

        } //SaveFileStd



        private void salidaATextoGrillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkToText.Checked =  chkToText.Checked ? false : true ;
        }



        public void Load_lstObjetos(string type)
        {
            MytoolTip.SetToolTip(lstObjetos, "");
            lsColumnas.Items.Clear();

            Objetos.Load(type);

            int x = 0;
            lstObjetos.Items.Clear();

            foreach (var n in Objetos)
            {
                ++x;
                lstObjetos.Items.Add(n);
            }

            string sObjeto = "";
            switch (type)
            {
                case "P":
                    sObjeto = "Procs.";
                    break;

                case "U":
                    sObjeto = "Tablas";
                    break;

                default:
                    sObjeto = "Objetos";
                    break;

            }

            laTablas.Text = string.Format("{0} {1}", lstObjetos.Items.Count, sObjeto);
        }



        private void btRefresh_Click(object sender, EventArgs e)
        {
            if (hSql.ConnectionStatus == false)
            {
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            laBuscarTablas.Text = "Buscar Tablas";
            PanelTipoObjetos = "U";
            this.SetMenuTablas();
            Load_lstObjetos(PanelTipoObjetos);
        }




        private void txBuscaEnLista_KeyDown(object sender, KeyEventArgs e)
        {
            if (txBuscaEnLista.Text.Trim() == "")
                return;

            if (e.KeyData == Keys.Enter)
            {
                e.Handled = false;

                //Busco en la lista
                for (int x = 0; x < lstObjetos.Items.Count; ++x)
                {
                    if (lstObjetos.Items[x].ToString().ToUpper().Contains(txBuscaEnLista.Text.ToUpper()))
                        lstObjetos.SelectedIndices.Add(x);
                    else
                    {
                        if (lstObjetos.SelectedIndices.Contains(x))
                            lstObjetos.SelectedIndices.Remove(x);
                    }
                }

                if (lstObjetos.SelectedIndices.Count > 0)
                    lstObjetos.TopIndex = lstObjetos.SelectedIndices[0];
                else
                    lstObjetos.TopIndex = 0;
            }
        }


        private void SetMenuTablas()
        {
            lstObjetos.ContextMenu = null;
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add("Get More Info", new EventHandler(ObjGetMoreInfo));
            cm.MenuItems.Add("Get CREATE TABLE", new EventHandler(ObjGetCreateTable));
            cm.MenuItems.Add("Get Text", new EventHandler(ObjGetText));
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ObjSelectedToClipboard));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Select COUNT(*) FROM ", new EventHandler(ObjectSelectCount));
            cm.MenuItems.Add("Select TOP(10) * FROM ", new EventHandler(ObjectSelectStar));
            cm.MenuItems.Add("Select * FROM ", new EventHandler(ObjectSelectStarAll));
            cm.MenuItems.Add("Editar Datos", new EventHandler(EditarDatos));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Buscar Tablas por Columna", new EventHandler(FindTableByColumnName));

            lstObjetos.ContextMenu = cm;
        }

        private void FindTableByColumnName(object sender, EventArgs e)
        {
            if (txBuscaEnLista.Text == "")
            {
                MessageBox.Show("Indique el Texto a buscar en 'Buscar Procs.'");
            }
            Objetos.FindByColumn("U", txBuscaEnLista.Text);
            if (Objetos.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias");
            }

            Load_lstObjetos_ProcFiltered();
        }


        private void verPanelDeObjetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(hSql.ConnectionStatus == false)
            {
                splitC.Panel1Collapsed = true;
                sTabla = string.Empty;
                return;
            }

            if (splitC.Panel1Collapsed)
            {
                splitC.Panel1Collapsed = false;
                laBuscarTablas.Text = "Buscar Tablas";
                PanelTipoObjetos = "U";
                this.SetMenuTablas();
                Load_lstObjetos(PanelTipoObjetos);
            }
            else
            {
                PanelTipoObjetos = "";
                splitC.Panel1Collapsed = true;
                lstObjetos.ContextMenu = null;
            }
        }




        private void ObjSelectedToClipboard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            foreach (var a in lstObjetos.SelectedItems)
            {
                Elementos += a.ToString() + "\n";
            }
            Clipboard.SetText(Elementos);
        }



        private void colmSelectionToClipBoard( object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            foreach (var a in lsColumnas.SelectedItems)
            {
                Elementos += a.ToString() + "\n";
            }
            Clipboard.SetText(Elementos);
        }


        

        private void colmSelectionNameToClipBoard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            foreach (var a in lsColumnas.SelectedItems)
            {
                string[] elem = a.ToString().Split(' ');

                Elementos += elem[0] + "\n";
            }
            Clipboard.SetText(Elementos);
        }




        private void ObjectSelectStar(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("Esta opción es sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 

            string sAux = DBObj.GetData(true);

            frmDespliegue Despliegue = new frmDespliegue();
            Despliegue.Text = sAux;
            Despliegue.Show();
        }




        private void ObjectSelectStarAll(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("Esta opción es sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sAux = DBObj.GetData(false);

            frmDespliegue Despliegue = new frmDespliegue();
            Despliegue.Text = sAux;
            Despliegue.Show();
        }



        private void EditarDatos(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("Esta opción es sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDataEdit DataEdit = new frmDataEdit(hSql, lstObjetos.Text);
            DataEdit.Show();

        }



        private void ObjGetMoreInfo(object sender, EventArgs e)
        {
            string sSalida = string.Empty;

            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);

            DBObj = (DBObject)lstObjetos.SelectedItem;

            sSalida += string.Format("\nSchema    : {0}\n", DBObj.schema_name);
            sSalida += string.Format("Nombre    : {0}\n", DBObj.name);
            sSalida += string.Format("Id        : {0}\n", DBObj.object_id);
            sSalida += string.Format("Type      : {0}\n", DBObj.type);
            sSalida += string.Format("Type Desc : {0}\n", DBObj.type_desc);
            sSalida += string.Format("Creado    : {0}\n", DBObj.create_date);
            sSalida += string.Format("Modificado: {0}\n", DBObj.modify_date);
            sSalida += string.Format("Schema Id : {0}\n", DBObj.schema_id);
            sSalida += string.Format("Parrent Id: {0}\n", DBObj.parent_object_id);

            Clipboard.Clear();
            Clipboard.SetText(sSalida);

            MessageBox.Show("Información en Clipboard", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void lstObjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Formatear el Nombre de Tabla a Nombre "Seguro" usando las partes entre paréntesis []

            MytoolTip.SetToolTip(lstObjetos, "");

            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type != "U")
                return;

            if (DBObj.description != "")
                MytoolTip.SetToolTip(lstObjetos, DBObj.description);

            sTabla = "[" +  DBObj.schema_name + "].[" + DBObj.name + "]" ;
            
            lsColumnas.Items.Clear();
            Table.Name = sTabla;
            
            foreach(ColumnDef t in Table.Columns)
            {
                string sColumna = string.Empty;
                string DataType = string.Empty;

                switch (t.Type)
                {
                    case "INT":
                    case "SMALLINT":
                    case "TINYINT":
                    case "BIGINT":
                    case "BIT":
                    case "DATETIME":
                    case "DATE":
                    case "TIME":
                    case "FLOAT":
                    case "DATETIME2":
                        sColumna = string.Format("{0} {1} {2}", t.Name, t.Type, t.Nullable ? "    NULL" : "NOT NULL");
                        break;

                    case "XML":
                    case "MONEY":
                    case "TEXT":
                    case "NTEXT":
                    case "UNIQUEIDENTIFIER":
                    case "SQL_VARIANT":
                        sColumna = string.Format("{0} {1} {2}", t.Name, t.Type, t.Nullable ? "    NULL" : "NOT NULL");
                        break;

                    case "CHAR":
                    case "VARCHAR":
                    case "NVARCHAR":
                    case "BINARY":
                    case "VARBINARY":
                        DataType = string.Format("{0}({1})", t.Type, t.Length == -1 ? "MAX" : Convert.ToString( t.Length) );
                        sColumna = string.Format("{0} {1} {2}", t.Name, DataType, t.Nullable ? "    NULL" : "NOT NULL");
                        break;

                    case "NUMERIC":
                    case "DECIMAL":
                        DataType = string.Format("{0}({1},{2})", t.Type, t.Prec, t.Scale);
                        sColumna = string.Format("{0} {1} {2}", t.Name, DataType, t.Nullable ? "    NULL" : "NOT NULL");
                        break;

                    default:
                        try
                        {
                            DataType = string.Format("{0}({1},{2},{3})", t.Type, t.Length == -1 ? "MAX" : Convert.ToString(t.Length), t.Prec, t.Scale);
                        }
                        catch
                        {
                            DataType = string.Format("{0}", t.Type);
                        }

                        sColumna = string.Format("{0} {1} {2}\n", t.Name, DataType, t.Nullable ? "    NULL" : "NOT NULL", "");
                        break;
                }
                
                lsColumnas.Items.Add(sColumna);
            }
        }




        private void ObjGetCreateTable(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type == "P")
            {
                MessageBox.Show("Opción no corresponde, el Objeto es un procedimiento...", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sSalida = string.Empty;

            sSalida = DBObj.ObjGetCreateTable();

            Clipboard.Clear();
            Clipboard.SetText(sSalida);

            MessageBox.Show("Información pegada en el Clipboard", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }



        private void SetMenuProcedimientos()
        {
            lstObjetos.ContextMenu = null;
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add("Get More Info", new EventHandler(ObjGetMoreInfo));
            cm.MenuItems.Add("Get CREATE TABLE", new EventHandler(ObjGetCreateTable));
            cm.MenuItems.Add("Get Text", new EventHandler(ObjGetText));
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ObjSelectedToClipboard));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Find Proedure by Text", new EventHandler(FindProcedureByText));            

            lstObjetos.ContextMenu = cm;
        }


        private void FindProcedureByText(object sender, EventArgs e)
        {
            if (txBuscaEnLista.Text == "")
            {
                MessageBox.Show("Indique el Texto a buscar en 'Buscar Procs.'");
            }
            Objetos.FindByText("P", txBuscaEnLista.Text);
            if (Objetos.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias");
            }
            Load_lstObjetos_ProcFiltered();
        }


        private void Load_lstObjetos_ProcFiltered()
        {
            lstObjetos.Items.Clear();
            MytoolTip.SetToolTip(lstObjetos, "");

            string TipoObjeto = "Objetos";

            if (Objetos.Count > 0)
            {
                if (Objetos[0].type == "U")
                    TipoObjeto = "Tablas Filtradas";
                else if (Objetos[0].type == "P")
                    TipoObjeto = "Procs. Filtrados";
            }

            foreach (var n in Objetos)
            {
                lstObjetos.Items.Add(n);
            }

            laTablas.Text = string.Format("{0} {1}", lstObjetos.Items.Count, TipoObjeto);

        }


        private void btProcs_Click(object sender, EventArgs e)
        {
            if (hSql.ConnectionStatus == false)
            {
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            laBuscarTablas.Text = "Buscar Procs.";
            PanelTipoObjetos = "P";
            this.SetMenuProcedimientos();
            Load_lstObjetos(PanelTipoObjetos);
        }


        private void ObjGetText(object sender, EventArgs e)
        {
            int x = 0;
            if (lstObjetos.SelectedIndex == -1)
                return;

            //rchTxt.Text = string.Empty;
            DBObject DBObj = new DBObject(hSql);

            string SqlObtenido = string.Empty;
            foreach (DBObject ob in lstObjetos.SelectedItems)
            {
                ++x;
                
                DBObj = ob;
                SqlObtenido += ob.GetText();
            }

            txtSql.SelectionStart = 0;
            txtSql.SelectionLength = 0;
            txtSql.SelectionLength = 0;
            txtSql.SelectedText = SqlObtenido;

        }



        private void analizarDeadlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = WorkPath;
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.FileName = CurrentFile;

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);
                return;
            }

            int lineCount = 0;
            int deadlockCount = 0;
            string line;
            bool refresh = false;

            System.IO.StreamReader file = new System.IO.StreamReader(ofd.FileName);
            while ((line = file.ReadLine()) != null)
            {
                lineCount++;
                string toBeSearched = "";

                toBeSearched = "Deadlock encountered";
                if (line.Contains(toBeSearched) )
                {
                    deadlockCount++;
                    txtSql.SelectedText = string.Format("\nLínea {0} : {1} : Deadlock encontrado\n", lineCount, deadlockCount);
                    refresh = true;
                }

                toBeSearched = "PAGE:";
                if (line.Contains(toBeSearched))
                {
                    int ix = line.IndexOf(toBeSearched);
                    line = line.Substring(ix + toBeSearched.Length, 25);
                    string[] StrSplit = line.Split(':');
                    string Database = StrSplit[0].Trim();
                    string File = StrSplit[1].Trim();
                    string Page = StrSplit[2].Trim();

                    string sAux = hSql.BuscaPagina(Database, File, Page);
                    txtSql.SelectedText = string.Format("   Línea {0} PAGE:{1}:{2}:{3} = {4}\n", lineCount, Database, File, Page, sAux);
                    refresh = true;
                }

                if (line.Contains("Database Id =") && line.Contains("Object Id =") )
                {

                    string toBeSearched1 = "Database Id =";
                    int ix1 = line.IndexOf(toBeSearched1) + toBeSearched1.Length;

                    string toBeSearched2 = "Object Id =";
                    int ix2 = line.IndexOf(toBeSearched2);

                    string Database = line.Substring(ix1, ix2 - ix1).Trim();

                    ix2 += toBeSearched2.Length;

                    string Object_id = line.Substring(ix2, line.IndexOf("]") - ix2).Trim();

                    string sAux = hSql.BuscaObjeto(Database, Object_id);
                    string db_name = hSql.GetDBNameById(Database);
                    txtSql.SelectedText = string.Format("      Linea {0} Base = {1}  Objeto: {2}\n", lineCount, db_name, sAux);
                    refresh = true;
                }

                if (refresh)
                {
                    txtSql.Refresh();
                    Application.DoEvents();
                    refresh = false;
                }

            } //While lectura del archivo

            txtSql.SelectedText = string.Format("\n\nLectura del Archivo finalizada, contiene {0} líneas\n", lineCount);
            txtSql.SelectedText = string.Format("Deadlocks encontrados {0}\n", deadlockCount);

            file.Close();
            

        }

        private void buscaPaginaSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hSql.ErrorClear();

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención");
                return;
            }

            frmBuscaPagina fBusPag = new frmBuscaPagina();
            fBusPag.ShowDialog();

            if (fBusPag.Cancelado)
                return;

            string sAux = hSql.BuscaPagina(fBusPag.BaseId, fBusPag.FileId, fBusPag.PageId);
            txtSql.SelectedText = sAux;

        }

        private void ejecutarArchivosEnBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hSql.ErrorClear();
            if (hSql.ConnectionStatus == false)
            {
                MessageBox.Show(this, "Debe estar conectado(a) a una Base de Datos.", "Atención", MessageBoxButtons.OK);
                return;
            }

            frmExecFiles myForm = new frmExecFiles();
            myForm.RTEXT_Salida = txtSql;
            myForm.hSql = hSql;
            myForm.Show();
        }

    }
}