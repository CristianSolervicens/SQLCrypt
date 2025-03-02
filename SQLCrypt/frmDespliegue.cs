using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Collections.Generic;
using SQLCrypt.FunctionalClasses;
using SQLCrypt.FunctionalClasses.MySql;
using static ScintillaNET.Style;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;


namespace SQLCrypt
{
    public partial class frmDespliegue:Form
    {

        private bool withTread = false;
        private DataSet ds = new DataSet();
        int current_ds = -1;
        public MySql hSql = null;
        Dictionary<string, string> DictParam = null;
        List<string> sComandos = new List<string>();
        List<string> listMensajes = new List<string>();
        List<string> listErrores = new List<string>();

        const string EOL = "\r\n";
        const string SHORTCUT_MSG = "\r\n\r\n[Ctrl] + [M] - To Hide this Messages\r\n\r\n[Ctrl] + [A] - Auto Adjust Column Width\r\n[Ctrl] + [Z] - Manual Adjust Column Width\r\n       [Esc] - Close Results Window";

        const int MESSAGE_WIDTH = 760;
        const int MESSAGE_HEIGHT = 450;
        const int ROW_HEADER_WIDTH = 55;


        /// <summary>
        /// frmDespliegue   CONSTRUCTOR SIN THREAD
        /// </summary>
        /// <param name="_hSql"></param>
        public frmDespliegue(MySql _hSql)
        {
            this.hSql = _hSql;
            InitializeComponent();
            txtMessages.Visible = false;
            txtMessages.SendToBack();
            btSalir.Top = -200;
            laMessages.Text = "";
        }


        /// <summary>
        /// frmDespliegue   CONSTRUCTOR CON THREAD
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="Database"></param>
        /// <param name="commandString"></param>
        /// <param name="DictParam"></param>
        public frmDespliegue(string connectionString, string Database, string commandString, Dictionary<string, string> DictParam)
        {
            InitializeComponent();
            withTread = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            // dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;

            txtMessages.Visible = false;
            txtMessages.SendToBack();

            btSalir.Top = -200;
            laMessages.Text = "";

            hSql = new MySql();
            hSql.ConnectionString = connectionString;
            hSql.ConnectToDB();
            hSql.UseDatabase(Database);

            QueryController.hSqlQuery = hSql;
            int spid = hSql.GetCurrent_SPID();
            QueryController.sql_spid = spid;
            QueryController.InQuery = false;

            if (QueryController.CancelQuery)
            {
                return;
            }

            sComandos = MyFuncs.ParseSqlCommandGO(commandString);

            foreach (string comando in sComandos)
            {
                if (QueryController.CancelQuery) 
                    return;

                if (DictParam != null)
                    hSql.ExecCmdDataWithParam(comando, DictParam);
                else
                    hSql.ExecuteSqlData(comando);

                if (hSql.Data == null)
                {
                    if (hSql.ErrorExiste)
                    {
                        listErrores.Add(hSql.ErrorString);
                        hSql.ErrorClear();
                        return;
                    }

                    string currentDb = hSql.GetCurrentDatabase();
                    if (currentDb != "")
                        QueryController.DataBase = currentDb;
                }
                else
                {
                    try
                    {
                        if (hSql.Data.FieldCount == 0)
                        {
                            if (hSql.ErrorString != "")
                                listErrores.Add(hSql.ErrorString);
                            if (hSql.Messages != "")
                                listMensajes.Add(hSql.Messages);

                            continue;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Non Administered SQL Error\n", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    try
                    {
                        LoadData();
                        
                        if (QueryController.CancelQuery)
                        {
                            if (hSql.ErrorString != "")
                                listErrores.Add(hSql.ErrorString);
                            if (hSql.Messages != "")
                                listMensajes.Add(hSql.Messages);

                            return;
                        }
                    }
                    catch { }
                }

                if (hSql.ErrorString != "")
                    listErrores.Add(hSql.ErrorString);
                if (hSql.Messages != "")
                    listMensajes.Add(hSql.Messages);
            }

            if (QueryController.sql_spid != 0)
                QueryController.sql_spid = 0;

            laMessages.Text = "";
            laMessages.Text = (listErrores.Count > 0 ? "Errors" : ""); 
            laMessages.Text += (laMessages.Text != ""? "; ": "") + (listMensajes.Count > 0 ? "Messages" : "");
            
            //Mostrar Mensajes si hay
            if (listErrores.Count + listMensajes.Count > 0)
            {
                txtMessages.Visible = false;
                txtMessages.SendToBack();
                verMensajesToolStripMenuItem_Click(null, null);
            }
        }


        /// <summary>
        /// frmDespliegue_Load    LOAD DEL FORMULARIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDespliegue_Load(object sender, EventArgs e)
        {

            string currentDb = "";

            if (QueryController.CancelQuery && withTread)
            {
                currentDb = hSql.GetCurrentDatabase();
                if (currentDb != "")
                    QueryController.DataBase = currentDb;

                hSql.ErrorClear();
                hSql.ClearMessages();
                this.Close();
                return;
            }

            if (sComandos.Count == 0)
            {
                try
                {
                    if (hSql.Data.FieldCount == 0)
                    {
                        if (hSql.Messages != "")
                        {
                            listMensajes.Add(hSql.Messages);
                            hSql.ErrorClear();
                            hSql.ClearMessages();
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Non Administered SQL Error\n", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                //Carga de Filas en la Grilla.
                try
                {
                    LoadData();
                    if (QueryController.CancelQuery && withTread)
                    {
                        this.Close();
                        return;
                    }
                }
                catch { }


                //Mostrar Mensajes si hay
                if (listErrores.Count + listMensajes.Count > 0)
                {
                    txtMessages.Visible = false;
                    txtMessages.SendToBack();
                    verMensajesToolStripMenuItem_Click(null, null);
                }
            }

            //
            // DESPLIEGUE DE DATOS
            current_ds = 0;
            if (ds.Tables.Count > 0)
            {
                dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dataGridView.RowHeadersVisible = false;
                dataGridView.AutoSize = false;
                
                dataGridView.DataSource = ds.Tables[current_ds];
                
                dataGridView.RowHeadersVisible = true;
                dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dataGridView.RowHeadersWidth = ROW_HEADER_WIDTH;
                dataGridView.Refresh();
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView.Refresh();
            }
            else
            {
                MessageBox.Show("No results for your SQL Command", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (listErrores.Count == 0 && listMensajes.Count == 0)
                {
                    this.Close();
                    return;
                }
            }

            if (ds.Tables.Count > 0)
                toolStripTextBox1.Text = string.Format($"Rows: {dataGridView.Rows.Count}  Result Set {current_ds + 1}/{ds.Tables.Count}");
            else
                toolStripTextBox1.Text = string.Format($"Rows: {dataGridView.Rows.Count}  Result Set 0/0");

            if (withTread)
            {
                currentDb = hSql.GetCurrentDatabase();
                if (currentDb != "")
                    QueryController.DataBase = currentDb;

                QueryController.hSqlQuery = null;
                QueryController.InQuery = false;
            }

            //Mostrar Mensajes si hay
            if (listErrores.Count + listMensajes.Count > 0)
            {
                txtMessages.Visible = false;
                txtMessages.SendToBack();
                verMensajesToolStripMenuItem_Click(null, null);
            }

            this.Show();
            this.Activate();
            this.BringToFront();
        }


        /// <summary>
        /// Carga los Result-Sets en el DataSet
        /// </summary>
        public void LoadData()
        {
            try
            {
                while (hSql.Data.FieldCount != 0)
                {
                    try
                    {
                        if (QueryController.CancelQuery && withTread)
                            return;

                        DataTable dt = new DataTable();
                        dt.Load(hSql.Data);
                        
                        if (QueryController.CancelQuery && withTread)
                            return;

                        ds.Tables.Add(dt);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error\r\n{ex.Message}", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    if (hSql.Data.IsClosed)
                        return;
                }
            }
            catch
            {
                return;
            }
        }



        /// <summary>
        /// N�mero de Fila en El Header de Las Filas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }


        /// <summary>
        /// Grabar el Contenido del la Tabla Actual a archivo JSON
        /// </summary>
        private void saveCurrentToJson()
        {
            // Selecci�n del Archivo de Salida
            using (SaveFileDialog saveForm = new SaveFileDialog())
            {
                saveForm.RestoreDirectory = true;
                saveForm.Filter = "Json File|*.json";
                saveForm.Title = "Save As Json File";
                saveForm.ShowDialog();

                if (saveForm.FileName == "")
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                if (File.Exists(saveForm.FileName))
                {
                    try
                    {
                        File.Delete(saveForm.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Cannot delete existing File.\nVerify is not in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                ds.Tables[current_ds].SaveToFile(saveForm.FileName);
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Archivo {saveForm.FileName} grabado", "Informaci�n", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //-------------------------------
        // Grabaci�n de la Salida a Excel
        //-------------------------------
        private void SaveToExcel()
        {

            // Selecci�n del Archivo de Salida
            using (SaveFileDialog saveForm = new SaveFileDialog())
            {
                saveForm.RestoreDirectory = true;
                saveForm.Filter = "Excell File|*.xlsx";
                saveForm.Title = "Save As Excell File";
                saveForm.ShowDialog();

                if (saveForm.FileName == "")
                {
                    return;
                }


                if (File.Exists(saveForm.FileName))
                {
                    try
                    {
                        File.Delete(saveForm.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Cannot delete existing File.\nVerify is not in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;
                FileInfo newFile = new FileInfo(saveForm.FileName);

                // Obtener Formato de Fecha desde el Sistema
                string dateFormat = $"{CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern} HH:MM:SS";
                //string dateFormat = "MM-dd-yyyy HH:MM:SS";

                // Salida a Excel
                var dt = ds.Tables[current_ds];
                using (ExcelPackage pck = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Columns[c].DataType == typeof(DateTime))
                        {
                            ws.Column(c + 1).Style.Numberformat.Format = dateFormat;
                        }
                    }

                    // Color de Fondo de las Filas de Dato
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var row = ws.Cells[i + 2, 1, i + 2, dt.Columns.Count];
                        row.Style.Font.Color.SetColor(Color.Black);
                        if (i % 2 != 0)
                        {
                            row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            row.Style.Fill.BackgroundColor.SetColor(Color.AliceBlue);

                        }
                        ApplyBorders(row, Color.LightGray);
                    }

                    // Formateo de Header + Auto Filter, y Freeze de la primera Fila
                    using (var range = ws.Cells[1, 1, 1, dt.Columns.Count])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
                        range.Style.Font.Color.SetColor(Color.AliceBlue);
                        range.Style.Font.Bold = true;
                        ApplyBorders(range, Color.Black);
                    }

                    // Apply auto filter to the first row
                    ws.Cells[1, 1, 1, dt.Columns.Count].AutoFilter = true;

                    // Freeze the first row
                    ws.View.FreezePanes(2, 1);

                    ws.Cells.AutoFitColumns();

                    pck.Save();
                    pck.Dispose();
                }
                dt = null;
                System.GC.Collect();
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Archivo [{newFile.Name}] grabado", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Helper para la Salida a Excel
        /// </summary>
        /// <param name="range"></param>
        /// <param name="color"></param>
        static void ApplyBorders(ExcelRange range, Color color)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Top.Color.SetColor(color);
            range.Style.Border.Bottom.Color.SetColor(color);
            range.Style.Border.Left.Color.SetColor(color);
            range.Style.Border.Right.Color.SetColor(color);
        }


        //------------------------
        //frmDespliegue_Closing
        //------------------------
        private void frmDespliegue_Closing(object sender, FormClosingEventArgs e)
        {
            // Program.hSql.DataClose();
            // Parent.LoadDatabaseList();
            if (withTread)
                QueryController.sql_spid = 0;

            foreach (DataTable dt in ds.Tables)
            {
                dt.Clear();
                dt.Dispose();
            }

            ds.Clear();
            ds.Dispose();

            try
            {
                dataGridView.Dispose();
                dataGridView = null;
            }
            catch { }
        }


        //------------------------
        //frmDespliegue_Closed
        //------------------------
        private void frmDespliegue_Closed(object sender, FormClosedEventArgs e)
        {
            if (withTread)
            {
                try{hSql.Data.Close();}catch { }
                hSql.Data = null;
                try { hSql.CloseDBConn(); }catch { }
                QueryController.hSqlQuery = null;
                QueryController.sql_spid = 0;
                QueryController.CancelQuery = false;
            }
            System.GC.Collect();
        }


        /// <summary>
        /// btSalir_Click   -   BOT�N DE SALIDA DEL FORMULARIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// grabarExcellToolStripMenuItem_Click    GRABAR SALIDA A EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grabarExcellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToExcel();
            System.GC.Collect();
        }


        /// <summary>
        /// salirToolStripMenuItem_Click    -    CERRAR FORMULARIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// verMensajesToolStripMenuItem_Click   MUESTRA/OCULTA EL PANEL DE MENSAJES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verMensajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtMessages.Visible)
                HideMessages();
            else
                ShowMessages();

            if (txtMessages.Visible)
                txtMessages.BringToFront();
            else
                txtMessages.SendToBack();

            if (listMensajes.Count + listErrores.Count > 0 )
            {
                string msg;
                msg = "\r\n";
                foreach (var error in listErrores)
                    msg += error + "\r\n\r\n";
                foreach (var error in listMensajes)
                    msg += error + "\r\n\r\n";

                msg += SHORTCUT_MSG;
                txtMessages.Text = msg;
            }
            else
            {
                txtMessages.Text = $"{EOL}{EOL}      There are no Errors or Messages{EOL}{EOL}" + SHORTCUT_MSG;
            }
        }


        /// <summary>
        /// ShowMessages
        /// </summary>
        private void ShowMessages()
        {
            txtMessages.Visible = true;
            txtMessages.BringToFront();
        }


        /// <summary>
        /// HideMessages
        /// </summary>
        private void HideMessages()
        {   
            txtMessages.Visible = false;
            txtMessages.SendToBack();
        }


        /// <summary>
        /// dataGridView_DataError
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }


        /// <summary>
        /// siguienteResultSetToolStripMenuItem_Click    MENU SIGUIENTE RESULT-SET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siguienteResultSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextResultSet();
        }


        /// <summary>
        /// previoResultSetToolStripMenuItem_Click    MENU PREVIO RESULT-SET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previoResultSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousResultSet();
        }


        /// <summary>
        /// nextResultSet      IR AL RESULT SET SIGUIENTE
        /// </summary>
        private void nextResultSet()
        {
            if (ds.Tables.Count > current_ds+1)
                current_ds++;

            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AutoSize = false;

            dataGridView.DataSource = ds.Tables[current_ds];
            toolStripTextBox1.Text = string.Format($"Rows: {dataGridView.Rows.Count}  Result Set {current_ds+1}/{ds.Tables.Count}");

            dataGridView.RowHeadersVisible = true;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView.RowHeadersWidth = ROW_HEADER_WIDTH;
            dataGridView.Refresh();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.Refresh();
        }



        /// <summary>
        /// previousResultSet    IR AL RESULTSET PREVIO
        /// </summary>
        private void previousResultSet()
        {
            if (current_ds > 0)
                current_ds--;

            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AutoSize = false;

            dataGridView.DataSource = ds.Tables[current_ds];
            toolStripTextBox1.Text = string.Format($"Rows: {dataGridView.Rows.Count}  Result Set {current_ds+1}/{ds.Tables.Count}");

            dataGridView.RowHeadersVisible = true;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridView.RowHeadersWidth = ROW_HEADER_WIDTH;
            dataGridView.Refresh();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.Refresh();
        }


        /// <summary>
        /// grabarJSONToolStripMenuItem_Click    Men� grabar a JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grabarJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveCurrentToJson();
            System.GC.Collect();
        }


        /// <summary>
        /// btBuscar_Click      BUTTON FIND VALUE IN TH GRID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBuscar_Click(object sender, EventArgs e)
        {
            BuscarEnGrilla();
        }


        private void BuscarEnGrilla()
        {
            if (txtSearch.Text == string.Empty)
            {
                MessageBox.Show("You mus set the search term...", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearch.Select();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            
            int selRow = 0;
            int selCol = 0;
            bool found = false;
            if (dataGridView.SelectedCells.Count > 0)
            {
                selRow = dataGridView.SelectedCells[0].RowIndex;

                if (dataGridView.SelectedCells[0].ColumnIndex + 1 < dataGridView.Columns.Count)
                    selCol = dataGridView.SelectedCells[0].ColumnIndex + 1;
                else
                    selRow++;

            }
            else
            {
                dataGridView.Rows[selRow].Cells[selCol].Selected = true;
            }

            dataGridView.MultiSelect = false;

            for (int row = selRow; row < dataGridView.Rows.Count; row++)
            {
                for (int col = selCol; col < dataGridView.Columns.Count; col++)
                {
                    if (dataGridView.Rows[row].Cells[col].Value.ToString().ToLower().Contains(txtSearch.Text.ToLower()))
                    {
                        dataGridView.Rows[row].Cells[col].Selected = true;
                        found = true;
                        dataGridView.Select();
                        dataGridView.MultiSelect = true;
                        dataGridView.Rows[row].Cells[col].Selected = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
                selCol = 0;
            }

            dataGridView.MultiSelect = true;

            Cursor.Current = Cursors.Default;

            if (!found)
            {
                MessageBox.Show("No matching value...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView.Select();
            }
        }


        private void autoSizeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Refresh();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.Refresh();
        }


        private void manualSizeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.Refresh();
        }


        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarEnGrilla();
        }

    }

}