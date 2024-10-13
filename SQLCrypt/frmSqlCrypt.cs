using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SQLCrypt.StructureClasses;
using SQLCrypt.FunctionalClasses.MySql;
using ScintillaNET;
using ScintillaFindReplaceControl;
using SQLCrypt.FunctionalClasses;
using System.Text;
using System.Text.RegularExpressions;
using SQLCrypt.frmUtiles;
using System.IO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Media;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading.Tasks;
using System.ComponentModel;
using static ScintillaNET.Style;
using static SQLCrypt.Program;
using System.Threading;
using qTimer = System.Timers.Timer;
using System.Timers;
using System.Diagnostics.Eventing.Reader;


//TODO: Buscar Errores de Sintáxis en el Documento Actual o Selección ??


namespace SQLCrypt
{
    public partial class FrmSqlCrypt : Form
    {
        private const string EOL = "\r\n";

        Thread threadQuery = null;
        private qTimer queryTimer = new qTimer();
        private Config cfg;

        private DbObjects Objetos;
        private System.Windows.Forms.ToolTip MytoolTip = new System.Windows.Forms.ToolTip();
        public string ConnectionFile = "";

        string CurrentFile = "";
        bool IsEncrypted = false;
        private string Server = "";
        private int TextLimit = 0;

        private string sTabla;
        private TableDef Table;

        //Cuando se buscan Objetos, para que, en el caso
        //De estar en modo Tablas no retorne todas sus
        //ccolumnas cada vez
        private bool EnBusqueda = false;

        private MySql hSql;
        SearchManager FindMan = null;
        FindReplace _findReplace = null;

        ///
        /// Clase para Encapsular el manejo de Scintilla
        ///
        private ScintillaCustom scintillaC;

        public FrmSqlCrypt(MySql hSql, string fileName)
        {
            InitializeComponent();

            cfg = new Config();
            cfg = Config.LoadFromJson();

            cfg.SetFormLocation(this);
            BuildMenuItems();

            scintillaC = new ScintillaCustom(txtSql, "keywords.cfg", "keywords2.cfg");

            QueryController.Prepare();
            
            // ---------------------------------------------
            laDataLoadStatus.Text = "";
            laDataLoadStatus.Visible = false;

            laTablas.Text = "";
            tssLaFile.Text = "";
            tssLaPos.Text = string.Empty;
            tssLaStat.Text = string.Empty;

            txtSql.AllowDrop = true;

            this.hSql = hSql;

            splitC.Panel1Collapsed = true;
            sTabla = string.Empty;

            ContextMenu colm = new ContextMenu();
            colm.MenuItems.Add("Selection to Clipboard", new EventHandler(colmSelectionToClipBoard));
            colm.MenuItems.Add("Name to Clipboard", new EventHandler(colmSelectionNameToClipBoard));

            lsColumnas.ContextMenu = colm;

            ContextMenu txm = new ContextMenu();
            txm.MenuItems.Add("Select All", new EventHandler(txmSelAll));
            txm.MenuItems.Add("Deselect All", new EventHandler(txmDeSelAll));
            txm.MenuItems.Add("-");
            txm.MenuItems.Add("Cut", new EventHandler(txmCut));
            txm.MenuItems.Add("Copy", new EventHandler(txmCopy));
            txm.MenuItems.Add("Paste", new EventHandler(txmPaste));
            txm.MenuItems.Add("-");
            txm.MenuItems.Add("Execut All/Selection", new EventHandler(ejecutarComandoToolStripMenuItem_Click));
            txm.MenuItems.Add("Show/Hide Objects Pannel", new EventHandler(verPanelDeObjetosToolStripMenuItem_Click));
            txtSql.ContextMenu = txm;

            _findReplace = new FindReplace();
            _findReplace.SetTarget(txtSql);
            _findReplace.SetFind(txtSql);

            MytoolTip.SetToolTip(btReconnect, "Reconnect to Database...");
            MytoolTip.SetToolTip(btRefreshType, "Refresh Objects list...");
            MytoolTip.SetToolTip(btConnectToBd, "Connect to a MS SQL Server Database Server");
            MytoolTip.SetToolTip(btCancell, "Cancel current query");
            
            string msg = @"Find Objeto in Objects List pressing [Enter]
You can also use the context menu in the Objects List
to Search Objects by their content";
            
            MytoolTip.SetToolTip(txBuscaEnLista, msg);

            FindMan = new SearchManager();
            FindMan.TextArea = txtSql;

            // Inicialización de Scintilla
            scintillaC.InitSyntaxColoring();
            txtSql_TextChanged(null, null);
            SetAutocompleteMenuItemText();

            if (fileName != "")
                OpenFileInEditor(fileName);

            txtSql.Select();
        }


        private void frmSqlCrypt_Load(object sender, EventArgs e)
        {
            tssLaFile.Text = "";
            tssLaPath.Text = "";
        }



        /// <summary>
        /// SCINTILLA REGION
        /// </summary>

        #region Scintilla



        private void scintilla__SelectionChanged(object sender, UpdateUIEventArgs e)
        {
            scintillaC.BraceHighLight();

            try
            {
                tssLaPos.Text = $"{StringComplete(string.Format("Row: {0}", txtSql.CurrentLine + 1), 13)} {StringComplete(string.Format("Col: {0}", scintillaC.CurrentColumn + 1), 13)}";
            }
            catch
            {
                //Do Nothing
            }
        }


        private void mostrarEspaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.MostrarEspacios();
        }


        private void guiaIndentacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.MostrarGuiaIndentacion();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            int outVal;
            int.TryParse(toolStripTextBox1.Text, out outVal);
            txtSql.TabWidth = (outVal == 0) ? 0 : outVal;
        }

        private void numerosDeLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.MostrarNumerosDeLinea();
        }


        private void commentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.CommentSelection();
        }


        private void uncommentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.UnCommentSelection();
        }



        private void SetAutocompleteMenuItemText()
        {
            autoCompleteToolStripMenuItem.Text = "Auto Complete" + (scintillaC.AutoCompleteEnabled ? " - Disable" : " - Enable");
        }



        private void txtSql_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }


        /// <summary>
        /// "scintilla" DragDrop de Archivos, Tablas y Columnas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_DragDrop(object sender, DragEventArgs e)
        {

            // Drag de Objetos y Columnas
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                var cadena = (string)e.Data.GetData(DataFormats.Text);
                if (cadena.Contains("\n"))
                {
                    var space_num = scintillaC.CurrentMinColumn;
                    var fill = new string(' ', space_num);

                    //Split
                    string[] snippet_spaced = System.Text.RegularExpressions.Regex.Split(cadena, EOL);
                    for (int i = 0; i < snippet_spaced.Length; i++)
                    {
                        snippet_spaced[i] = $"{(i != 0 ? fill : "")}{snippet_spaced[i]}";
                    }

                    cadena = "";
                    for (int i = 0; i < snippet_spaced.Length; i++)
                        cadena += $"{snippet_spaced[i]}{(i == snippet_spaced.Length - 1 ? "" : EOL)}";
                }

                if (txtSql.SelectedText != "")
                    txtSql.ReplaceSelection(cadena);
                else
                {
                    txtSql.InsertText(txtSql.CurrentPosition, cadena);
                    txtSql.CurrentPosition += cadena.Length;
                    txtSql.SelectionStart = txtSql.CurrentPosition;
                }
                txtSql.Focus();
                return;
            }

            // Drag de Archivos
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
                cfg.WorkingDirectory = System.IO.Path.GetDirectoryName(CurrentFile);
                tssLaPath.Text = cfg.WorkingDirectory;

                this.Text = $"SQLCrypt - {CurrentFile}";
                scintillaC.LoadFile(CurrentFile);
                cerrarToolStripMenuItem.Enabled = true;
            }

            this.TopMost = true;
            this.TopMost = false;
            this.BringToFront();
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
        }


        /// <summary>
        /// Grabación del Archivo "scintilla"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Apertura y carga de archivo en "scintilla"
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenFileInEditor(string fileName)
        {
            txtSql.Text = "";

            if (string.Compare(System.IO.Path.GetExtension(fileName).ToLower(), ".sqc", true) == 0)
            {
                CurrentFile = fileName;
                IsEncrypted = true;
                try
                {
                    OpenCryptoFile(CurrentFile);
                    this.Text = $"SQLCrypt - {CurrentFile}";
                }
                catch {
                    scintillaC.LoadFile(CurrentFile);
                    this.Text = CurrentFile;
                    this.Text = $"SQLCrypt - {CurrentFile}";
                }
                grabarComoToolStripMenuItem.Enabled = true;
                cfg.WorkingDirectory = System.IO.Path.GetDirectoryName(CurrentFile);
            }
            else
            {
                CurrentFile = fileName;
                IsEncrypted = false;
                //txtSql.Text = System.IO.File.ReadAllText(CurrentFile);
                scintillaC.LoadFile(CurrentFile);
                this.Text = CurrentFile;
                this.Text = $"SQLCrypt - {CurrentFile}";
                grabarComoToolStripMenuItem.Enabled = true;
                cfg.WorkingDirectory = System.IO.Path.GetDirectoryName(CurrentFile);
            }

            tssLaStat.Text = "Archivo Abierto...";
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
        }



        /// <summary>
        /// Carga snippet seleccionado en la selección actual en "scintilla"
        /// </summary>
        /// <param name="snippet"></param>
        private void Load_Snippet(string snippet)
        {
            var l = txtSql.CurrentLine;
            var space_num = scintillaC.CurrentMinColumn;

            var fill = new string(' ', space_num);
            if (fill == null)
                fill = "";

            //Split
            string[] snippet_spaced = System.Text.RegularExpressions.Regex.Split(snippet, EOL);
            for (int i = 1; i < snippet_spaced.Length; i++)
            {
                snippet_spaced[i] = $"{(i != 0 ? fill : "")}{snippet_spaced[i]}";
            }

            var cadena = "";
            for (int i = 0; i < snippet_spaced.Length; i++)
                cadena += $"{snippet_spaced[i]}{(i == snippet_spaced.Length - 1 ? "" : EOL)}";

            txtSql.ReplaceSelection(cadena);

            //if (snippet.Contains("<"))
            //    MessageBox.Show("Presione [Ctrl][-] o [Ctrl][Tab] para completar el Snippet", "Atención");
        }


        #endregion




        private void ObjectSelectCount(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type != "U" && DBObj.type != "V" && DBObj.type != "S")
            {
                MessageBox.Show("This option is only for Tables and Views", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int Count = DBObj.GetCountRows();
            string TipoObjeto = (DBObj.type.Trim() == "U" ? "Table" : "View");
            MessageBox.Show($"{TipoObjeto} [{DBObj.schema_name}].[{DBObj.name}] has {Count} Rows.", "Rows", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        /// <summary>
        /// Abre archivo .sqc "encriptado"
        /// </summary>
        /// <param name="sFileName"></param>
        private void OpenCryptoFile(string sFileName)
        {
            CurrentFile = sFileName;
            IsEncrypted = true;
            cfg.WorkingDirectory = System.IO.Path.GetDirectoryName(CurrentFile);

            txtSql.Text = hSql.DecryptFiletoString(CurrentFile);

            this.Text = $"SQLCrypt - {CurrentFile}";
            tssLaPath.Text = cfg.WorkingDirectory;
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
                var res = MessageBox.Show($"The Document has been modified.{EOL}¿Do you want to Save befor Quit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                    return res;

                if (res == DialogResult.Yes)
                {
                    if (CurrentFile == "")
                        SaveFileStd(true);
                    else
                        SaveFileStd(false);

                    if (txtSql.Modified)
                        return DialogResult.Cancel;
                }
                else if (res == DialogResult.No)
                    return DialogResult.Yes;
            }

            if (hSql.ConnectionStatus == true)
                hSql.CloseDBConn();

            return DialogResult.Yes;
        }



        //Ejecutar !!! ejecutarComandoToolStripMenuItem_Click
        private void ejecutarComandoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (txtSql.Text.Trim().ToString() == "")
            {
                MessageBox.Show(this, "There is no SQL Code to execute", "Information", MessageBoxButtons.OK);
                return;
            }

            if (hSql.ConnectionStatus == false)
            {
                MessageBox.Show(this, "You should be connected to a Database. ;)", "Attention", MessageBoxButtons.OK);
                return;
            }

            // ========== REVISION DE SINTAXIS ==========
            var sqlcheck = new SQLCheck();
            List<SQLParseError> sqlErrors;

            scintillaC.HighlightErrorClean();

            if (txtSql.SelectedText != "")
                sqlErrors = sqlcheck.RunCheck(txtSql.SelectedText);
            else
                sqlErrors = sqlcheck.RunCheck(txtSql.Text);

            string sErrors = "";
            foreach (var error in sqlErrors)
            {
                sErrors += $"Line: {error.line}  Col.: {error.column} {error.ErrorMessage}{EOL}";
                scintillaC.HighlightError(error.line - 1, error.column);
            }
            if (sErrors != "")
            {
                MessageBox.Show(sErrors, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // ========================================

            ExecuteSqlInNewThread();

        }


        /// <summary>
        /// Ejecución de Query
        /// </summary>
        private void ExecuteSqlInNewThread()
        {
            if (threadQuery != null)
            {
                if (threadQuery.IsAlive)
                {
                    MessageBox.Show("There is an active query running...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show(this, "You should be connected to a Database. ;)", "Attention", MessageBoxButtons.OK);
                return;
            }

            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Not connected :({EOL}{hSql.ErrorString}{EOL}{hSql.Messages}", "Oh No!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string sSqlCommand = "";

            if (txtSql.Text.Trim().ToString() == "")
            {
                MessageBox.Show(this, "There is no SQL Code to execute", "Information", MessageBoxButtons.OK);
                return;
            }

            if (txtSql.SelectedText != "")
                sSqlCommand = txtSql.SelectedText.ToString();
            else
                sSqlCommand = txtSql.Text.ToString();

            MySql.strList param = new MySql.strList();
            Dictionary<string, string> DictParam = null;

            param = hSql.GetParameters(sSqlCommand);

            if (param.Count != 0)
            {
                frmParam fmp = new frmParam();
                fmp.Parametros = param;
                fmp.ShowDialog();
                if (fmp.OutParameters.Count == 0)
                    return;

                DictParam = fmp.OutParameters;
            }

            SetTimer();

            {
                threadQuery = new Thread(() =>
                {
                    QueryController.InQuery = true;
                    QueryController.CancelQuery = false;
                    string connectionString = hSql.ConnectionString;
                    string currentDB = hSql.GetCurrentDatabase();
                    int top = this.Top;
                    int left = this.Left;
                    
                    frmDespliegue Despliegue = new frmDespliegue(connectionString, currentDB, sSqlCommand, DictParam);
                    
                    Despliegue.Top = this.Top;
                    Despliegue.Left = this.Left;
                    Despliegue.Height = this.Height;
                    Despliegue.Width = this.Width;

                    Despliegue.ShowDialog();

                    if (Despliegue.hSql.ConnectionStatus)
                    {
                        string curr_db = Despliegue.hSql.GetCurrentDatabase();
                        if (curr_db != "")
                            QueryController.DataBase = curr_db;

                        Despliegue.hSql.CloseDBConn();
                    }
                    QueryController.hSqlQuery = null;
                    Despliegue.Dispose();
                    System.GC.Collect();
                });
                threadQuery.SetApartmentState(ApartmentState.STA);
                threadQuery.Priority = ThreadPriority.Highest;
                threadQuery.Start();
            }

        }


        /// <summary>
        /// Timer para Indicar Consulta en Ejecución
        /// </summary>
        private void SetTimer()
        {
            laDataLoadStatus.Text = "";
            laDataLoadStatus.Visible = false;

            queryTimer = new qTimer(300);
            queryTimer.SynchronizingObject = this;
            queryTimer.Elapsed += this.OnQueryTimeEvent;
            queryTimer.AutoReset = true;
            queryTimer.Enabled = true;
            queryTimer.Start();
        }


        /// <summary>
        /// Finaliza el Timer de Consulta en Ejecución
        /// </summary>
        private void DisposeTimer()
        {
            if ( (QueryController.sql_spid != 0 || QueryController.hSqlQuery != null) )
                return;

            try
            {
                queryTimer.Stop();
                queryTimer.Dispose();
                pgBarQuery.Value = 0;
            }
            catch { }

            laDataLoadStatus.Text = "";
            laDataLoadStatus.Visible = false;

            if (QueryController.DataBase != "")
            {
                if (hSql.ConnectionStatus )
                    hSql.SetDatabase(QueryController.DataBase);
                QueryController.DataBase = "";
            }
            threadQuery = null;
            QueryController.Prepare();
            if (hSql.ConnectionStatus)
                LoadDatabaseList();
        }


        /// <summary>
        /// Evento del Timer de Consulta en Ejecución
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnQueryTimeEvent(Object source, ElapsedEventArgs e)
        {

            if (QueryController.sql_spid != 0 || (QueryController.sql_spid == 0 && QueryController.InQuery))
            {
                laDataLoadStatus.Visible=false;
                if (pgBarQuery.Value < pgBarQuery.Maximum)
                    pgBarQuery.Value += 10;
                else
                    pgBarQuery.Value = 0;
            }
            
            if (QueryController.sql_spid == 0 && QueryController.hSqlQuery != null)
            {
                pgBarQuery.Value = pgBarQuery.Maximum;

                if (laDataLoadStatus.Text == "")
                {
                    laDataLoadStatus.Text = "Loading Data...";
                    laDataLoadStatus.Visible = true;
                }
                else
                {
                    laDataLoadStatus.Text = "";
                    laDataLoadStatus.Visible = false;
                }
                laDataLoadStatus.Refresh();
            }

            if (QueryController.sql_spid == 0 && QueryController.hSqlQuery == null && !QueryController.InQuery)
            {
                DisposeTimer();
            }

        }


        /// <summary>
        /// DEPRECADO, POR ELIMINAR !!!!
        /// </summary>
        private void ExecuteSQLCommand()
        {

            //if (chkToText.Checked)
            //{
            //    frmDespliegueTxt frm = new frmDespliegueTxt(sql);
            //    frm.Text = $"Resultados : {databasesToolStripMenuItem.Text}";
            //    frm.Show();

            //    frm.Top = this.Top;
            //    frm.Left = this.Left;

            //    if (txtSql.SelectedText != "")
            //        frm.ExecuteSQLStatement(txtSql.SelectedText.ToString(), TextLimit);
            //    else
            //        frm.ExecuteSQLStatement(txtSql.Text.ToString(), TextLimit);

            //    LoadDatabaseList();

            //    return;
            //}

        }


        
        /// <summary>
        /// Cierre del Documento en curso.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.Modified)
            {
                var res = MessageBox.Show($"The Document has been modified.{EOL}¿Do you want to Save befor close the file?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (CurrentFile == "")
                        SaveFileStd(true);
                    else
                        SaveFileStd(false);
                }
            }

            CurrentFile = "";
            IsEncrypted = false;
            txtSql.Text = "";
            this.Text = "SQLCrypt";
            tssLaStat.Text = "File Closed...";
            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
        }


        /// <summary>
        /// Grabar Archivo Como...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grabarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileStd(true);
        }


        /// <summary>
        /// Abrir Archivo en Editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSql.Text = "";

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = cfg.WorkingDirectory;
                ofd.Filter = "Sql Files (*.sql;*.sqc)|*.sql;*.sqc|Text Files (*.txt)|*.txt|Config Files (*.cfg)|*.cfg|Any File (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.FileName = CurrentFile;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenFileInEditor(ofd.FileName);
                    CurrentFile = ofd.FileName;
                    cfg.AddOpenedFile(CurrentFile);
                    BuildMenuItems();
                    cfg.WorkingDirectory = System.IO.Path.GetDirectoryName(CurrentFile);
                    this.Text = $"SQLCrypt - {CurrentFile}";
                    tssLaPath.Text = cfg.WorkingDirectory;
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
        }


        /// <summary>
        /// Conteo de Parámetros, para Ejecución de "CommonTasks" parametrizados
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Completar Strings, usado para operaciones de Snippets (Indentación automática)
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0) dif = sValue.Length;

            return sValue + new String(' ', dif);
        }


        /// <summary>
        /// Establecer Conexión a la BD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectToDatabase(object sender, EventArgs e)
        {
            if (hSql.ConnectionStatus)  hSql.CloseDBConn();

            frmConexion frmC = new frmConexion();
            frmC.ShowDialog();

            if (frmC.ConnectionString == string.Empty)
            {
                splitC.Panel1Collapsed = true;
                sTabla = string.Empty;
                lstObjetos.Items.Clear();
                lsColumnas.Items.Clear();
                databasesToolStripMenuItem.Items.Clear();
                databasesToolStripMenuItem.Text = string.Empty;
                return;
            }

            hSql.ConnectionString = frmC.ConnectionString;

            hSql.ErrorClear();
            hSql.CloseDBConn();
            databasesToolStripMenuItem.Items.Clear();
            hSql.ConnectToDB();

            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error connecting to Database");
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


        /// <summary>
        /// Carga Lista de Bases de Dato en Combo
        /// </summary>
        public void LoadDatabaseList()
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


        /// <summary>
        /// Cambio (Selección) de la Base de Datos de Trabajo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void databasesToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!hSql.ConnectionStatus)
                return;

            if (databasesToolStripMenuItem.SelectedIndex == -1)
                return;

            if (!hSql.SetDatabase(databasesToolStripMenuItem.Text))
            {
                MessageBox.Show(hSql.ErrorString, $"Error Opening Database{EOL}{hSql.ErrorString}");
                hSql.ErrorClear();
                databasesToolStripMenuItem.Text = hSql.GetCurrentDatabase();
                return;
            }

            tssLaFile.Text = $"{this.Server} / {databasesToolStripMenuItem.Text}";

            if (splitC.Panel1Collapsed == false)
                cbObjetosSelect("U");
            
        }


        /// <summary>
        /// Abre el diálogo de Encriptación de Claves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encriptarClavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPassWord frmPass = new frmPassWord();
            frmPass.Show();
        }


        /// <summary>
        /// btConsultas_Click
        /// Consultas predefinidas de ejecución rápida (CommonTasks)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConsultas_Click(object sender, EventArgs e)
        {
            hSql.ErrorClear();

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("You must be connected to a Database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (param.Count != 0)
            {
                frmParam fmp = new frmParam();
                fmp.Parametros = param;
                fmp.ShowDialog();
                if (fmp.OutParameters.Count == 0)
                    return;

                DictParam = fmp.OutParameters;
            }

            if (string.IsNullOrEmpty(sSqlCommand) || string.IsNullOrWhiteSpace(sSqlCommand))
            {
                MessageBox.Show("Empty Command", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (param.Count != 0)
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
                frm.Top = this.Top;
                frm.Left = this.Left;
                frm.Width = this.Width;
                frm.Height = this.Height;

                frm.Show(this);
                frm.ExecuteSQLStatement(sSqlCommand, TextLimit);
            }
            else
            {
                hSql.ExecuteSqlData(sSqlCommand);

                if (hSql.Data == null)
                    if (hSql.ErrorExiste)
                    {
                        MessageBox.Show(this, $"SQL Error{EOL}{hSql.ErrorString}", "Attention", MessageBoxButtons.OK);
                        hSql.ErrorClear();
                        return;
                    }

                frmDespliegue Despliegue = new frmDespliegue(hSql);
                Despliegue.Top = this.Top;
                Despliegue.Left = this.Left;
                Despliegue.Width = this.Width;
                Despliegue.Height = this.Height;

                Despliegue.Show();
            }

            databasesToolStripMenuItem_SelectedIndexChanged(null, null);
        }


        /// <summary>
        /// Diálogo de Comandos Imnmediatos (CommonTasks)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comandosInmediatosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btConsultas_Click(null, null);
        }


        /// <summary>
        /// Consultas rápidas (CommonTasks)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Grabación del Archivo en Edición
        /// </summary>
        /// <param name="bSaveAs"></param>
        private void SaveFileStd(bool bSaveAs)
        {

            if (CurrentFile == "" || bSaveAs)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.InitialDirectory = cfg.WorkingDirectory;
                    sfd.Filter = "Sql Crypt Files (*.sqc)|*.sqc";
                    sfd.Filter = "Sql Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|Sql Crypt Files (*.sqc)|*.sqc";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (sfd.FilterIndex == 1 || sfd.FilterIndex == 2)
                            IsEncrypted = false;
                        else
                            IsEncrypted = true;

                        CurrentFile = sfd.FileName;
                        cfg.AddOpenedFile(CurrentFile);
                        cfg.WorkingDirectory = System.IO.Path.GetDirectoryName(CurrentFile);
                        this.Text = $"SQLCrypt - {CurrentFile}";
                        tssLaPath.Text = cfg.WorkingDirectory;
                        this.Text = "SQLCrypt - " + CurrentFile;
                    }
                    else
                        return;
                }
            }

            if (IsEncrypted)
            {
                hSql.EncryptStringtoFile(txtSql.Text, CurrentFile);
            }
            else
            {
                scintillaC.SaveFile(CurrentFile);
            }

            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
            tssLaStat.Text = "Archivo Grabado...";

        }


        /// <summary>
        /// Salida a Texto o Grilla (Deprecado)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salidaATextoGrillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkToText.Checked =  chkToText.Checked ? false : true ;
        }


        /// <summary>
        /// Carga Objetos en la Lista de Objetos (Tablas, Funciones, Vistas, etc..)
        /// </summary>
        /// <param name="type"></param>
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

            laTablas.Text = $"Objetos: {lstObjetos.Items.Count}";
        }


        /// <summary>
        /// Busca los Objetos que contienen el Texto ingresado en txBuscaEnLista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                
                lsColumnas.Items.Clear();

                if (lstObjetos.SelectedIndices.Count > 0)
                {
                    lstObjetos.TopIndex = lstObjetos.SelectedIndices[0];
                    if (lstObjetos.SelectedIndices.Count == 1)
                    {
                        lstObjetos_SelectedIndexChanged(null, null);
                    }
                }
                else
                    lstObjetos.TopIndex = 0;

            }
        }


        /// <summary>
        /// Menú contextual para Tablas
        /// </summary>
        private void SetMenuTablas()
        {
            lstObjetos.ContextMenu = null;
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add("Get More Info", new EventHandler(ObjGetMoreInfo));
            //cm.MenuItems.Add("Get CREATE TABLE", new EventHandler(ObjGetCreateTable));
            cm.MenuItems.Add("Get Text", new EventHandler(ObjGetText));
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ObjSelectedToClipboard));

            string Type = ((ObjectType)cbObjetos.SelectedItem).type.Trim();
            if (Type == "U" || Type == "V" || Type == "S")
            {
                cm.MenuItems.Add("Select COUNT(*) FROM ", new EventHandler(ObjectSelectCount));
                cm.MenuItems.Add("Select TOP(100) * FROM ", new EventHandler(ObjectSelectStar));
            }
            if (Type == "U")
            {
                cm.MenuItems.Add("Edit Data", new EventHandler(EditarDatos));
                cm.MenuItems.Add("Get Indexes", new EventHandler(GetIndexes));
            }

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


        /// <summary>
        /// Busca Todas las tablas que tienen el la Columna indicada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindTableByColumnName(object sender, EventArgs e)
        {
            if (txBuscaEnLista.Text == "")
            {
                MessageBox.Show("What are you looking for? Set it in 'Find Procs.'");
            }

            string Type = ((ObjectType)cbObjetos.SelectedItem).type.Trim();
            Objetos.FindByColumn(Type, txBuscaEnLista.Text);
            
            if (Objetos.Count == 0)
            {
                MessageBox.Show("No matchess found :(");
            }

            Load_lstObjetos_ProcFiltered();
        }


        /// <summary>
        /// Toggle de Panel de Objetos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verPanelDeObjetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(hSql.ConnectionStatus == false)
            {
                splitC.Panel1Collapsed = true;
                sTabla = string.Empty;
                MessageBox.Show("You must connect to a Database...", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (splitC.Panel1Collapsed)
            {
                splitC.Panel1Collapsed = false;
                laBuscarTablas.Text = "Search...";
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


        /// <summary>
        /// Objetos seleccionados al Clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjSelectedToClipboard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            foreach (var item in lstObjetos.SelectedItems)
            {
                Elementos += (Elementos != "" ? EOL : "") + lstObjetos.GetItemText(item);
            }
            
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("There are no selected elements", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        /// <summary>
        /// Copiar al Clipboard Fila Completa de las Columnas Seleccionadas del Objeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colmSelectionToClipBoard( object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            
            for(int x = 0; x < lsColumnas.Items.Count; ++x)
            {
                if (lsColumnas.Items[x].Selected)
                    Elementos += (Elementos != "" ? EOL : "") + $"{lsColumnas.Items[x].Text} {lsColumnas.Items[x].SubItems[1].Text} {lsColumnas.Items[x].SubItems[2].Text}";
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("There are no selected Elements", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        /// <summary>
        /// Copiar al Clipboard Solo los NOMBRES de las Columnas del Objeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colmSelectionNameToClipBoard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";

            for (int x = 0; x < lsColumnas.Items.Count; ++x)
            {
                if (lsColumnas.Items[x].Selected)
                    Elementos += (Elementos != "" ? EOL : "") + $"{lsColumnas.Items[x].Text}";
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("There are no selected Elements", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        /// <summary>
        /// Select Top 100 de Vista o Tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectSelectStar(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type != "U" && DBObj.type != "V" && DBObj.type != "S")
            {
                MessageBox.Show("This options is only for Tables and Views", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 

            string sAux = DBObj.GetData(true);

            frmDespliegue Despliegue = new frmDespliegue(hSql);
            Despliegue.Top = this.Top;
            Despliegue.Left = this.Left;
            Despliegue.Width = this.Width;
            Despliegue.Height = this.Height;

            Despliegue.Text = sAux;
            Despliegue.Show();
        }


        /// <summary>
        /// Ayudante de Creación - Borrado de Índices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetIndexes(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("This option is only for Tables", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new frmIndexes(hSql, lstObjetos.Text);
            frm.Show();
        }


        /// <summary>
        /// Editar Datos en Tabla de Forma Gráfica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditarDatos(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);
            DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() != "U")
            {
                MessageBox.Show("This option is only for Tables", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDataEdit DataEdit = new frmDataEdit(hSql, lstObjetos.Text);
            DataEdit.Show();

        }


        /// <summary>
        /// Información adicional del Objeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjGetMoreInfo(object sender, EventArgs e)
        {

            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);

            DBObj = (DBObject)lstObjetos.SelectedItem;

            StringBuilder sb = new StringBuilder();

            sb.Append($"{EOL}Schema    : {DBObj.schema_name}{EOL}");
            sb.Append($"Nombre    : {DBObj.name}{EOL}");
            sb.Append($"Id        : {DBObj.object_id}{EOL}");
            sb.Append($"Type      : {DBObj.type}{EOL}")    ;
            sb.Append($"Type Desc : {DBObj.type_desc}{EOL}")   ;
            sb.Append($"Creado    : {DBObj.create_date}{EOL}");
            sb.Append($"Modificado: {DBObj.modify_date}{EOL}");
            sb.Append($"Schema Id : {DBObj.schema_id}{EOL}");
            sb.Append($"Parrent Id: {DBObj.parent_object_id}{EOL}");

            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());

            MessageBox.Show("Información is in the Clipboard", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        /// <summary>
        /// Cambio en la Selección de Objetos (Lista)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                (string sColName, string sColDataType) = t.GetTranslated();

                ListViewItem Item = new ListViewItem(sColName);
                Item.SubItems.Add(sColDataType);
                Item.SubItems.Add(t.Nullable? "X": "");
                Item.SubItems.Add(t.IsIdentity? "X": "");
                Item.SubItems.Add(t.IsPrimaryKey? "X": "");
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
                MessageBox.Show("Option not applicable, the object is a Stored Procedure...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sSalida = string.Empty;

            sSalida = DBObj.ObjGetCreateTable();

            Clipboard.Clear();
            Clipboard.SetText(sSalida);

            MessageBox.Show("Information is in theh Clipboard", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Define el menú contextual de Objetos Para los Procedimientos Almacenados
        /// </summary>
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


        /// <summary>
        /// Busca Los procedimientos que contienen un Texto determinado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindProcedureByText(object sender, EventArgs e)
        {
            if (txBuscaEnLista.Text == "")
            {
                MessageBox.Show("Set the text to search for in 'Find...'");
            }
            
            Objetos.FindByText(((ObjectType)cbObjetos.SelectedItem).type, txBuscaEnLista.Text);
            if (Objetos.Count == 0)
            {
                MessageBox.Show("There are no coincidences", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Load_lstObjetos_ProcFiltered();
        }


        /// <summary>
        /// Carga la Lista de Objetos Filtrada (por búsqueda)
        /// </summary>
        private void Load_lstObjetos_ProcFiltered()
        {
            lstObjetos.Items.Clear();
            MytoolTip.SetToolTip(lstObjetos, "");

            foreach (var n in Objetos)
            {
                lstObjetos.Items.Add(n);
            }

            laTablas.Text = $"Objects: {lstObjetos.Items.Count}";

        }


        /// <summary>
        /// Obtiene el Texto de un Objeto de BD (Vistas, Procs, Funciones, Triggers)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Diálogo de Búsqueda de Página 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buscaPaginaSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hSql.ErrorClear();

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("You must be connected to a Database ;)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmBuscaPagina fBusPag = new frmBuscaPagina();
            fBusPag.ShowDialog();

            if (fBusPag.Cancelado)
                return;

            string sAux = hSql.BuscaPagina(fBusPag.BaseId, fBusPag.FileId, fBusPag.PageId);
            txtSql.InsertText(txtSql.CurrentPosition, sAux);
        }


        /// <summary>
        /// Evento MouseDown lstObjetos para DragDrop y Copiado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstObjetos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lstObjetos.ContextMenu.Show(this, new Point(e.X, e.Y));
                return;
            }

            if (lstObjetos.SelectedItems.Count > 0)
            {
                string Elementos = "";
                for (int i = 0; i < lstObjetos.SelectedItems.Count; ++i)
                {
                    Elementos += (Elementos != "" ? EOL : "") + lstObjetos.SelectedItems[i].ToString();
                }
                if (Elementos != "")
                    txtSql.DoDragDrop(Elementos, DragDropEffects.Copy);

                lstObjetos_SelectedIndexChanged(sender, e);
            }
        }


        /// <summary>
        /// Evento MouseDown lsColumnas (Selección para DragDrop y Copiado)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsColumnas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lsColumnas.ContextMenu.Show(this, new Point( panColumnas.Left + e.X, panColumnas.Left + lstObjetos.Height + 80 + e.Y));
                return;
            }

            if (lsColumnas.SelectedItems.Count > 0)
            {
                string Elementos = "";

                for (int x = 0; x < lsColumnas.Items.Count; ++x)
                {
                    if (lsColumnas.Items[x].Selected)
                        Elementos += (Elementos != "" ? EOL : "") + $"{lsColumnas.Items[x].Text}";
                }
                if (Elementos != "")
                    txtSql.DoDragDrop(Elementos, DragDropEffects.Copy);
            }
        }


        /// <summary>
        /// Carga el ComboBox de Tipos de Objetos (Bajo la Lista de Objetos).
        /// </summary>
        private void Load_cbObjetos()
        {
            cbObjetos.Items.Clear();

            ObjectTypes objt = new ObjectTypes();

            foreach (var n in objt)
            {
                cbObjetos.Items.Add(n);
            }
        }


        /// <summary>
        /// Selección del Tipo de Objetos a Desplegar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbObjetos_SelectedValueChanged(object sender, EventArgs e)
        {
            Load_lstObjetos( ((ObjectType)cbObjetos.SelectedItem).type );
            SetMenuTablas();
        }


        /// <summary>
        /// Carga el Tipo de Objetos Seleccionado en la Lista de Objetos
        /// </summary>
        /// <param name="Tipo"></param>
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


        /// <summary>
        /// Diálogo de Edición de las Propiedades Extendidas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void extendedPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Must be connected to a Database...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmExtendedProperties frmC = new frmExtendedProperties(hSql);
            frmC.Top = this.Top;
            frmC.Left = this.Left;
            frmC.Show();

        }


        /// <summary>
        /// Conexión a BD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConnectToBd_Click(object sender, EventArgs e)
        {
            ConnectToDatabase(sender, e);
        }


        /// <summary>
        /// Editor, Eliminar Espacios sobrantes al fin de línea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eliminarEspaciosFinDeLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.CutTrailingSpaces();
        }


        /// <summary>
        /// Abrir el Diálogo de Find & Replace 
        /// Es peersistente Sólo se invisibiliza para que F3 siga funcionando aún ccerrado el Diálogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _findReplace.Show();
        }

        
        /// <summary>
        /// Al Cerrar el presente Form (Principal)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSqlCrypt_FormClosing(object sender, FormClosingEventArgs e)
        {

            //Guardo el último Estado de la App
            cfg.GetFormLocation(this);
            cfg.SaveToJson();

            if (AlCerrarElFormulario() == DialogResult.Cancel)
                e.Cancel = true;

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


        /// <summary>
        /// Diálogo del Helper de Índices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void indicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show("Must be connected to a Database...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new frmIndexes(hSql);
            frm.Show();
        }


        /// <summary>
        /// Edición, Convertir los Tabs a Espacios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tABAEspaciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int numSpaces = Convert.ToInt32(toolStripTextBox1.Text);
                scintillaC.TABtoSpaces(numSpaces);
            }
            catch
            {
                MessageBox.Show("Please enter the number of spaces in 'Tab Size' field", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            
        }

        
        /// <summary>
        /// Refrescar La Lista de Objetos cuando ha sido Filtrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefreshType_Click(object sender, EventArgs e)
        {
            cbObjetos_SelectedValueChanged(sender, e);
        }


        /// <summary>
        /// Botón de Re-Conexión (En caso Necesarios, evita tener que reconectar con el Diálogo de Conexión)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    MessageBox.Show($"Error connecting to Database ! {EOL} {hSql.ErrorString}", "Oh Nooooo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("Re-Connected");
            else
                MessageBox.Show("No Re-Connected");
        }


        /// <summary>
        /// Habilita-Deshabilita Autocompletar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.AutoCompleteEnabled = !scintillaC.AutoCompleteEnabled;
            SetAutocompleteMenuItemText();
        }


        private void txtSql_SelectionChanged(object sender, UpdateUIEventArgs e)
        {
            scintilla__SelectionChanged(sender, e);
        }


        /// <summary>
        /// Mostrar Guias de Indentación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btIndentShow_Click(object sender, EventArgs e)
        {
            //scintilla__IndentationGuides();
            scintillaC.MostrarGuiaIndentacion();
        }

        
        private void txtSql_TextChanged(object sender, EventArgs e)
        {
            textChanged();
        }



        /// <summary>
        /// Llamado por el evento de Scintilla
        /// </summary>
        /// <param name="tssLaStat"></param>
        /// <param name="_maxLineNumberCharLength"></param>
        public void textChanged()
        {
            if (txtSql.Modified && tssLaStat.Text != "File Modified...")
                tssLaStat.Text = "File Modified...";

            if (txtSql.Margins[0].Width == 3)
                return;

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = txtSql.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == scintillaC._maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            txtSql.Margins[0].Width = txtSql.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            scintillaC._maxLineNumberCharLength = maxLineNumberCharLength;

        }


        /// <summary>
        /// Diálogo de Snippets (Simula Menú)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmSnippets();
            try
            {
                frm.Parent = Parent;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
                var snippet = frm.snippet;
                Load_Snippet(snippet);
            }
            catch { }
        }


        /// <summary>
        /// Ir al Bookmark Previo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.PreviousBookmark();
        }


        /// <summary>
        /// MArca Desmarca Bookmark en la Línea Actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.ToogleBookmark();
        }


        /// <summary>
        /// Ir al Siguiente Bookmark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goToNextBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.NextBookmark();
        }


        /// <summary>
        /// Completar Snippet busca <expansión>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void completarSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //complete_snippet();
            scintillaC.complete_snippet();
        }

  
        /// <summary>
        /// Pegado de Objetos en el Editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstObjetos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (lstObjetos.SelectedIndex != -1)
                txtSql.ReplaceSelection(lstObjetos.Items[lstObjetos.SelectedIndex].ToString());
            txtSql.Select();
        }


        /// <summary>
        /// Pegado de Columnas en el Editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsColumnas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (lsColumnas.SelectedIndices.Count == 0) return;

            string Elementos = "";
            var space_num = scintillaC.CurrentMinColumn;
            var fill = new string(' ', space_num-1 < 0? 0: space_num-1);

            for (int x = 0; x < lsColumnas.Items.Count; ++x)
            {
                if (lsColumnas.Items[x].Selected)
                    Elementos += (Elementos != "" ? $"{EOL}{fill}," : "") + $"{lsColumnas.Items[x].Text}";
            }
            txtSql.ReplaceSelection(Elementos);
            txtSql.Select();
        }


        /// <summary>
        /// Cancelación de Consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancell_Click(object sender, EventArgs e)
        {
            if (QueryController.sql_spid == 0 && QueryController.hSqlQuery != null && !QueryController.InQuery)
            {
                QueryController.CancelQuery = true;
                MessageBox.Show("Query ended, data is beeing loaded, cannot Cancel...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (QueryController.InQuery)
            {
                QueryController.CancelQuery = true;
                return;
            }

            if (QueryController.sql_spid != 0)
            {
                int mySpid = hSql.GetCurrent_SPID();
                hSql.Kill_SPID(QueryController.sql_spid);
                QueryController.CancelQuery = true;
                if (hSql.ErrorExiste || hSql.Messages != "")
                {
                    MessageBox.Show($"SQL Connection with Error: {EOL}{hSql.ErrorString}{EOL}{EOL}{hSql.Messages}", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DisposeTimer();
                MessageBox.Show($"There is no running Query", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (threadQuery != null)
                if (!threadQuery.IsAlive)
                    threadQuery = null;

            System.GC.Collect();
            
        }


        /// <summary>
        /// formatSQLCodeToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formatSQLCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            var SQLFormatter = new SQLFormatter();
            List<SQLParseError> sqlErrors;

            if (txtSql.SelectedText == "")
                sqlErrors = SQLFormatter.Format_TSQL(txtSql.Text);
            else
                sqlErrors = SQLFormatter.Format_TSQL(txtSql.SelectedText);
            
            if (sqlErrors.Count > 0)
            {
                scintillaC.HighlightErrorClean();
                
                string sErrors = "";
                foreach (var error in sqlErrors)
                {
                    sErrors += $"Line: {error.line}  Col.: {error.column} {error.ErrorMessage}{EOL}";
                    scintillaC.HighlightError(error.line - 1, error.column);
                }
                MessageBox.Show(sErrors, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (txtSql.SelectedText == "")
                    txtSql.Text = SQLFormatter.FormattedString;
                else
                    txtSql.ReplaceSelection(SQLFormatter.FormattedString);
            }

        }


        /// <summary>
        /// checkSQLCodeToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkSQLCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sqlcheck = new SQLCheck();
            List<SQLParseError> sqlErrors;

            scintillaC.HighlightErrorClean();

            if (txtSql.SelectedText != "")
                sqlErrors = sqlcheck.RunCheck(txtSql.SelectedText);
            else
                sqlErrors = sqlcheck.RunCheck(txtSql.Text);

            string sErrors = "";
            foreach (var error in sqlErrors)
            {
                sErrors += $"Line: {error.line}  Col.: {error.column} {error.ErrorMessage}{EOL}";
                scintillaC.HighlightError(error.line-1, error.column);
            }
            if (sErrors != "")
                MessageBox.Show(sErrors, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                scintillaC.HighlightErrorClean();
                MessageBox.Show("Sintaxis is OK", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// RemoveMultiSpaces
        /// Wipe spaces until next no-space character or EOL
        /// </summary>
        private void RemoveMultiSpaces()
        {
            int pos_ini = txtSql.SelectionStart;
            int len = 0;

            while (txtSql.Text.Substring(pos_ini + len, 1) == " ")
                len++;

            txtSql.SelectionEnd = pos_ini + len;
            txtSql.ReplaceSelection("");
            txtSql.SelectionStart = pos_ini;
            txtSql.SelectionEnd = pos_ini;
            
        }



        /// <summary>
        /// splitCommasToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitCommasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scintillaC.splitByComma();
        }



        /// <summary>
        /// removeMultiSpacesToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeMultiSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveMultiSpaces();
        }



        /// <summary>
        /// BuildMenuItems
        /// Agrega Elementos al ToolStripMenuItem "Recent Files" (lastOpenedFilesToolStripMenuItem)
        /// </summary>
        private void BuildMenuItems()
        {
            if (cfg.LastOpenedFiles.Count == 0)
                return;

            ToolStripMenuItem[] items = new ToolStripMenuItem[cfg.LastOpenedFiles.Count]; // You would obviously calculate this value at runtime
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Name = i.ToString();
                items[i].Tag = "specialDataHere";
                items[i].Text = cfg.LastOpenedFiles[i];
                items[i].Click += new EventHandler(MenuItemClickHandler);
            }

            lastOpenedFilesToolStripMenuItem.DropDownItems.Clear();
            lastOpenedFilesToolStripMenuItem.DropDownItems.AddRange(items);
        }



        /// <summary>
        /// Handler para la carga de "Recent Files"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            string fileName = clickedItem.Text;

            if (fileName == "(empty)")
                return;

            if (File.Exists(fileName))
            {
                OpenFileInEditor(fileName);
            }
            else
            {
                cfg.LastOpenedFiles.Remove(fileName);
            }
        }

    }
}
