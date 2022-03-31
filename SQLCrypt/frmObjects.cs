using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLCrypt.StructureClasses;
using HSql = SQLCrypt.FunctionalClasses.MySql.MySql;

namespace SQLCrypt
{
    /// <summary>
    /// Formulario de Objetos, acá se pueden explorar y buscar objetos
    /// </summary>
    public partial class frmObjects:Form
    {
        ToolTip MytoolTip = new ToolTip();
        ToolTip MytoolTipDataType = new ToolTip();
        DbObjects Objetos;
        HSql hSql;

        private int WinMinHeight = 0;
        private int WinMinWidth = 0;
        private int rchTxtWidth = 0;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hSql"></param>
        public frmObjects(HSql hSql)
        {
            InitializeComponent();

            this.hSql = hSql;
            btSalir.Top = -20;

            WinMinHeight = this.Height;
            WinMinWidth = this.Width;

            toolStripStatusLabel.Text = string.Empty;
            toolStripStatusLabelSel.Text = string.Empty;
            Load_cbObjType();
            Objetos = new DbObjects(hSql);

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Get More Info", new EventHandler(ObjGetMoreInfo));
            cm.MenuItems.Add("Get Text", new EventHandler(ObjGetText));
            cm.MenuItems.Add("Backup Text (multi files)", new EventHandler(BkpObjGetText));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ObjSelectedToClipboard));
            cm.MenuItems.Add("Invert Selection", new EventHandler(ObjectInvertSelection));
            cm.MenuItems.Add("Select All", new EventHandler(ObjectSelectAll));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Get CREATE TABLE", new EventHandler(ObjGetCreateTable));
            cm.MenuItems.Add("Select TOP(10) * FROM ", new EventHandler(ObjectSelectStar));
            cm.MenuItems.Add("Select * FROM ", new EventHandler(ObjectSelectStarAll));
            cm.MenuItems.Add("Editar Datos", new EventHandler(EditarDatos));

            ContextMenu RtfCm = new ContextMenu();
            RtfCm.MenuItems.Add("Select all", new EventHandler(rtfSelectAll));
            RtfCm.MenuItems.Add("Selection To Clipboard", new EventHandler(rtfSelectionToClipBoard));
            RtfCm.MenuItems.Add("Clear Text", new EventHandler(rtfClear));

            rchTxtWidth = rchTxt.Width;
            lstObjetos.ContextMenu = cm;
            rchTxt.ContextMenu = RtfCm;
            this.rchTxt.RightMargin = 300 * this.rchTxt.Font.Height;

            MytoolTip.SetToolTip(txBuscaEnLista, "Presiona [Enter] para buscar");
            MytoolTipDataType.SetToolTip(lstObjetos, "");
        }



        private void rtfClear(object sender, EventArgs e)
        {
            rchTxt.Clear();
        }



        private void rtfSelectAll(object sender, EventArgs e)
        {
            rchTxt.SelectAll();
        }



        private void rtfSelectionToClipBoard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(rchTxt.SelectedText);
        }



        private void ObjectSelectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < lstObjetos.Items.Count; i++)
            {
                if (!lstObjetos.SelectedIndices.Contains(i))
                    lstObjetos.SelectedIndices.Add(i);
            }
        }



        private void ObjectInvertSelection(object sender, EventArgs e)
        {
            for (int i = 0; i < lstObjetos.Items.Count; i++)
            {
                if (!lstObjetos.SelectedIndices.Contains(i))
                    lstObjetos.SelectedIndices.Add(i);
                else
                    lstObjetos.SelectedIndices.Remove(i);
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



        private void ObjGetMoreInfo(object sender, EventArgs e)
        {
            if (lstObjetos.SelectedIndex == -1)
                return;

            DBObject DBObj = new DBObject(hSql);

            DBObj = (DBObject)lstObjetos.SelectedItem;
            rchTxt.AppendText(string.Format("\nSchema    : {0}\n", DBObj.schema_name));
            rchTxt.AppendText(string.Format("Nombre    : {0}\n", DBObj.name));
            rchTxt.AppendText(string.Format("Id        : {0}\n", DBObj.object_id));
            rchTxt.AppendText(string.Format("Type      : {0}\n", DBObj.type));
            rchTxt.AppendText(string.Format("Type Desc : {0}\n", DBObj.type_desc));
            rchTxt.AppendText(string.Format("Creado    : {0}\n", DBObj.create_date));
            rchTxt.AppendText(string.Format("Modificado: {0}\n", DBObj.modify_date));
            rchTxt.AppendText(string.Format("Schema Id : {0}\n", DBObj.schema_id));
            rchTxt.AppendText(string.Format("Parrent Id: {0}\n", DBObj.parent_object_id));

            rchTxt.SelectionStart = 0;
            rchTxt.SelectionLength = 0;
            rchTxt.Refresh();
            rchTxt.Select();

        }



        private void BkpObjGetText(object sender, EventArgs e)
        {
            int x = 0;

            if (lstObjetos.SelectedIndex == -1)
                return;

            FolderBrowserDialog flder = new FolderBrowserDialog();

            if (flder.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Cancelado por Usuario");
                return;
            }

            string FileContent = string.Empty;
            DBObject DBObj = new DBObject(hSql);

            foreach (DBObject ob in lstObjetos.SelectedItems)
            {
                ++x;

                toolStripProgressBar1.Value = ( 100 * x ) / lstObjetos.SelectedItems.Count;

                DBObj = ob;
                FileContent = ob.GetText();
                string fName = string.Format("{0}.{1}.sql", ob.schema_name, ob.name);
                System.IO.StreamWriter fout = new System.IO.StreamWriter(flder.SelectedPath + "\\" + fName);
                fout.Write(FileContent);
                fout.Close();
            }

            toolStripStatusLabelSel.Text = string.Format("Se extrajeron {0} objetos", x);
            System.Media.SystemSounds.Beep.Play();
            toolStripProgressBar1.Value = 0;

        }



        private void ObjGetText(object sender, EventArgs e)
        {
            int x = 0;
            if (lstObjetos.SelectedIndex == -1)
                return;

            rchTxt.Text = string.Empty;
            DBObject DBObj = new DBObject(hSql);

            foreach (DBObject ob in lstObjetos.SelectedItems)
            {
                ++x;

                toolStripProgressBar1.Value = ( 100 * x ) / lstObjetos.SelectedItems.Count;

                DBObj = ob;
                rchTxt.Text += ob.GetText();
            }

            rchTxt.SelectionStart = 0;
            rchTxt.SelectionLength = 0;
            rchTxt.Refresh();
            rchTxt.Select();

            toolStripStatusLabelSel.Text = string.Format("Se extrajeron {0} objetos", x);

            System.Media.SystemSounds.Beep.Play();
            toolStripProgressBar1.Value = 0;
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



        private void EditarDatos( object sender, EventArgs e)
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

            frmDataEdit DataEdit = new frmDataEdit( hSql, lstObjetos.Text);
            DataEdit.Show();

        }


        private void ObjGetCreateTable(object sender, EventArgs e)
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

            rchTxt.Text = DBObj.ObjGetCreateTable();

            rchTxt.SelectionStart = 0;
            rchTxt.SelectionLength = 0;
            rchTxt.Refresh();
            rchTxt.Select();
        }



        private void Load_cbObjType()
        {
            cbObjType.Items.Clear();

            ObjectTypes objt = new ObjectTypes();

            foreach (var n in objt)
            {
                cbObjType.Items.Add(n);
            }
        }



        private void btRefreshType_Click(object sender, EventArgs e)
        {
            if (cbObjType.SelectedIndex == -1)
                return;

            Objetos.Load(( (ObjectType)cbObjType.SelectedItem ).type);
            Load_lstObjetos();
        }



        public void Load_lstObjetos()
        {
            int x = 0;
            lstObjetos.Items.Clear();

            foreach (var n in Objetos)
            {
                ++x;
                toolStripProgressBar1.Value = x % 100;
                lstObjetos.Items.Add(n);
            }

            toolStripProgressBar1.Value = 100;
            System.Media.SystemSounds.Beep.Play();
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel.Text = string.Format("{0} {1} Encontrados(as)", lstObjetos.Items.Count, cbObjType.Text);
        }



        private bool IsWide()
        {
            if (this.Width == 1186)
                return true;
            else
                return false;
        }



        private void btRefreshFiltro_Click(object sender, EventArgs e)
        {
            switch (cbFiltro.SelectedIndex)
            {
                case -1: // Sin Selección
                    return;
                //break;

                case 0: // Texto
                    Objetos.FindByText(( (ObjectType)cbObjType.SelectedItem ).type, txFiltro.Text);
                    Load_lstObjetos();
                    break;

                case 1: // Columna
                    Objetos.FindByColumn(( (ObjectType)cbObjType.SelectedItem ).type, txFiltro.Text);
                    Load_lstObjetos();
                    break;
            }

            toolStripStatusLabel.Text = string.Format("{0} {1} Encontrados(as) que {2} = {3}", lstObjetos.Items.Count, cbObjType.Text, cbFiltro.Text, txFiltro.Text);

        }



        private void btFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txFind.Text))
                return;

            int pos = rchTxt.SelectionStart + rchTxt.SelectionLength;
            if (pos >= rchTxt.TextLength)
                pos = 0;

            try
            {
                pos = rchTxt.Text.IndexOf(txFind.Text, pos, rchTxt.TextLength - pos, StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return;
            }

            //pos = txtSql.Find(toolStripTextBox1.Text, pos, txtSql.TextLength, RichTextBoxFinds.None);
            if (pos == -1)
            {
                rchTxt.SelectionStart = 0;
                rchTxt.SelectionLength = 0;
            }
            else
            {
                rchTxt.SelectionStart = pos;
                rchTxt.SelectionLength = txFind.Text.Length;
            }

            rchTxt.Refresh();
            Application.DoEvents();
            rchTxt.Select();

        }



        private void txFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.Enter)
                btFind_Click(sender, e);
        }



        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void frmObjects_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            if (this.Height != WinMinHeight)
            {
                this.Height = WinMinHeight;
            }

            if (this.Width < WinMinWidth)
            {
                this.Width = WinMinWidth;
            }

            int diffH = this.Height - WinMinHeight;
            int diffW = this.Width - WinMinWidth;

            rchTxt.Width = rchTxtWidth + diffW;
        }



        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0)
                dif = sValue.Length;

            return sValue + new String(' ', dif);
        }



        private void rchTxt_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int line = rchTxt.GetLineFromCharIndex(rchTxt.SelectionStart);
                int column = rchTxt.SelectionStart - rchTxt.GetFirstCharIndexFromLine(line);

                laTextPosition.Text = StringComplete(string.Format("Fila: {0}", line), 13) + " " + StringComplete(string.Format("Col: {0}", column), 13);

            }
            catch
            {
                //Do Nothing
            }
        }

        

        private void rchTxt_KeyDown(object sender, KeyEventArgs e)
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



        private void btSaveSQL_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Sql Crypt Files (Sql Files (*.sql)|*.sql";
            ofd.FilterIndex = 1;

            Color ColorResult = Color.Green;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rchTxt.SaveFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                }
                catch
                { ColorResult = Color.Red; }
            }
            else
            {
                return;
            }

            Color colorOrig = this.BackColor;
            for (int x = 0; x < 4; ++x)
            {
                if (x != 0)
                    System.Threading.Thread.Sleep(150);

                this.BackColor = ColorResult;
                Application.DoEvents();
                System.Threading.Thread.Sleep(150);
                this.BackColor = colorOrig;
                Application.DoEvents();
            }

        }



        private void lstObjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabelSel.Text = string.Format("{0} Elem. Seleccionados", lstObjetos.SelectedItems.Count);

            if (lstObjetos.SelectedIndex == -1)
            {
                MytoolTipDataType.SetToolTip(lstObjetos, "");
                return;
            }

            var DBObj = (DBObject)lstObjetos.SelectedItem;

            if (DBObj.type.Trim() == "U")
                MytoolTipDataType.SetToolTip(lstObjetos, DBObj.description);
            
        }



        private void btLimpiaFiltro_Click(object sender, EventArgs e)
        {
            txFiltro.Text = string.Empty;
        }



        private void txBuscaEnLista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = false;

                //Busco en la lista
                for (int x = 0; x < lstObjetos.Items.Count; ++x)
                {
                    if (txBuscaEnLista.Text.Trim() != "" && lstObjetos.Items[x].ToString().ToUpper().Contains(txBuscaEnLista.Text.ToUpper()))
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

    }

}
