using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SQLCrypt.StructureClasses;
using SQLCrypt.FunctionalClasses.MySql;
using System.IO;
using ScintillaNET;
using ScintillaFindReplaceControl;
using SQLCrypt.FunctionalClasses;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using SQLCrypt.frmUtiles;


namespace SQLCrypt
{
    public partial class FrmSqlCrypt : Form
    {
        private DbObjects Objetos;
        private ToolTip MytoolTip = new ToolTip();
        public string ConnectionFile = "";

        string CurrentFile = "";
        bool IsEncrypted = false;
        public string WorkPath = "";
        private string Server = "";
        private int TextLimit = 0;

        private string PanelTipoObjetos = "";

        private string sTabla;
        private TableDef Table;

        //Cuando se buscan Objetos, para que, en el caso
        //De estar en modo Tablas no retorne todas sus
        //ccolumnas cada vez
        private bool EnBusqueda = false;

        private MySql hSql;
        SearchManager FindMan = null;
        FindReplace _findReplace = null;
        private int maxLineNumberCharLength;


        public FrmSqlCrypt(MySql hSql, string fileName)
        {
            InitializeComponent();

            laTablas.Text = "";
            tssLaFile.Text = "";
            tssLaPos.Text = string.Empty;
            tssLaStat.Text = string.Empty;

            txtSql.AllowDrop = true;

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

            _findReplace = new FindReplace();
            _findReplace.SetTarget(txtSql);
            _findReplace.SetFind(txtSql);

            FindMan = new SearchManager();
            FindMan.TextArea = txtSql;
            InitSyntaxColoring(txtSql);

            
            if (fileName != "")
                OpenFileInEditor(fileName);
        }


        private void OpenFileInEditor(string fileName)
        {
            txtSql.Text = "";

            if (string.Compare(System.IO.Path.GetExtension(fileName), ".sqc", true) == 0 || string.Compare(System.IO.Path.GetExtension(fileName), ".cfg", true) == 0)
            {
                CurrentFile = fileName;
                IsEncrypted = true;
                OpenCryptoFile(CurrentFile);
                this.Text = $"SQLCrypt - {CurrentFile}";
                grabarComoToolStripMenuItem.Enabled = true;
                WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
            }
            else
            {
                CurrentFile = fileName;
                IsEncrypted = false;
                txtSql.Text = System.IO.File.ReadAllText(CurrentFile);
                this.Text = CurrentFile;
                this.Text = $"SQLCrypt - {CurrentFile}";
                grabarComoToolStripMenuItem.Enabled = true;
                WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
            }

            tssLaStat.Text = "Archivo Abierto...";
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
        }


        private void ObjectSelectCount(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type != "U" && DBObj.type != "V" && DBObj.type != "S")
            {
                MessageBox.Show("Esta opción es sólo para Tablas y Vistas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int Count = DBObj.GetCountRows();
            string TipoObjeto = (DBObj.type.Trim() == "U" ? "Tabla" : "Vista");
            MessageBox.Show($"La {TipoObjeto} [{DBObj.schema_name}].[{DBObj.name}] tiene {Count} Filas.", "Filas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void txmCut(object sender, EventArgs e)
        {
            Clipboard.Clear();
            if (string.IsNullOrEmpty(txtSql.SelectedText))
                return;
            Clipboard.SetText(txtSql.SelectedText);
            txtSql.SetSelection(0, 0);  //Scintilla
        }

        private void txmCopy(object sender, EventArgs e)
        {
            Clipboard.Clear();
            if (string.IsNullOrEmpty(txtSql.SelectedText))
                return;

            Clipboard.SetText(txtSql.SelectedText);
        }

        private void txmPaste(object sender, EventArgs e)
        {
            txtSql.Paste();  //Scintilla
        }

        private void txmDeSelAll(object sender, EventArgs e)
        {
            txtSql.SelectionStart = txtSql.CurrentPosition;  //Scintilla
            txtSql.SelectionEnd = txtSql.CurrentPosition;
        }


        private void txmSelAll(object sender, EventArgs e)
        {
            txtSql.SelectAll();
        }


        // TODO: Change
        private void txtSql_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            else if (e.Data.GetDataPresent(DataFormats.Text)) e.Effect = DragDropEffects.Move;
            else if (e.Data.GetDataPresent(DataFormats.UnicodeText)) e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;

        }


        // TODO: Change
        private void txtSql_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                //txtSql.SelectedText = (string)e.Data.GetData(DataFormats.Text);
                txtSql.InsertText(txtSql.CurrentPosition, (string)e.Data.GetData(DataFormats.Text));
                txtSql.Focus();
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool encriptado = false;

            if (files.Length > 1)
            {
                MessageBox.Show("Sólo puede arrastrar un archivo.", "Atención");
                return;
            }

            if (MessageBox.Show("Open as Encrypted File (yes/no)?", "Seleccione", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification) == DialogResult.Yes)
                encriptado = true;

            if (string.Compare(System.IO.Path.GetExtension(files[0]), ".sqc", true) == 0 || encriptado)
                OpenCryptoFile(files[0]);
            else
            {
                IsEncrypted = false;
                CurrentFile = files[0];
                WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
                tssLaPath.Text = WorkPath;

                this.Text = $"SQLCrypt - {CurrentFile}";
                txtSql.Text = System.IO.File.ReadAllText(files[0]);
                cerrarToolStripMenuItem.Enabled = true;
            }

            this.TopMost = true;
            System.Threading.Thread.Sleep(500);
            this.TopMost = false;
            this.BringToFront();
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
        }


        private void OpenCryptoFile(string sFileName)
        {
            CurrentFile = sFileName;
            IsEncrypted = true;
            WorkPath = System.IO.Path.GetDirectoryName(CurrentFile);
            tssLaPath.Text = WorkPath;

            txtSql.Text = hSql.DecryptFiletoString(CurrentFile);

            this.Text = $"SQLCrypt - {CurrentFile}";
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
            if (AlCerrarElFormulario() == DialogResult.Yes)
            {
                txtSql.SetSavePoint();
                this.Close();
            }
        }


        /// <summary>
        /// Validación antes de Cerrar el Formulario
        /// </summary>
        /// <returns>DialogResult   YES para Salir, Cancel NO SALIR </returns>
        private DialogResult AlCerrarElFormulario()
        {
            if (txtSql.Modified)
            {
                var res = MessageBox.Show("El Documento ha sido Modificado.\nDesea Grabar antes de Salir?", "Atención", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (CurrentFile == "")
                        SaveFileStd(true);
                    else
                        SaveFileStd(false);

                    if (txtSql.Modified)
                        return DialogResult.Cancel;
                }
                else if (res == DialogResult.Cancel)
                    return DialogResult.Cancel;
                else if (res == DialogResult.No)
                    return DialogResult.Yes;
            }

            if (hSql.ConnectionStatus == true)
                hSql.CloseDBConn();

            return DialogResult.Yes;
        }


        private void frmSqlCrypt_Load(object sender, EventArgs e)
        {
            tssLaFile.Text = "";
            tssLaPath.Text = "";
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
                frm.Text = $"Resultados : {databasesToolStripMenuItem.Text}";
                frm.Show();

                frm.Top = this.Top;
                frm.Left = this.Left;

                if (txtSql.SelectedText != "")
                    frm.ExecuteSQLStatement(txtSql.SelectedText, TextLimit);
                else
                    frm.ExecuteSQLStatement(txtSql.Text, TextLimit);

                LoadDatabaseList();

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
                    MessageBox.Show(this, $"Error SQL {hSql.ErrorString}", "Atención", MessageBoxButtons.OK);
                    hSql.ErrorClear();
                    return;
                }

                //Current DB
                LoadDatabaseList();

                MessageBox.Show("No hay resultados para su consulta\n\n *** Mensajes ***\n\n" + hSql.Messages);
                Clipboard.Clear();
                Clipboard.SetText(hSql.Messages, TextDataFormat.Text);

                return;
            }

            frmDespliegue Despliegue = new frmDespliegue();
            Despliegue.Text = $"Resultados : {databasesToolStripMenuItem.Text}";
            Despliegue.Show();
            Despliegue.Top = this.Top;
            Despliegue.Left = this.Left;

            LoadDatabaseList();

        }


        private void Lowercase()
        {

            // save the selection
            int start = txtSql.SelectionStart;
            int end = txtSql.SelectionEnd;

            // modify the selected text
            txtSql.ReplaceSelection(txtSql.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            txtSql.SetSelection(start, end);
        }


        private void Uppercase()
        {

            // save the selection
            int start = txtSql.SelectionStart;
            int end = txtSql.SelectionEnd;

            // modify the selected text
            txtSql.ReplaceSelection(txtSql.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            txtSql.SetSelection(start, end);
        }

        
        //Grabar archivo encriptado.
        private void grabarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == "")
            {
                SaveFileStd(true);
            }
            else
            {
                SaveFileStd(false);
            }
        }


        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentFile = "";
            IsEncrypted = false;
            txtSql.Text = "";
            this.Text = "SQLCrypt";
            tssLaStat.Text = "Archivo Cerrado...";
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
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
            ofd.Filter = "Sql Files (*.sql;*.sqc)|*.sql;*.sqc|Text Files (*.txt)|*.txt|Config Files (*.cfg)|*.cfg";
            ofd.FilterIndex = 1;
            ofd.FileName = CurrentFile;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenFileInEditor(ofd.FileName);
            }
            else
            {
                txtSql.SetSavePoint();
                return;
            }
            tssLaStat.Text = "Archivo Abierto...";
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
        }


        private int paramCount()
        {
            int i;
            bool found = false;
            int fn;

            for (i = 1; ; ++i)
            {
                fn = FindMan.Find(true, true, 0, $"#{i.ToString()}#");
                if (fn < 0)
                    break;
                else
                    found = true;
            }

            if (found == false)
                return 0;
            else
                return i - 1;
        }


        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0) dif = sValue.Length;

            return sValue + new String(' ', dif);
        }


        private void ConnectToDatabase(object sender, EventArgs e)
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

            LoadDatabaseList();

            this.Server = frmC.Servidor;
            tssLaFile.Text = $"{frmC.Servidor}/{databasesToolStripMenuItem.Text}";

            Objetos = new DbObjects(hSql);
            Table = new TableDef(hSql);
            Load_cbObjetos();
        }


        private void LoadDatabaseList()
        {
            string OriginalDB = databasesToolStripMenuItem.Text;
            string DBName = hSql.GetCurrentDatabase();
            databasesToolStripMenuItem.Text = databasesToolStripMenuItem.Text;

            List<string> databases = hSql.GetDatabases();

            if (databases.Count == databasesToolStripMenuItem.Items.Count)
            {
                if (OriginalDB != DBName)
                    databasesToolStripMenuItem.Text = DBName;
                return;
            }

            databasesToolStripMenuItem.Items.Clear();

            foreach (string db_name in databases)
            {
                databasesToolStripMenuItem.Items.Add(db_name);
            }

            databasesToolStripMenuItem.Text = DBName;
            if (OriginalDB != DBName && OriginalDB != "")
                if (cbObjetos.SelectedIndex != -1)
                    Load_lstObjetos(((ObjectType)cbObjetos.SelectedItem).type);
        }


        private void databasesToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (databasesToolStripMenuItem.SelectedIndex == -1)
                return;

            if (!hSql.SetDatabase(databasesToolStripMenuItem.Text))
            {
                MessageBox.Show(hSql.ErrorString, $"Error Abriendo Base de Datos\n{hSql.ErrorString}");
                hSql.ErrorClear();
                databasesToolStripMenuItem.Text = hSql.GetCurrentDatabase();
                return;
            }

            tssLaFile.Text = $"{this.Server} / {databasesToolStripMenuItem.Text}";

            if (splitC.Panel1Collapsed == false)
                cbObjetosSelect("U");
            
            // databasesToolStripMenuItem.Text = databasesToolStripMenuItem.Text;
        }


        // TODO: Change
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
                        MessageBox.Show(this, $"Error SQL\n{hSql.ErrorString}", "Atención", MessageBoxButtons.OK);
                        hSql.ErrorClear();
                        return;
                    }

                frmDespliegue Despliegue = new frmDespliegue();
                Despliegue.Show();
            }

            databasesToolStripMenuItem_SelectedIndexChanged(null, null);
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
                if (hSql.ErrorExiste)
                {
                    hSql.ErrorClear();
                    continue;
                }

                if (databasesToolStripMenuItem.Text == "master" || databasesToolStripMenuItem.Text == "model" || databasesToolStripMenuItem.Text == "msdb" || databasesToolStripMenuItem.Text == "tempdb")
                    continue;

                ejecutarComandoToolStripMenuItem_Click(null, null);
            }
        }


        private void SaveFileStd(bool bSaveAs)
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
                }
                else
                    return;

            }

            if (IsEncrypted)
            {
                sqlString = txtSql.Text.ToString();
                hSql.EncryptStringtoFile(sqlString, CurrentFile);
            }
            else
            {
                txtSql_SaveFile();
            }

            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
            tssLaStat.Text = "Archivo Grabado...";

        } //SaveFileStd


        private void txtSql_SaveFile()
        {
            System.IO.File.WriteAllText(CurrentFile, txtSql.Text);
        }


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

            laTablas.Text = $"({lstObjetos.Items.Count})";
        }


        private void txBuscaEnLista_KeyDown(object sender, KeyEventArgs e)
        {
            if (txBuscaEnLista.Text.Trim() == "")
                return;

            if (e.KeyData == Keys.Enter)
            {
                e.Handled = false;

                //Busco en la lista
                EnBusqueda = true;
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
                EnBusqueda= false;

                if (lstObjetos.SelectedIndices.Count > 0)
                {
                    lstObjetos.TopIndex = lstObjetos.SelectedIndices[0];
                }
                else
                    lstObjetos.TopIndex = 0;

            }
        }


        private void SetMenuTablas()
        {
            lstObjetos.ContextMenu = null;
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add("Get More Info", new EventHandler(ObjGetMoreInfo));
            //cm.MenuItems.Add("Get CREATE TABLE", new EventHandler(ObjGetCreateTable));
            cm.MenuItems.Add("Get Text", new EventHandler(ObjGetText));
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ObjSelectedToClipboard));
            cm.MenuItems.Add("-");

            string Type = ((ObjectType)cbObjetos.SelectedItem).type.Trim();
            if (Type == "U" || Type == "V" || Type == "S")
            {
                cm.MenuItems.Add("Select COUNT(*) FROM ", new EventHandler(ObjectSelectCount));
                cm.MenuItems.Add("Select TOP(100) * FROM ", new EventHandler(ObjectSelectStar));
                cm.MenuItems.Add("Select * FROM ", new EventHandler(ObjectSelectStarAll));
            }
            if (Type == "U")
            {
                cm.MenuItems.Add("Edit Data", new EventHandler(EditarDatos));
                cm.MenuItems.Add("Get Indexes", new EventHandler(GetIndexes));
            }
            cm.MenuItems.Add("-");

            string sAux = "";

            switch(Type)
            {
                case "S":
                case "U":
                case "IT": //Internal Table
                case "V":
                    sAux = "Table/View";
                    cm.MenuItems.Add($"Find {sAux} by Column Name", new EventHandler(FindTableByColumnName));
                    break;

                case "P":
                case "FN":  //Func escalar
                case "TF":  //Func Tabular
                case "TR":  //Trigger

                    cm.MenuItems.Add("Find Object by Text", new EventHandler(FindProcedureByText));
                    break;

            }
            
            lstObjetos.ContextMenu = cm;
        }


        private void FindTableByColumnName(object sender, EventArgs e)
        {
            if (txBuscaEnLista.Text == "")
            {
                MessageBox.Show("Indique el Texto a buscar en 'Buscar Procs.'");
            }

            string Type = ((ObjectType)cbObjetos.SelectedItem).type.Trim();
            Objetos.FindByColumn(Type, txBuscaEnLista.Text);
            
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
                laBuscarTablas.Text = "Buscar...";
                cbObjetosSelect("U");
                this.SetMenuTablas();
                Load_lstObjetos(((ObjectType)cbObjetos.SelectedItem).type.Trim());
            }
            else
            {
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
                Elementos += (Elementos != "" ? "\n" : "") + a.ToString();
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("No hay elementos Seleccionados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void colmSelectionToClipBoard( object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            
            for(int x = 0; x < lsColumnas.Items.Count; ++x)
            {
                if (lsColumnas.Items[x].Selected)
                    Elementos += (Elementos != "" ? "\n" : "") + $"{lsColumnas.Items[x].Text} {lsColumnas.Items[x].SubItems[1].Text} {lsColumnas.Items[x].SubItems[2].Text}";
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("No hay elementos Seleccionados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        private void colmSelectionNameToClipBoard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";

            for (int x = 0; x < lsColumnas.Items.Count; ++x)
            {
                if (lsColumnas.Items[x].Selected)
                    Elementos += (Elementos != "" ? "\n" : "") + $"{lsColumnas.Items[x].Text}";
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("No hay elementos Seleccionados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void ObjectSelectStar(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type != "U" && DBObj.type != "V" && DBObj.type != "S")
            {
                MessageBox.Show("Esta opción es sólo para Tablas y Vistas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (DBObj.type != "U" && DBObj.type != "V" && DBObj.type != "S")
            {
                MessageBox.Show("Esta opción es sólo para Tablas y Vistas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sAux = DBObj.GetData(false);

            frmDespliegue Despliegue = new frmDespliegue();
            Despliegue.Text = sAux;
            Despliegue.Show();
        }


        private void GetIndexes(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("Esta opción aplica sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new frmIndexes(hSql, lstObjetos.Text);
            frm.Show();
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

            sSalida += $"\nSchema    : {DBObj.schema_name}\n";
            sSalida += $"Nombre    : {DBObj.name}\n";
            sSalida += $"Id        : {DBObj.object_id}\n";
            sSalida += $"Type      : {DBObj.type}\n";
            sSalida += $"Type Desc : {DBObj.type_desc}\n";
            sSalida += $"Creado    : {DBObj.create_date}\n";
            sSalida += $"Modificado: {DBObj.modify_date}\n";
            sSalida += $"Schema Id : {DBObj.schema_id}\n";
            sSalida += $"Parrent Id: {DBObj.parent_object_id}\n";

            Clipboard.Clear();
            Clipboard.SetText(sSalida);

            MessageBox.Show("Información en Clipboard", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void lstObjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Formatear el Nombre de Tabla a Nombre "Seguro" usando las partes entre paréntesis []

            if (EnBusqueda)
                return;

            MytoolTip.SetToolTip(lstObjetos, "");

            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type != "U" && DBObj.type != "V" && DBObj.type != "S")
                return;

            if (DBObj.description != "")
                MytoolTip.SetToolTip(lstObjetos, DBObj.description);

            sTabla = $"[{DBObj.schema_name}].[{DBObj.name}]";
            
            lsColumnas.Items.Clear();
            Table.Name = sTabla;
            
            foreach(ColumnDef t in Table.Columns)
            {
                string sColName = string.Empty;
                string sColDataType = string.Empty;
                string sColNullable = string.Empty;

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
                    case "XML":
                    case "MONEY":
                    case "TEXT":
                    case "NTEXT":
                    case "UNIQUEIDENTIFIER":
                    case "SQL_VARIANT":
                        sColumna = $"{t.Name} {t.Type} {(t.Nullable ? "    NULL" : "NOT NULL")}";

                        sColName = t.Name;
                        sColDataType = $"{t.Type}";
                        sColNullable = $"{(t.Nullable ? "NULL" : "NOT NULL")}";
                        break;

                    case "CHAR":
                    case "VARCHAR":
                    case "NVARCHAR":
                    case "BINARY":
                    case "VARBINARY":
                        DataType = $"{t.Type}({(t.Length == -1 ? "MAX" : Convert.ToString(t.Length))})";
                        sColumna = $"{t.Name} {DataType} {(t.Nullable ? "    NULL" : "NOT NULL")}";

                        sColName = t.Name;
                        sColDataType = $"{t.Type}({(t.Length == -1 ? "MAX" : Convert.ToString(t.Length))})";
                        sColNullable = $"{(t.Nullable ? "NULL" : "NOT NULL")}";
                        break;

                    case "NUMERIC":
                    case "DECIMAL":
                        DataType = $"{t.Type}({t.Prec},{t.Scale})";
                        sColumna = $"{t.Name} {DataType} {(t.Nullable ? "    NULL" : "NOT NULL")}";

                        sColName = t.Name;
                        sColDataType = $"{t.Type}({t.Prec},{t.Scale})";
                        sColNullable = $"{(t.Nullable ? "NULL" : "NOT NULL")}";
                        break;

                    default:
                        try
                        {
                            if (t.Collation != "")
                                sColDataType = $"{t.Type} * {(t.Length == -1 ? "MAX" : Convert.ToString(t.Length))}";
                            else
                                sColDataType = $"{t.Type}({(t.Length == -1 ? "MAX" : Convert.ToString(t.Length))},{t.Prec},{t.Scale})";
                        }
                        catch
                        {
                            sColDataType = string.Format("{0}", t.Type);
                        }

                        sColumna = string.Format("{0} {1} {2}\n", t.Name, DataType, t.Nullable ? "    NULL" : "NOT NULL", "");

                        sColName = t.Name;
                        sColNullable = $"{(t.Nullable ? "NULL" : "NOT NULL")}";

                        break;
                }
                
                ListViewItem Item = new ListViewItem(sColName);
                Item.SubItems.Add(sColDataType);
                Item.SubItems.Add(sColNullable);
                lsColumnas.Items.Add(Item);
            }
            lsColumnas.Columns[0].Width = -1;
            lsColumnas.Columns[1].Width = -1;
            lsColumnas.Columns[2].Width = -1;

        }



        //Deshabilitado temporalmente
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
            string Type = ((ObjectType)cbObjetos.SelectedItem).type.Trim();
            if (Type != "FN" && Type != "P" && Type != "TF" && Type != "TR")
                return;

            lstObjetos.ContextMenu = null;
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add("Get More Info", new EventHandler(ObjGetMoreInfo));
            //cm.MenuItems.Add("Get CREATE TABLE", new EventHandler(ObjGetCreateTable));
            cm.MenuItems.Add("Get Text", new EventHandler(ObjGetText));
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ObjSelectedToClipboard));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Find Object by Text", new EventHandler(FindProcedureByText));

            lstObjetos.ContextMenu = cm;
        }


        private void FindProcedureByText(object sender, EventArgs e)
        {
            if (txBuscaEnLista.Text == "")
            {
                MessageBox.Show("Indique el Texto a buscar en 'Buscar...'");
            }
            Objetos.FindByText(((ObjectType)cbObjetos.SelectedItem).type, txBuscaEnLista.Text);
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

            foreach (var n in Objetos)
            {
                lstObjetos.Items.Add(n);
            }

            laTablas.Text = $"Objetos: {lstObjetos.Items.Count}";

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
            txtSql.SelectionEnd = 0;
            txtSql.InsertText(txtSql.CurrentPosition, SqlObtenido);

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
                    txtSql.InsertText(txtSql.CurrentPosition, $"\nLínea {lineCount} : {deadlockCount} : Deadlock encontrado\n");
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
                    txtSql.InsertText(txtSql.CurrentPosition, $"   Línea {lineCount} PAGE:{Database}:{File}:{Page} = {sAux}\n");
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
                    txtSql.InsertText(txtSql.CurrentPosition, $"      Linea {lineCount} Base = {db_name}  Objeto: {sAux}\n");
                    refresh = true;
                }

                if (refresh)
                {
                    txtSql.Refresh();
                    Application.DoEvents();
                    refresh = false;
                }

            } //While lectura del archivo

            txtSql.InsertText(txtSql.CurrentPosition, $"\n\nLectura del Archivo finalizada, contiene {lineCount} líneas\n");
            txtSql.InsertText(txtSql.CurrentPosition, $"Deadlocks encontrados {deadlockCount}\n");

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
            txtSql.InsertText(txtSql.CurrentPosition, sAux);

        }

        private void ejecutarArchivosEnBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por implementar");
            return;

            hSql.ErrorClear();
            if (hSql.ConnectionStatus == false)
            {
                MessageBox.Show(this, "Debe estar conectado(a) a una Base de Datos.", "Atención", MessageBoxButtons.OK);
                return;
            }

            //frmExecFiles myForm = new frmExecFiles();
            //myForm.RTEXT_Salida = txtSql;
            //myForm.hSql = hSql;
            //myForm.Show();
        }


        private void lstObjetos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lstObjetos.ContextMenu.Show(this, new Point(e.X, e.Y));
                return;
            }
            
            if (lstObjetos.SelectedItems.Count > 0)
            {
                txtSql.DoDragDrop(lstObjetos.SelectedItem.ToString(), DragDropEffects.Move);
                lstObjetos_SelectedIndexChanged(sender, e);
            }
        }


        private void lsColumnas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lsColumnas.ContextMenu.Show(this, new Point( panColumnas.Left + e.X, panColumnas.Left + lstObjetos.Height + 80 + e.Y));
                return;
            }

            if (lsColumnas.SelectedItems.Count > 0)
            { 
                string elem = lsColumnas.SelectedItems[0].Text;
                txtSql.DoDragDrop(elem, DragDropEffects.Move);
            }
            
        }

        private void Load_cbObjetos()
        {
            cbObjetos.Items.Clear();

            ObjectTypes objt = new ObjectTypes();

            foreach (var n in objt)
            {
                cbObjetos.Items.Add(n);
            }
        }

        private void cbObjetos_SelectedValueChanged(object sender, EventArgs e)
        {
            Load_lstObjetos( ((ObjectType)cbObjetos.SelectedItem).type );
            SetMenuTablas();
        }


        private void cbObjetosSelect( string Tipo)
        {
            string OriginalValue = "";
            if (cbObjetos.SelectedIndex != -1)
            {
                OriginalValue = ((ObjectType)cbObjetos.SelectedItem).type;
                if (OriginalValue == Tipo)
                    Load_lstObjetos(Tipo);
            }
            ObjectType obj = null;
            for (int x = 0; x < cbObjetos.Items.Count; ++x)
            {
                obj = (ObjectType)cbObjetos.Items[x];
                if (obj.type == Tipo)
                    cbObjetos.SelectedIndex= x;
            }
        }

        private void extendedPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Debe establecer una conexión a Base de Datos", "Atención");
                return;
            }

            frmExtendedProperties frmC = new frmExtendedProperties(hSql);
            frmC.Top = this.Top;
            frmC.Left = this.Left;
            frmC.Show();

        }


        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void InitSyntaxColoring(ScintillaNET.Scintilla TextArea)
        {

            // Configure the default style
            TextArea.Margins[0].Width = 5;

            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "Consolas";
            TextArea.Styles[Style.Default].Size = 10;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.CaretLineBackColor = IntToColor(0x333333);
            TextArea.CaretForeColor = IntToColor(0xF0F0F0);
            TextArea.CaretWidth = 2;
            TextArea.SetSelectionBackColor(true, IntToColor(0x535353));
            TextArea.StyleClearAll();

            TextArea.Styles[Style.Sql.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Sql.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Sql.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Sql.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Sql.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Sql.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Sql.Character].ForeColor = IntToColor(0xE95454);
            //TextArea.Styles[Style.Sql.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Sql.Operator].ForeColor = IntToColor(0xE0E0E0);
            //TextArea.Styles[Style.Sql.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Sql.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Sql.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Sql.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Sql.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Sql.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            //TextArea.Styles[Style.Sql.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            TextArea.Lexer = Lexer.Sql;

            TextArea.SetKeywords(0, "action add all alter and any as asc authorization backup begin between break browse bulk by cascade case check checkpoint close clustered coalesce collate column commit committed compute confirm constraint contains containstable continue controlrow convert create cross current current_date current_time current_timestamp current_user cursor database dbcc deallocate declare default delete deny desc disable disk distinct distributed double drop dummy dump else enable end errlvl errorexit escape except exec execute exists exit fetch file fillfactor floppy for foreign forward_only freetext freetexttable from full function go goto grant group having holdlock identity identity_insert identitycol if in index inner insert instead intersect into is isolation join key kill left level like lineno load mirrorexit move national no nocheck nocount nonclustered norecovery not nounload null nullif of off offsets on once only open opendatasource openquery openrowset option or order outer output over percent perm permanent pipe plan precision prepare primary print privileges proc procedure processexit public raiserror read readtext read_only reconfigure recovery references repeatable replication restore restrict return returns revoke right rollback rowcount rowguidcol rule save schema select serializable session_user set setuser shutdown some statistics stats synonym system_user table tape temp temporary textsize then to top tran transaction trigger truncate tsequal uncommitted union unique update updatetext use user values varying view waitfor when where while with work writetext");
            TextArea.SetKeywords(1, "bigint binary bit char character date datetime dec decimal float image int integer money nchar ntext numeric nvarchar real smalldatetime smallint smallmoney sql_variant sysname text timestamp tinyint uniqueidentifier varbinary varchar");
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(0x000000);

            TextArea.AdditionalSelectionTyping = true;
            
        }

        private void txtSql_SelectionChanged(object sender, UpdateUIEventArgs e)
        {
            try
            {
                int line = txtSql.CurrentLine;
                int column = txtSql.GetColumn(txtSql.CurrentPosition);

                tssLaPos.Text = $"{StringComplete(string.Format("Fila: {0}", line+1), 13)} {StringComplete(string.Format("Col: {0}", column+1), 13)}";
            }
            catch
            {
                //Do Nothing
            }
        }

        private void btIndentShow_Click(object sender, EventArgs e)
        {
            if (txtSql.IndentationGuides == IndentView.None)
                txtSql.IndentationGuides = IndentView.LookBoth;
            else
                txtSql.IndentationGuides = IndentView.None;
        }


        private void buscarEnBDToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void btBuscarEnBd_Click(object sender, EventArgs e)
        {
            buscarEnBDToolStripMenuItem_Click(sender, e);
        }

        private void btConnectToBd_Click(object sender, EventArgs e)
        {
            ConnectToDatabase(sender, e);
        }


        private void CutTrailingSpaces()
        {
            StringBuilder strB = new StringBuilder();

            for (int i = 0; i < txtSql.Lines.Count; i++)
            {
                // Get the text of the current line
                string lineText = txtSql.Lines[i].Text;
                // Remove trailing spaces from the line
                strB.AppendLine( lineText.TrimEnd());
            }
            txtSql.Text = strB.ToString();
        }

        private void mostrarEspaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.ViewWhitespace == WhitespaceMode.Invisible)
                txtSql.ViewWhitespace = WhitespaceMode.VisibleAlways;
            else
                txtSql.ViewWhitespace = WhitespaceMode.Invisible;
        }


        private void guiaIndentacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.IndentationGuides == IndentView.None)
                txtSql.IndentationGuides = IndentView.LookBoth;
            else
                txtSql.IndentationGuides = IndentView.None;
        }

        private void eliminarEspaciosFinDeLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutTrailingSpaces();
        }

        private void selecciónAMayúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }

        private void selecciónAMinúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            int outVal;
            int.TryParse(toolStripTextBox1.Text, out outVal);
            txtSql.TabWidth = (outVal ==0 )? 0:outVal;
        }

        private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _findReplace.Show();
        }


        private void txtSql_TextChanged(object sender, EventArgs e)
        {
            if (txtSql.Modified && tssLaStat.Text != "Archivo Modificado...")
                tssLaStat.Text = "Archivo Modificado...";

            if (txtSql.Margins[0].Width == 3)
                return;            

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = txtSql.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            txtSql.Margins[0].Width = txtSql.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void numerosDeLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.Margins[0].Width != 3)
                txtSql.Margins[0].Width = 3;
            else
            {
                const int padding = 2;
                txtSql.Margins[0].Width = txtSql.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            }
        }

        private void FrmSqlCrypt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AlCerrarElFormulario() == DialogResult.Cancel)
                e.Cancel = true;

        }


        private void commentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.SelectedText.Length > 0)
            {
                int f = txtSql.LineFromPosition(txtSql.SelectionStart);
                int t = txtSql.LineFromPosition(txtSql.SelectionEnd);

                for (int i = f; i <= t; i++)
                {
                    txtSql.InsertText(txtSql.Lines[i].Position, "--");
                }
                txtSql.SelectionStart = txtSql.Lines[f].Position;
                txtSql.SelectionEnd = txtSql.Lines[t].EndPosition -1;
            }
        }

        private void uncommentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.SelectedText.Length > 0)
            {
                int f = txtSql.LineFromPosition(txtSql.SelectionStart);
                int t = txtSql.LineFromPosition(txtSql.SelectionEnd);

                for (int i = f; i <= t; i++)
                {
                    string s = txtSql.Lines[i].Text;
                    if (s.TrimStart().StartsWith("--"))
                    {
                        var regex = new Regex(Regex.Escape("--"));
                        var newText = regex.Replace(s, "", 1);
                        int x = txtSql.Lines[i].Position;
                        int y = txtSql.Lines[i].EndPosition;
                        txtSql.SelectionStart = x;
                        txtSql.SelectionEnd = y;
                        txtSql.ReplaceSelection(newText);
                    }
                }
                txtSql.SelectionStart = txtSql.Lines[f].Position;
                txtSql.SelectionEnd = txtSql.Lines[t].EndPosition -1;
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_findReplace.FindNext())
                _findReplace.Show();
        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_findReplace.FindPrevious())
                _findReplace.Show();
        }

        private void indicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención");
                return;
            }

            var frm = new frmIndexes(hSql);
            frm.Show();
        }

        private void tABAEspaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TAB_to_spaces();
        }

        private void TAB_to_spaces()
        {
            string spaces = "";
            try
            {
                int numSpaces = Convert.ToInt32(toolStripTextBox1.Text);
                for (int i = 0; i < numSpaces; ++i)
                    spaces += " ";
            }
            catch
            {
                MessageBox.Show("Por favor ingrese el número de espacios en 'Tab Size'");
                return;
            }
            StringBuilder strB = new StringBuilder();

            for (int i = 0; i < txtSql.Lines.Count; i++)
            {
                // Get the text of the current line
                string lineText = txtSql.Lines[i].Text;
                // Remove trailing spaces from the line
                strB.AppendLine(lineText.TrimEnd().Replace("\t", spaces));
            }
            txtSql.Text = strB.ToString();
        }


        private void btRefreshType_Click(object sender, EventArgs e)
        {
            cbObjetos_SelectedValueChanged(sender, e);
        }


        private void btReconnect_Click(object sender, EventArgs e)
        {
            if (hSql.ConnectionString != "" && !hSql.ConnectionStatus)
            {
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

                LoadDatabaseList();

                tssLaFile.Text = $"{hSql.GetServerName()}/{databasesToolStripMenuItem.Text}";

                Objetos = new DbObjects(hSql);
                Table = new TableDef(hSql);
                Load_cbObjetos();
            }

            if (hSql.ConnectionStatus)
                MessageBox.Show("Re-Conectado");
            else
                MessageBox.Show("No Re-Conectado");

        }
    }
}
