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
using System.Timers;

//TODO: Parsear scripts respetando GO
//TODO: Buscar Errores de Sintáxis en el Documento Actual o Selección ??


namespace SQLCrypt
{
    public partial class FrmSqlCrypt : Form
    {
        Thread threadQuery = null;
        private System.Timers.Timer queryTimer;

        private DbObjects Objetos;
        private System.Windows.Forms.ToolTip MytoolTip = new System.Windows.Forms.ToolTip();
        public string ConnectionFile = "";

        string CurrentFile = "";
        bool IsEncrypted = false;
        public string WorkPath = "";
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
        private int maxLineNumberCharLength;
        private int lastCaretPos = 0;
        private bool autoCompleteEnabled = true;

        private bool scintilla_end_mode = false;
        private const int BOOKMARK_MARGIN = 1; // Conventionally the symbol margin
        private const int BOOKMARK_MARKER = 3; // Arbitrary. Any valid index would work.

        private bool omit_key = false;

        private string keyWords = "add all alter and any as asc avg" +
                                  "backup begin between break browse bulk by " +
                                  "cascade case check checkpoint close clustered coalesce collate column commit constraint continue convert count create cross current " +
                                  "current_timestamp current_user cursor " +
                                  "database datediff datepart dbcc deallocate declare default delete deny desc disable disk distinct distributed double drop dummy dump " +
                                  "else enable end errlvl errorexit escape except exec execute exists exit " +
                                  "fetch file fillfactor for foreign forward_only freetext from full function " +
                                  "getdate go grant group " +
                                  "having holdlock " +
                                  "identity identity_insert identitycol if in index inner insert instead intersect into is isnull isolation " +
                                  "join " +
                                  "key kill " +
                                  "left level like load " +
                                  "move " +
                                  "no nocheck nocount nonclustered norecovery not null nullif " +
                                  "object_id of off offsets on once only open opendatasource openquery openrowset option or order outer output over " +
                                  "percent perm prepare primary print proc procedure public " +
                                  "raiserror read readtext read_only reconfigure recovery references repeatable replication restore restrict return returns revoke right " +
                                  "rollback rowcount rowguidcol rule " +
                                  "save schema select serializable session_user set setuser shutdown some statistics stats sum synonym system_user " +
                                  "table tape temp then to top tran transaction trigger truncate " +
                                  "uncommitted union unique update updatetext use user " +
                                  "values view " +
                                  "waitfor when where while with work writetext";

        private string keyWords2 = "bigint binary max bit char character date datetime dec decimal float image int integer money nchar ntext numeric nvarchar " +
                                   "real smalldatetime smallint smallmoney sql_variant sysname text timestamp tinyint uniqueidentifier varbinary varchar";

        private string autoCompleteKeywords = "";


        public FrmSqlCrypt(MySql hSql, string fileName)
        {
            InitializeComponent();

            // =======   KEYWORDS Y AUTOCOMPLETAR   ========
            var keywords_file = "keywords.cfg";
            if (File.Exists(keywords_file))
                keyWords = File.ReadAllText(keywords_file);

            keyWords = keyWords.Replace("\r\n", " ");

            var keywords2_file = "keywords2.cfg";
            if (File.Exists(keywords2_file))
                keyWords2 = File.ReadAllText(keywords2_file);

            keyWords2 = keyWords2.Replace("\r\n", " ");

            autoCompleteKeywords = keyWords.ToUpper();
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

            MytoolTip.SetToolTip(btReconnect, "Reconectarse a la Base de Datos...");
            MytoolTip.SetToolTip(btRefreshType, "Refrescar la lista de objetos...");
            MytoolTip.SetToolTip(btConnectToBd, "Conectarse a un Servidor de Base de Datos");
            MytoolTip.SetToolTip(btCancell, "Cancelar una consulta en ejecución");
            
            string msg = @"Busca objeto en lista de Objetos presionando [Enter]
También se usa con el menú contextual de la Lista de Objetos
Para buscar por contenido";
            
            MytoolTip.SetToolTip(txBuscaEnLista, msg);
            

            FindMan = new SearchManager();
            FindMan.TextArea = txtSql;
            InitSyntaxColoring(txtSql);

            SetAutocompleteMenuItemText();

            if (fileName != "")
                OpenFileInEditor(fileName);

            txtSql.Select();
        }


        /// <summary>
        /// SCINTILLA REGION
        /// </summary>

        #region Scintilla

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private static bool IsBrace(int c)
        {
            switch (c)
            {
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case '<':
                case '>':
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Seteo del control "scintilla"
        /// con todas las opciones de color y otras, esperadas para el control
        /// </summary>
        /// <param name="TextArea"></param>
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

            //TextArea.SetSelectionBackColor(true, IntToColor(0x000099));
            TextArea.SetSelectionBackColor(true, IntToColor(0x004389));
            TextArea.StyleClearAll();

            //Resaltado de Parentesis (Braces)
            TextArea.Styles[Style.BraceBad].ForeColor = IntToColor(0xFFFFFF);
            TextArea.Styles[Style.BraceLight].ForeColor = IntToColor(0xFF00AA);

            TextArea.Styles[Style.Sql.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Sql.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Sql.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Sql.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Sql.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Sql.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Sql.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Sql.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Sql.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Sql.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Sql.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Sql.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Sql.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);

            TextArea.Lexer = Lexer.Sql;

            TextArea.SetKeywords(0, keyWords);
            TextArea.SetKeywords(1, keyWords2);

            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(0xAFAFAF);
            TextArea.Styles[Style.LineNumber].BackColor = IntToColor(0x211021);

            TextArea.AdditionalSelectionTyping = true;
            TextArea.MultipleSelection = true;
            TextArea.MouseSelectionRectangularSwitch = true;
            TextArea.VirtualSpaceOptions = VirtualSpace.RectangularSelection;

            //Barra de Book-Marks
            TextArea.Margins[BOOKMARK_MARGIN].Width = 16;
            TextArea.Margins[BOOKMARK_MARGIN].Sensitive = true;
            TextArea.Margins[BOOKMARK_MARGIN].Type = MarginType.Symbol;
            //TextArea.Margins[BOOKMARK_MARGIN].Mask = Marker.MaskAll;
            TextArea.Margins[BOOKMARK_MARGIN].Cursor = MarginCursor.Arrow;
            TextArea.Margins[BOOKMARK_MARGIN].BackColor = IntToColor(0x211021);

            TextArea.Markers[BOOKMARK_MARKER].Symbol = MarkerSymbol.Bookmark;
            TextArea.Markers[BOOKMARK_MARKER].SetBackColor(Color.Bisque);
            TextArea.Markers[BOOKMARK_MARKER].SetForeColor(Color.Black);

            TextArea.MarginClick += txtSql_MarginClick;
            //------------------

            scintilla__TextChanged();
        }


        private void txtSql_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = txtSql.Lines[txtSql.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }


        private void scintilla__SelectionChanged(object sender, UpdateUIEventArgs e)
        {
            // Has the caret changed position?
            var caretPos = txtSql.CurrentPosition;
            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;
                var bracePos2 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(txtSql.GetCharAt(caretPos - 1)))
                    bracePos1 = (caretPos - 1);
                else if (IsBrace(txtSql.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    bracePos2 = txtSql.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                        txtSql.BraceBadLight(bracePos1);
                    else
                        txtSql.BraceHighlight(bracePos1, bracePos2);
                }
                else
                {
                    // Turn off brace matching
                    txtSql.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                }
            }

            try
            {
                int line = txtSql.CurrentLine;
                int column = txtSql.GetColumn(txtSql.CurrentPosition);

                tssLaPos.Text = $"{StringComplete(string.Format("Fila: {0}", line + 1), 13)} {StringComplete(string.Format("Col: {0}", column + 1), 13)}";
            }
            catch
            {
                //Do Nothing
            }
        }

        private void scintilla__IndentationGuides()
        {
            if (txtSql.IndentationGuides == IndentView.None)
                txtSql.IndentationGuides = IndentView.LookBoth;
            else
                txtSql.IndentationGuides = IndentView.None;
        }

        private void scintilla__TextChanged()
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


        private void CutTrailingSpaces()
        {
            StringBuilder strB = new StringBuilder();

            for (int i = 0; i < txtSql.Lines.Count; i++)
            {
                // Get the text of the current line
                string lineText = txtSql.Lines[i].Text;
                // Remove trailing spaces from the line
                strB.AppendLine(lineText.TrimEnd());
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

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            int outVal;
            int.TryParse(toolStripTextBox1.Text, out outVal);
            txtSql.TabWidth = (outVal == 0) ? 0 : outVal;
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
                txtSql.SelectionEnd = txtSql.Lines[t].EndPosition - 1;
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
                txtSql.SelectionEnd = txtSql.Lines[t].EndPosition - 1;
            }
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


        /// <summary>
        /// Rutina para Autocompletar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_CharAdded(object sender, CharAddedEventArgs e)
        {

            if (!autoCompleteEnabled || scintilla_end_mode)
                return;

            // Find the word start
            var currentPos = txtSql.CurrentPosition;
            var wordStartPos = txtSql.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                if (!txtSql.AutoCActive)
                {
                    txtSql.AutoCIgnoreCase = true;
                    txtSql.AutoCShow(lenEntered, autoCompleteKeywords);
                }
            }
        }


        private void SetAutocompleteMenuItemText()
        {
            autoCompleteToolStripMenuItem.Text = "Auto Complete" + (autoCompleteEnabled ? "   - Deshabilitar" : "   - Habilitar");
        }


        /// <summary>
        /// Brace matching en modo INSERT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (omit_key)
            {
                e.Handled = true;
                return;
            }

            if (txtSql.SelectedText.Length > 0)
            {
                var selected = txtSql.SelectedText;
                switch (e.KeyChar)
                {
                    case '"':
                        txtSql.ReplaceSelection($"\"{selected}\"");
                        e.Handled = true;
                        break;
                    case '\'':
                        txtSql.ReplaceSelection($"'{selected}'");
                        e.Handled = true;
                        break;
                    case '(':
                        txtSql.ReplaceSelection($"({selected})");
                        e.Handled = true;
                        break;
                    case '{':
                        txtSql.ReplaceSelection("{" + selected + "}");
                        e.Handled = true;
                        break;
                    case '[':
                        txtSql.ReplaceSelection($"[{selected}]");
                        e.Handled = true;
                        break;
                }
            }
            else
            {
                //switch (e.Char)
                switch (e.KeyChar)
                {
                    case '"':
                        txtSql.InsertText(txtSql.CurrentPosition, "\"");
                        break;
                    case '\'':
                        txtSql.InsertText(txtSql.CurrentPosition, "'");
                        break;
                    case '(':
                        txtSql.InsertText(txtSql.CurrentPosition, ")");
                        break;
                    case '{':
                        txtSql.InsertText(txtSql.CurrentPosition, "}");
                        break;
                    case '[':
                        txtSql.InsertText(txtSql.CurrentPosition, "]");
                        break;
                }
            }
        }


        /// <summary>
        /// Auto Indentación "scintilla"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_InsertCheck(object sender, InsertCheckEventArgs e)
        {
            if ((e.Text.EndsWith("\r") || e.Text.EndsWith("\n")))
            {
                var curLine = txtSql.LineFromPosition(e.Position);
                var curLineText = txtSql.Lines[curLine].Text;
                var indent = Regex.Match(curLineText, @"^[ ]*");
                e.Text += indent.Value; // Add indent following "\r\n"
            }
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

            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                var cadena = (string)e.Data.GetData(DataFormats.Text);
                if (cadena.Contains("\n"))
                {
                    var space_num = txtSql.GetColumn(Math.Min(txtSql.SelectionStart, txtSql.SelectionEnd));
                    var fill = new string(' ', space_num);

                    //Split
                    string[] snippet_spaced = System.Text.RegularExpressions.Regex.Split(cadena, "\r\n");
                    for (int i = 0; i < snippet_spaced.Length; i++)
                    {
                        snippet_spaced[i] = $"{(i != 0 ? fill : "")}{snippet_spaced[i]}";
                    }

                    cadena = "";
                    for (int i = 0; i < snippet_spaced.Length; i++)
                        cadena += $"{snippet_spaced[i]}{(i == snippet_spaced.Length - 1 ? "" : "\r\n")}";
                }

                if (txtSql.SelectionStart != txtSql.SelectionEnd)
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
            // System.Threading.Thread.Sleep(500);
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

            if (string.Compare(System.IO.Path.GetExtension(fileName).ToLower(), ".sqc", true) == 0
                || string.Compare(System.IO.Path.GetExtension(fileName).ToLower(), ".cfg", true) == 0)
            {
                CurrentFile = fileName;
                IsEncrypted = true;
                try
                {
                    OpenCryptoFile(CurrentFile);
                    this.Text = $"SQLCrypt - {CurrentFile}";
                }
                catch {
                    txtSql.Text = System.IO.File.ReadAllText(CurrentFile);
                    this.Text = CurrentFile;
                    this.Text = $"SQLCrypt - {CurrentFile}";
                }
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


        /// <summary>
        /// txtSql_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_KeyDown(object sender, KeyEventArgs e)
        {
            omit_key = false;

            //Completar Snippets
            if (e.Control && (e.KeyCode == Keys.Tab || e.KeyCode == Keys.OemMinus))
            {
                omit_key = true;
                e.Handled = true;
                complete_snippet();
                return;
            }

            // Modo Fin de Linea para Bloques
            if (txtSql.Selections.Count > 1 && e.KeyCode != Keys.End && scintilla_end_mode)
            {
                foreach (var sel in txtSql.Selections)
                {
                    var l = txtSql.LineFromPosition(sel.Start);
                    var end_pos = txtSql.Lines[l].EndPosition - 2;
                    sel.Start = end_pos;
                    sel.End = end_pos;
                    sel.Caret = end_pos;
                }
            }

            // Modo Fin de Linea para Bloques
            if (txtSql.Selections.Count > 1 && e.KeyCode == Keys.End)
            {
                scintilla_end_mode = true;
                foreach (var sel in txtSql.Selections)
                {
                    var l = txtSql.LineFromPosition(sel.Start);
                    var end_pos = txtSql.Lines[l].EndPosition - 2;
                    sel.Start = end_pos;
                    sel.End = end_pos;
                    sel.Caret = end_pos;
                }
                e.Handled = true;
            }

            // Salir de Modo Fin de Linea para Bloques
            if (scintilla_end_mode && txtSql.Selections.Count <= 1)
                scintilla_end_mode = false;
        }


        /// <summary>
        /// txtSql_PreviousBookmark
        /// </summary>
        private void txtSql_PreviousBookmark()
        {
            var line = txtSql.LineFromPosition(txtSql.CurrentPosition);
            var prevLine = txtSql.Lines[--line].MarkerPrevious(1 << BOOKMARK_MARKER);
            if (prevLine != -1)
                txtSql.Lines[prevLine].Goto();
        }


        /// <summary>
        /// txtSql_NextBookmark
        /// </summary>
        private void txtSql_NextBookmark()
        {
            var line = txtSql.LineFromPosition(txtSql.CurrentPosition);
            var nextLine = txtSql.Lines[++line].MarkerNext(1 << BOOKMARK_MARKER);
            if (nextLine != -1)
                txtSql.Lines[nextLine].Goto();
        }


        /// <summary>
        /// txtSql_ToogleBookmark
        /// </summary>
        private void txtSql_ToogleBookmark()
        {
            const uint mask = (1 << BOOKMARK_MARKER);
            var line = txtSql.Lines[txtSql.LineFromPosition(txtSql.CurrentPosition)];
            if ((line.MarkerGet() & mask) > 0)
            {
                // Remove existing bookmark
                line.MarkerDelete(BOOKMARK_MARKER);
            }
            else
            {
                // Add bookmark
                line.MarkerAdd(BOOKMARK_MARKER);
            }
        }


        /// <summary>
        /// Control para permitir Edición en modo Fin de Línea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_KeyUp(object sender, KeyEventArgs e)
        {
            if (omit_key)
            {
                e.Handled = true;
                return;
            }

            if (txtSql.Selections.Count > 1 && e.KeyCode != Keys.End && scintilla_end_mode)
            {
                foreach (var sel in txtSql.Selections)
                {
                    var l = txtSql.LineFromPosition(sel.Start);
                    var end_pos = txtSql.Lines[l].EndPosition - 2;
                    sel.Start = end_pos;
                    sel.End = end_pos;
                    sel.Caret = end_pos;
                }
            }
        }


        /// <summary>
        /// HELPER - retorna el número de Espacios al inicio de la línea actual en "scintilla"
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private int beginning_spaces(string texto)
        {
            int count = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == ' ') count++;
                if (texto[i] != ' ') break;
            }
            return count;
        }


        /// <summary>
        /// Carga snippet seleccionado en la selección actual en "scintilla"
        /// </summary>
        /// <param name="snippet"></param>
        private void Load_Snippet(string snippet)
        {
            var l = txtSql.LineFromPosition(txtSql.SelectionStart);
            var space_num = txtSql.GetColumn(Math.Min(txtSql.SelectionStart, txtSql.SelectionEnd));

            var fill = new string(' ', space_num);
            if (fill == null)
                fill = "";

            //Split
            string[] snippet_spaced = System.Text.RegularExpressions.Regex.Split(snippet, "\r\n");
            for (int i = 1; i < snippet_spaced.Length; i++)
            {
                snippet_spaced[i] = $"{(i != 0 ? fill : "")}{snippet_spaced[i]}";
            }

            var cadena = "";
            for (int i = 0; i < snippet_spaced.Length; i++)
                cadena += $"{snippet_spaced[i]}{(i == snippet_spaced.Length - 1 ? "" : "\r\n")}";

            txtSql.ReplaceSelection(cadena);

            //if (snippet.Contains("<"))
            //    MessageBox.Show("Presione [Ctrl][-] o [Ctrl][Tab] para completar el Snippet", "Atención");
        }



        /// <summary>
        /// Helper para buscar los campos a llenar para un snippet
        /// </summary>
        private void complete_snippet()
        {
            SearchFlags flags = new SearchFlags();
            flags |= SearchFlags.Regex;
            txtSql.SearchFlags = flags;
            txtSql.TargetStart = 0;
            txtSql.TargetEnd = txtSql.TextLength;

            var pos = txtSql.SearchInTarget("[<][a-zA-Z0-9_]+[>]");

            if (pos == -1)
            {
                SystemSounds.Beep.Play();
            }

            if (pos >= 0)
                txtSql.SetSel(txtSql.TargetStart, txtSql.TargetEnd);
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


        /// <summary>
        /// Abre archivo .sqc "encriptado"
        /// </summary>
        /// <param name="sFileName"></param>
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
                var res = MessageBox.Show("El Documento ha sido Modificado.\n¿Desea Grabar antes de Salir?", "Atención", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
            WorkPath = System.Windows.Forms.Application.StartupPath;
        }


        //Ejecutar !!! ejecutarComandoToolStripMenuItem_Click
        private void ejecutarComandoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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

            ExecuteAlternative();

        }


        /// <summary>
        /// Ejecución de Query
        /// </summary>
        private void ExecuteAlternative()
        {
            if (threadQuery != null)
            {
                if (threadQuery.IsAlive)
                {
                    MessageBox.Show("Hay una consulta en curso...", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!hSql.ConnectionStatus)
            {
                MessageBox.Show(this, "Debe estar conectado(a) a una Base de Datos.", "Atención", MessageBoxButtons.OK);
                return;
            }

            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Conexión a SQL con Error:\r\n{hSql.ErrorString}\r\n{hSql.Messages}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string sSqlCommand = "";

            if (txtSql.Text.Trim().ToString() == "")
            {
                MessageBox.Show(this, "No hay sentencia SQL para ejecutar", "Atención", MessageBoxButtons.OK);
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
                    Program.CancelQuery = false;
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
                            Program.DataBase = curr_db;

                        Despliegue.hSql.CloseDBConn();
                    }
                    Program.hSqlQuery = null;
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

            queryTimer = new System.Timers.Timer(300);
            queryTimer.SynchronizingObject = this;
            queryTimer.Elapsed += OnQueryTimeEvent;
            queryTimer.AutoReset = true;
            queryTimer.Enabled = true;
            queryTimer.Start();
        }


        /// <summary>
        /// Finaliza el Timer de Consulta en Ejecución
        /// </summary>
        private void DisposeTimer()
        {
            if ( (Program.sql_spid != 0 || Program.hSqlQuery != null) )
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

            if (Program.DataBase != "")
            {
                hSql.SetDatabase(Program.DataBase);
                Program.DataBase = "";
            }
            threadQuery = null;
            LoadDatabaseList();
        }


        /// <summary>
        /// Evento del Timer de Consulta en Ejecución
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnQueryTimeEvent(Object source, ElapsedEventArgs e)
        {

            if (Program.sql_spid != 0)
            {
                laDataLoadStatus.Visible=false;
                if (pgBarQuery.Value < pgBarQuery.Maximum)
                    pgBarQuery.Value += 10;
                else
                    pgBarQuery.Value = 0;
            }
            
            if (Program.sql_spid == 0 && Program.hSqlQuery != null)
            {
                pgBarQuery.Value = pgBarQuery.Maximum;

                if (laDataLoadStatus.Text == "")
                {
                    laDataLoadStatus.Text = "Cargando Data...";
                    laDataLoadStatus.Visible = true;
                }
                else
                {
                    laDataLoadStatus.Text = "";
                    laDataLoadStatus.Visible = false;
                }
                laDataLoadStatus.Refresh();
            }

            if (Program.sql_spid == 0 && Program.hSqlQuery == null)
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
        /// Selección "scintilla" a LOWERCASE
        /// </summary>
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


        /// <summary>
        /// Selección "scintilla" a UPPERCASE
        /// </summary>
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

        
        /// <summary>
        /// Cierre del Documento en curso.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSql.Modified)
            {
                var res = MessageBox.Show("El Documento ha sido Modificado.\nDesea Grabar antes de Cerrarlo?", "Atención", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
            tssLaStat.Text = "Archivo Cerrado...";
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

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = WorkPath;
            ofd.Filter = "Sql Files (*.sql;*.sqc)|*.sql;*.sqc|Text Files (*.txt)|*.txt|Config Files (*.cfg)|*.cfg|Any File (*.*)|*.*";
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
            if (hSql.ConnectionStatus)
                hSql.CloseDBConn();

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
                MessageBox.Show("Comando vacío");
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
                        MessageBox.Show(this, $"Error SQL\n{hSql.ErrorString}", "Atención", MessageBoxButtons.OK);
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
                hSql.EncryptStringtoFile(txtSql.Text.ToString(), CurrentFile);
            }
            else
            {
                txtSql_SaveFile();
            }

            txtSql.SetSavePoint();
            txtSql.EmptyUndoBuffer();
            tssLaStat.Text = "Archivo Grabado...";

        }


        /// <summary>
        /// Llamada a Grabación de Archivo en Edición
        /// </summary>
        private void txtSql_SaveFile()
        {
            System.IO.File.WriteAllText(CurrentFile, txtSql.Text);
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

                if (lstObjetos.SelectedIndices.Count > 0)
                {
                    lstObjetos.TopIndex = lstObjetos.SelectedIndices[0];
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
                MessageBox.Show("Debe conectarse a una Base de Datos...", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Elementos += (Elementos != "" ? "\n" : "") + lstObjetos.GetItemText(item);
            }
            
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("No hay elementos Seleccionados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    Elementos += (Elementos != "" ? "\n" : "") + $"{lsColumnas.Items[x].Text} {lsColumnas.Items[x].SubItems[1].Text} {lsColumnas.Items[x].SubItems[2].Text}";
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("No hay elementos Seleccionados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    Elementos += (Elementos != "" ? "\n" : "") + $"{lsColumnas.Items[x].Text}";
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                MessageBox.Show("No hay elementos Seleccionados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Esta opción es sólo para Tablas y Vistas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Esta opción aplica sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Esta opción es sólo para Tablas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            sb.Append($"\nSchema    : {DBObj.schema_name}\n");
            sb.Append($"Nombre    : {DBObj.name}\n");
            sb.Append($"Id        : {DBObj.object_id}\n");
            sb.Append($"Type      : {DBObj.type}\n")    ;
            sb.Append($"Type Desc : {DBObj.type_desc}\n")   ;
            sb.Append($"Creado    : {DBObj.create_date}\n");
            sb.Append($"Modificado: {DBObj.modify_date}\n");
            sb.Append($"Schema Id : {DBObj.schema_id}\n");
            sb.Append($"Parrent Id: {DBObj.parent_object_id}\n");

            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());

            MessageBox.Show("Información en Clipboard", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show("Opción no corresponde, el Objeto es un procedimiento...", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sSalida = string.Empty;

            sSalida = DBObj.ObjGetCreateTable();

            Clipboard.Clear();
            Clipboard.SetText(sSalida);

            MessageBox.Show("Información pegada en el Clipboard", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("Indique el Texto a buscar en 'Buscar...'");
            }
            Objetos.FindByText(((ObjectType)cbObjetos.SelectedItem).type, txBuscaEnLista.Text);
            if (Objetos.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias");
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

            laTablas.Text = $"Objetos: {lstObjetos.Items.Count}";

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
                    Elementos += (Elementos != "" ? "\r\n" : "") + lstObjetos.SelectedItems[i].ToString();
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
                        Elementos += (Elementos != "" ? "\r\n" : "") + $"{lsColumnas.Items[x].Text}";
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
                MessageBox.Show("Debe establecer una conexión a Base de Datos", "Atención");
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
            CutTrailingSpaces();
        }

        /// <summary>
        /// Convertir Selección a Mayúsculas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selecciónAMayúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }


        /// <summary>
        /// Convertir Selección a Minúsculas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selecciónAMinúsculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
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
                MessageBox.Show("Debe estar conectado a una Base de Datos", "Atención");
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
            TAB_to_spaces();
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


        /// <summary>
        /// Habilita-Deshabilita Autocompletar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoCompleteEnabled = !autoCompleteEnabled;
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
            scintilla__IndentationGuides();
        }

        
        private void txtSql_TextChanged(object sender, EventArgs e)
        {
            scintilla__TextChanged();
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
            txtSql_PreviousBookmark();
        }


        /// <summary>
        /// MArca Desmarca Bookmark en la Línea Actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSql_ToogleBookmark();
        }


        /// <summary>
        /// Ir al Siguiente Bookmark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goToNextBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSql_NextBookmark();
        }


        /// <summary>
        /// Completar Snippet busca <expansión>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void completarSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            complete_snippet();
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
            var space_num = txtSql.GetColumn(Math.Min(txtSql.SelectionStart, txtSql.SelectionEnd));
            var fill = new string(' ', space_num-1 < 0? 0: space_num-1);

            for (int x = 0; x < lsColumnas.Items.Count; ++x)
            {
                if (lsColumnas.Items[x].Selected)
                    Elementos += (Elementos != "" ? $"\n{fill}," : "") + $"{lsColumnas.Items[x].Text}";
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
            if (Program.sql_spid == 0 && Program.hSqlQuery != null)
            {
                Program.CancelQuery = true;
                MessageBox.Show("Query finalizada, la data está en proceso de Carga, ya no es posible Cancelar...", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Program.sql_spid != 0)
            {
                int mySpid = hSql.GetCurrent_SPID();
                hSql.Kill_SPID(Program.sql_spid);
                Program.CancelQuery = true;
                if (hSql.ErrorExiste || hSql.Messages != "")
                {
                    MessageBox.Show($"Error\r\n{hSql.ErrorString}\r\n{hSql.Messages}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DisposeTimer();
                MessageBox.Show($"No existe un proceso de Consulta activo", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (threadQuery != null)
                if (!threadQuery.IsAlive)
                    threadQuery = null;

            System.GC.Collect();
            
        }


    }
}
