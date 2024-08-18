using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Media;
using System.IO;


namespace SQLCrypt.FunctionalClasses
{
    public class ScintillaCustom
    {
        private const int BOOKMARK_MARGIN = 1; // Conventionally the symbol margin
        private const int BOOKMARK_MARKER = 3; // Arbitrary. Any valid index would work.
        private const int NUM = 8;
        private const string EOL = "\r\n";

        private bool scintilla_end_mode = false;
        private string autoCompleteKeywords = "";
        private bool omit_key = false;
        private int lastCaretPos = 0;

        private string keyWords;
        private string keyWords2;

        public int _maxLineNumberCharLength { get; internal set; }

        private ScintillaNET.Scintilla scintillaCtrl { get; set; }
        public bool AutoCompleteEnabled { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scintillaCtrl"></param>
        public ScintillaCustom(ScintillaNET.Scintilla scintillaCtrl, string keywordFile, string keyword2File)
        {
            this.scintillaCtrl = scintillaCtrl;
            LoadKeywords(keywordFile);
            LoadKeywords2(keyword2File);
        }


        public int CurrentMinColumn 
        { get {
                return scintillaCtrl.GetColumn(Math.Min(scintillaCtrl.SelectionStart, scintillaCtrl.SelectionEnd));
              }
        }


        public int CurrentColumn
        {
            get {
                return scintillaCtrl.GetColumn(scintillaCtrl.CurrentPosition);
            }
        }


        /// <summary>
        /// Inicialización del Objeto
        /// </summary>
        /// <param name="keyWords"></param>
        /// <param name="keyWords2"></param>
        public void InitSyntaxColoring()
        {

            autoCompleteKeywords = keyWords.ToUpper();
            AutoCompleteEnabled = true;

            // Configure the default style
            scintillaCtrl.Margins[0].Width = 5;

            scintillaCtrl.StyleResetDefault();

            scintillaCtrl.Styles[Style.Default].Font = "Consolas";
            scintillaCtrl.Styles[Style.Default].Size = 10;
            scintillaCtrl.Styles[Style.Default].BackColor = IntToColor(0x212121);
            scintillaCtrl.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);

            scintillaCtrl.CaretLineBackColor = IntToColor(0x333333);
            scintillaCtrl.CaretForeColor = IntToColor(0xF0F0F0);
            scintillaCtrl.CaretWidth = 2;

            //TextArea.SetSelectionBackColor(true, IntToColor(0x000099));
            scintillaCtrl.SetSelectionBackColor(true, IntToColor(0x004389));
            scintillaCtrl.StyleClearAll();

            //Resaltado de Parentesis (Braces)
            scintillaCtrl.Styles[Style.BraceBad].ForeColor = IntToColor(0xFFFFFF);
            scintillaCtrl.Styles[Style.BraceLight].ForeColor = IntToColor(0xFF00AA);

            scintillaCtrl.Styles[Style.Sql.Identifier].ForeColor = IntToColor(0xD0DAE2);
            scintillaCtrl.Styles[Style.Sql.Comment].ForeColor = IntToColor(0xBD758B);
            scintillaCtrl.Styles[Style.Sql.CommentLine].ForeColor = IntToColor(0x40BF57);
            scintillaCtrl.Styles[Style.Sql.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            scintillaCtrl.Styles[Style.Sql.Number].ForeColor = IntToColor(0xFFFF00);
            scintillaCtrl.Styles[Style.Sql.String].ForeColor = IntToColor(0xFFFF00);
            scintillaCtrl.Styles[Style.Sql.Character].ForeColor = IntToColor(0xE95454);
            scintillaCtrl.Styles[Style.Sql.Operator].ForeColor = IntToColor(0xE0E0E0);
            scintillaCtrl.Styles[Style.Sql.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            scintillaCtrl.Styles[Style.Sql.Word].ForeColor = IntToColor(0x48A8EE);
            scintillaCtrl.Styles[Style.Sql.Word2].ForeColor = IntToColor(0xF98906);
            scintillaCtrl.Styles[Style.Sql.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            scintillaCtrl.Styles[Style.Sql.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);

            scintillaCtrl.Lexer = Lexer.Sql;

            scintillaCtrl.SetKeywords(0, keyWords);
            scintillaCtrl.SetKeywords(1, keyWords2);

            scintillaCtrl.Styles[Style.LineNumber].ForeColor = IntToColor(0xAFAFAF);
            scintillaCtrl.Styles[Style.LineNumber].BackColor = IntToColor(0x211021);

            scintillaCtrl.AdditionalSelectionTyping = true;
            scintillaCtrl.MultipleSelection = true;
            scintillaCtrl.MouseSelectionRectangularSwitch = true;
            scintillaCtrl.VirtualSpaceOptions = VirtualSpace.RectangularSelection;

            //Barra de Book-Marks
            scintillaCtrl.Margins[BOOKMARK_MARGIN].Width = 16;
            scintillaCtrl.Margins[BOOKMARK_MARGIN].Sensitive = true;
            scintillaCtrl.Margins[BOOKMARK_MARGIN].Type = MarginType.Symbol;
            //TextArea.Margins[BOOKMARK_MARGIN].Mask = Marker.MaskAll;
            scintillaCtrl.Margins[BOOKMARK_MARGIN].Cursor = MarginCursor.Arrow;
            scintillaCtrl.Margins[BOOKMARK_MARGIN].BackColor = IntToColor(0x211021);

            scintillaCtrl.Markers[BOOKMARK_MARKER].Symbol = MarkerSymbol.Bookmark;
            scintillaCtrl.Markers[BOOKMARK_MARKER].SetBackColor(Color.Bisque);
            scintillaCtrl.Markers[BOOKMARK_MARKER].SetForeColor(Color.Black);

            // ---- EVENTOS ------

            scintillaCtrl.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(_CharAdded);
            scintillaCtrl.InsertCheck += new System.EventHandler<ScintillaNET.InsertCheckEventArgs>(txtSql_InsertCheck);
            scintillaCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSql_KeyDown);
            scintillaCtrl.KeyUp += new System.Windows.Forms.KeyEventHandler(txtSql_KeyUp);
            scintillaCtrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(_KeyPress);
            scintillaCtrl.MarginClick += txtSql_MarginClick;

            //------------------
            // Remove all uses of our indicator
            scintillaCtrl.IndicatorCurrent = NUM;

            // Update indicator appearance
            scintillaCtrl.Indicators[NUM].Style = IndicatorStyle.StraightBox;
            scintillaCtrl.Indicators[NUM].Under = true;
            scintillaCtrl.Indicators[NUM].ForeColor = Color.Magenta;
            scintillaCtrl.Indicators[NUM].OutlineAlpha = 80;
            scintillaCtrl.Indicators[NUM].Alpha = 80;

        }


        public void LoadKeywords(string keywordFile)
        {
            if (File.Exists(keywordFile))
                keyWords = File.ReadAllText(keywordFile);

            keyWords = keyWords.Replace(EOL, " ");
        }


        public void LoadKeywords2(string keyword2File)
        {
            if (File.Exists(keyword2File))
                keyWords2 = File.ReadAllText(keyword2File);

            keyWords2 = keyWords2.Replace(EOL, " ");
        }


        /// <summary>
        /// Selección "scintilla" a LOWERCASE
        /// </summary>
        public void SelectionLowercase()
        {

            // save the selection
            int start = scintillaCtrl.SelectionStart;
            int end = scintillaCtrl.SelectionEnd;

            // modify the selected text
            scintillaCtrl.ReplaceSelection(scintillaCtrl.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            scintillaCtrl.SetSelection(start, end);
        }


        /// <summary>
        /// Selección "scintilla" a UPPERCASE
        /// </summary>
        public void SelectionUppercase()
        {

            // save the selection
            int start = scintillaCtrl.SelectionStart;
            int end = scintillaCtrl.SelectionEnd;

            // modify the selected text
            scintillaCtrl.ReplaceSelection(scintillaCtrl.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            scintillaCtrl.SetSelection(start, end);
        }


        /// <summary>
        /// txtSql_MarginClick    OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSql_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = scintillaCtrl.Lines[scintillaCtrl.LineFromPosition(e.Position)];
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



        /// <summary>
        /// splitByComma
        /// </summary>
        public void splitByComma()
        {
            int row = scintillaCtrl.CurrentLine;
            int pos_ini = scintillaCtrl.Lines[row].Position;
            //int pos_fin = txtSql.Lines[row].EndPosition;

            for (int i = pos_ini; i < scintillaCtrl.TextLength; i++)
            {
                if (i + 1 >= scintillaCtrl.TextLength)
                    break;

                if (scintillaCtrl.Text.Substring(i, 2) == EOL)
                    break;

                if (scintillaCtrl.Text.Substring(i, 1) == ",")
                {
                    i++;
                    int len = 0;
                    scintillaCtrl.SelectionStart = i;

                    while (scintillaCtrl.Text.Substring(i + len, 1) == " ")
                        len++;

                    scintillaCtrl.SelectionEnd = i + len;
                    scintillaCtrl.ReplaceSelection(EOL);
                    i = scintillaCtrl.SelectionStart;

                }

            }

        }



        /// <summary>
        /// HighlightErrorClean  OK
        /// </summary>
        public void HighlightErrorClean()
        {
            scintillaCtrl.IndicatorCurrent = NUM;
            scintillaCtrl.IndicatorClearRange(0, scintillaCtrl.TextLength);
        }


        /// <summary>
        /// HighlightError  OK
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void HighlightError(int row, int col)
        {

            // Search the document
            scintillaCtrl.SearchFlags = SearchFlags.None;
            int _row = 0;

            if (scintillaCtrl.SelectedText != "")
            {
                _row = scintillaCtrl.LineFromPosition(scintillaCtrl.SelectionStart);
            }
            else
            {
                _row = 0;
            }

            int end_pos = 0;
            int start_pos = scintillaCtrl.Lines[_row + row].Position + col - 1;
            for (int i = start_pos; i < scintillaCtrl.TextLength && scintillaCtrl.Text.Substring(i, 1) != " "; i++)
                end_pos = i;

            scintillaCtrl.IndicatorFillRange(start_pos, end_pos - start_pos + 1);
            scintillaCtrl.SelectionStart = start_pos;
            scintillaCtrl.SelectionEnd = start_pos;
        }


        /// <summary>
        /// Manejo de Colores en Hexa
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }


        /// <summary>
        /// Braces Identification
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsBrace(int c)
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
        /// MostrarGuiaIndentacion
        /// </summary>
        public void MostrarGuiaIndentacion()
        {
            if (scintillaCtrl.IndentationGuides == IndentView.None)
                scintillaCtrl.IndentationGuides = IndentView.LookBoth;
            else
                scintillaCtrl.IndentationGuides = IndentView.None;
        }


        /// <summary>
        /// MostrarNumerosDeLinea
        /// </summary>
        public void MostrarNumerosDeLinea()
        {
            if (scintillaCtrl.Margins[0].Width != 3)
                scintillaCtrl.Margins[0].Width = 3;
            else
            {
                const int padding = 2;
                scintillaCtrl.Margins[0].Width = scintillaCtrl.TextWidth(Style.LineNumber, new string('9', _maxLineNumberCharLength + 1)) + padding;
            }
        }


        /// <summary>
        /// CommentSelection
        /// </summary>
        public void CommentSelection()
        {

            if (scintillaCtrl.SelectedText.Length > 0)
            {
                int f = scintillaCtrl.LineFromPosition(scintillaCtrl.SelectionStart);
                int t = scintillaCtrl.LineFromPosition(scintillaCtrl.SelectionEnd);

                for (int i = f; i <= t; i++)
                {
                    scintillaCtrl.InsertText(scintillaCtrl.Lines[i].Position, "--");
                }
                scintillaCtrl.SelectionStart = scintillaCtrl.Lines[f].Position;
                scintillaCtrl.SelectionEnd = scintillaCtrl.Lines[t].EndPosition - 1;
            }
        }


        /// <summary>
        /// UnCommentSelection
        /// </summary>
        public void UnCommentSelection()
        {
            if (scintillaCtrl.SelectedText.Length > 0)
            {
                int f = scintillaCtrl.LineFromPosition(scintillaCtrl.SelectionStart);
                int t = scintillaCtrl.LineFromPosition(scintillaCtrl.SelectionEnd);

                for (int i = f; i <= t; i++)
                {
                    string s = scintillaCtrl.Lines[i].Text;
                    if (s.TrimStart().StartsWith("--"))
                    {
                        var regex = new Regex(Regex.Escape("--"));
                        var newText = regex.Replace(s, "", 1);
                        int x = scintillaCtrl.Lines[i].Position;
                        int y = scintillaCtrl.Lines[i].EndPosition;
                        scintillaCtrl.SelectionStart = x;
                        scintillaCtrl.SelectionEnd = y;
                        scintillaCtrl.ReplaceSelection(newText);
                    }
                }
                scintillaCtrl.SelectionStart = scintillaCtrl.Lines[f].Position;
                scintillaCtrl.SelectionEnd = scintillaCtrl.Lines[t].EndPosition - 1;
            }
        }


        /// <summary>
        /// _CharAdded      OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _CharAdded(object sender, CharAddedEventArgs e)
        {

            if (!AutoCompleteEnabled || scintilla_end_mode)
                return;

            // Find the word start
            var currentPos = scintillaCtrl.CurrentPosition;
            var wordStartPos = scintillaCtrl.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                if (!scintillaCtrl.AutoCActive)
                {
                    scintillaCtrl.AutoCIgnoreCase = true;
                    scintillaCtrl.AutoCShow(lenEntered, autoCompleteKeywords);
                }
            }
        }


        /// <summary>
        /// Auto Indentación "scintilla"      OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtSql_InsertCheck(object sender, InsertCheckEventArgs e)
        {
            if ((e.Text.EndsWith("\r") || e.Text.EndsWith("\n")))
            {
                var curLine = scintillaCtrl.LineFromPosition(e.Position);
                var curLineText = scintillaCtrl.Lines[curLine].Text;
                var indent = Regex.Match(curLineText, @"^[ ]*");
                e.Text += indent.Value; // Add indent following "\r\n"
            }
        }


        /// <summary>
        /// txtSql_KeyDown           OK
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
            if (scintillaCtrl.Selections.Count > 1 && e.KeyCode != Keys.End && scintilla_end_mode)
            {
                foreach (var sel in scintillaCtrl.Selections)
                {
                    omit_key = false;
                    var l = scintillaCtrl.LineFromPosition(sel.Start);
                    var end_pos = scintillaCtrl.Lines[l].EndPosition - 2;
                    sel.Start = end_pos;
                    sel.End = end_pos;
                    sel.Caret = end_pos;
                }
            }

            // Modo Fin de Linea para Bloques
            if (scintillaCtrl.Selections.Count > 1 && e.KeyCode == Keys.End)
            {
                omit_key = false;
                scintilla_end_mode = true;
                foreach (var sel in scintillaCtrl.Selections)
                {
                    var l = scintillaCtrl.LineFromPosition(sel.Start);
                    var end_pos = scintillaCtrl.Lines[l].EndPosition - 2;
                    sel.Start = end_pos;
                    sel.End = end_pos;
                    sel.Caret = end_pos;
                }
                e.Handled = true;
            }

            // Salir de Modo Fin de Linea para Bloques
            if (scintilla_end_mode && scintillaCtrl.Selections.Count <= 1)
            {
                scintilla_end_mode = false;
            }
        }


        /// <summary>
        /// txtSql_PreviousBookmark
        /// </summary>
        public void PreviousBookmark()
        {
            var line = scintillaCtrl.CurrentLine;
            var prevLine = scintillaCtrl.Lines[--line].MarkerPrevious(1 << BOOKMARK_MARKER);
            if (prevLine != -1)
                scintillaCtrl.Lines[prevLine].Goto();
        }


        /// <summary>
        /// txtSql_NextBookmark
        /// </summary>
        public void NextBookmark()
        {
            var line = scintillaCtrl.CurrentLine;
            var nextLine = scintillaCtrl.Lines[++line].MarkerNext(1 << BOOKMARK_MARKER);
            if (nextLine != -1)
                scintillaCtrl.Lines[nextLine].Goto();
        }


        /// <summary>
        /// Control para permitir Edición en modo Fin de Línea       OK
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

            if (scintillaCtrl.Selections.Count > 1 && e.KeyCode != Keys.End && scintilla_end_mode)
            {
                foreach (var sel in scintillaCtrl.Selections)
                {
                    var l = scintillaCtrl.LineFromPosition(sel.Start);
                    var end_pos = scintillaCtrl.Lines[l].EndPosition - 2;
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
        public int GetIndentation(string texto)
        {
            int count = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == ' ') count++;
                if (texto[i] != ' ') break;
            }
            return count;
        }


        public void SaveFile(string fileName)
        {
            System.IO.File.WriteAllText(fileName, scintillaCtrl.Text);
        }


        /// <summary>
        /// Helper para buscar los campos a llenar para un snippet
        /// </summary>
        public void complete_snippet()
        {
            SearchFlags flags = new SearchFlags();
            flags |= SearchFlags.Regex;
            scintillaCtrl.SearchFlags = flags;
            scintillaCtrl.TargetStart = 0;
            scintillaCtrl.TargetEnd = scintillaCtrl.TextLength;

            var pos = scintillaCtrl.SearchInTarget("[<][a-zA-Z0-9_]+[>]");

            if (pos == -1)
            {
                SystemSounds.Beep.Play();
            }

            if (pos >= 0)
                scintillaCtrl.SetSel(scintillaCtrl.TargetStart, scintillaCtrl.TargetEnd);
        }



        /// <summary>
        /// txtSql_ToogleBookmark
        /// </summary>
        public void ToogleBookmark()
        {
            const uint mask = (1 << BOOKMARK_MARKER);
            var line = scintillaCtrl.Lines[scintillaCtrl.CurrentLine];
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
        /// Brace matching en modo INSERT     OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _KeyPress(object sender, KeyPressEventArgs e)
        {
            if (omit_key)
            {
                e.Handled = true;
                return;
            }

            if (scintillaCtrl.SelectedText.Length > 0)
            {
                var selected = scintillaCtrl.SelectedText;
                switch (e.KeyChar)
                {
                    case '"':
                        scintillaCtrl.ReplaceSelection($"\"{selected}\"");
                        e.Handled = true;
                        break;
                    case '\'':
                        scintillaCtrl.ReplaceSelection($"'{selected}'");
                        e.Handled = true;
                        break;
                    case '(':
                        scintillaCtrl.ReplaceSelection($"({selected})");
                        e.Handled = true;
                        break;
                    case '{':
                        scintillaCtrl.ReplaceSelection("{" + selected + "}");
                        e.Handled = true;
                        break;
                    case '[':
                        scintillaCtrl.ReplaceSelection($"[{selected}]");
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
                        scintillaCtrl.InsertText(scintillaCtrl.CurrentPosition, "\"");
                        break;
                    case '\'':
                        scintillaCtrl.InsertText(scintillaCtrl.CurrentPosition, "'");
                        break;
                    case '(':
                        scintillaCtrl.InsertText(scintillaCtrl.CurrentPosition, ")");
                        break;
                    case '{':
                        scintillaCtrl.InsertText(scintillaCtrl.CurrentPosition, "}");
                        break;
                    case '[':
                        scintillaCtrl.InsertText(scintillaCtrl.CurrentPosition, "]");
                        break;
                }
            }
        }



        /// <summary>
        /// MostrarEspacios
        /// </summary>
        public void MostrarEspacios()
        {
            if (scintillaCtrl.ViewWhitespace == WhitespaceMode.Invisible)
                scintillaCtrl.ViewWhitespace = WhitespaceMode.VisibleAlways;
            else
                scintillaCtrl.ViewWhitespace = WhitespaceMode.Invisible;
        }


        
        /// <summary>
        /// Elimina Espacios al final de las líneas
        /// </summary>
        public void CutTrailingSpaces()
        {
            StringBuilder strB = new StringBuilder();

            for (int i = 0; i < scintillaCtrl.Lines.Count; i++)
            {
                // Get the text of the current line
                string lineText = scintillaCtrl.Lines[i].Text;
                // Remove trailing spaces from the line
                strB.AppendLine(lineText.TrimEnd());
            }
            scintillaCtrl.Text = strB.ToString();
        }



        public void BraceHighLight()
        {
            // Has the caret changed position?
            var caretPos = scintillaCtrl.CurrentPosition;
            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;
                var bracePos2 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(scintillaCtrl.GetCharAt(caretPos - 1)))
                    bracePos1 = (caretPos - 1);
                else if (IsBrace(scintillaCtrl.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    bracePos2 = scintillaCtrl.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                        scintillaCtrl.BraceBadLight(bracePos1);
                    else
                        scintillaCtrl.BraceHighlight(bracePos1, bracePos2);
                }
                else
                {
                    // Turn off brace matching
                    scintillaCtrl.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                }
            }

        }



        /// <summary>
        /// TABtoSpaces   OJO
        /// </summary>
        public void TABtoSpaces(int TabSpaces)
        {
            string spaces = new string(' ', TabSpaces);
            
            StringBuilder strB = new StringBuilder();

            for (int i = 0; i < scintillaCtrl.Lines.Count; i++)
            {
                // Get the text of the current line
                string lineText = scintillaCtrl.Lines[i].Text;
                // Remove trailing spaces from the line
                strB.AppendLine(lineText.TrimEnd().Replace("\t", spaces));
            }
            scintillaCtrl.Text = strB.ToString();
        }


        // this.txtSql.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.txtSql_SelectionChanged);  OK
        // this.txtSql.TextChanged += new System.EventHandler(this.txtSql_TextChanged);  
        // this.txtSql.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSql_DragDrop);
        // this.txtSql.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtSql_DragEnter);

    }
}
