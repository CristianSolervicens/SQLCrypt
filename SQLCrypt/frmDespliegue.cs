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


namespace SQLCrypt
{
    public partial class frmDespliegue:Form
    {

        private DataSet ds = new DataSet();
        int current_ds = -1;
        public MySql hSql = null;
        Dictionary<string, string> DictParam = null;
        

        public frmDespliegue(MySql _hSql)
        {
            this.hSql = _hSql;
            InitializeComponent();
            btSalir.Top = -200;
        }


        public frmDespliegue(string connectionString, string Database, string commandString, Dictionary<string,string> DictParam)
        {
            hSql = new MySql();
            hSql.ConnectionString = connectionString;
            hSql.ConnectToDB();
            hSql.SetDatabase(Database);
            
            InitializeComponent();

            btSalir.Top = -200;

            Program.hSqlQuery = hSql;
            int spid = hSql.GetCurrent_SPID();
            Program.sql_spid = spid;


            if (DictParam == null)
                hSql.ExecuteSqlData(commandString);
            else
                hSql.ExecCmdDataWithParam(commandString, DictParam);

            if (hSql.Data == null)
            {
                if (hSql.ErrorExiste)
                {
                    MessageBox.Show(this, $"Error SQL {hSql.ErrorString}\r\n{hSql.Messages}", "Atención", MessageBoxButtons.OK);
                    hSql.ErrorClear();
                    this.Close();
                    return;
                }

                MessageBox.Show("No hay resultados para su consulta\r\nMensajes\r\n" + hSql.Messages);
                Clipboard.Clear();
                Clipboard.SetText(hSql.Messages, TextDataFormat.Text);
                this.Close();
                return;
            }

        }


        private void frmDespliegue_Load(object sender, EventArgs e)
        {

            try
            {
                if (!hSql.Data.HasRows)
                {
                    if (hSql.Messages != "")
                    {
                        MessageBox.Show(String.Format("No hay resultdos para su consulta\n\n{0}", hSql.Messages),
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        hSql.ErrorClear();
                        hSql.ClearMessages();
                        this.Close();
                        return;
                    }
                    else 
                    {
                        MessageBox.Show("No hay resultados para su consulta\n\n", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hSql.ErrorClear();
                        this.Close();
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error SQL no Administrado\n", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            //Carga de Filas en la Grilla.
            try
            {
                do
                {
                    try
                    {
                        if (Program.CancelQuery)
                        {
                            this.Close();
                            return;
                        }
                        DataTable dt = new DataTable();
                        if (Program.CancelQuery)
                        {
                            this.Close();
                            return;
                        }
                        dt.Load(hSql.Data);
                        if (Program.CancelQuery)
                        {
                            this.Close();
                            return;
                        }
                        ds.Tables.Add(dt);

                        if (Program.sql_spid != 0 )
                            Program.sql_spid = 0;
                    }
                    catch( Exception ex)
                    {
                        MessageBox.Show($"Error\r\n{ex.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                } while (!hSql.Data.IsClosed);

                current_ds = 0;
                if (ds.Tables.Count > 0)
                    dataGridView.DataSource = ds.Tables[current_ds];

                toolStripTextBox1.Text = string.Format($"Filas: {dataGridView.Rows.Count}  Result Set {current_ds + 1}/{ds.Tables.Count}");
                
                Program.hSqlQuery = null;

                this.Show();
                this.Activate();
                this.BringToFront();
            }
            catch {
                this.Close();
            }
        }


        /// <summary>
        /// Número de Fila en El Header de Las Filas
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
            // Selección del Archivo de Salida
            SaveFileDialog saveForm = new SaveFileDialog();
            saveForm.RestoreDirectory = true;
            saveForm.Filter = "Json File|*.json";
            saveForm.Title = "Save As Json File";
            saveForm.ShowDialog();

            if (saveForm.FileName == "")
                return;

            if (File.Exists(saveForm.FileName))
            {
                try
                {
                    File.Delete(saveForm.FileName);
                }
                catch
                {
                    MessageBox.Show("No fue posible eliminar el Archivo existente.\nVerifique que no esté en uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            ds.Tables[current_ds].SaveToFile(saveForm.FileName);

        }


        //-------------------------------
        // Grabación de la Salida a Excel
        //-------------------------------
        private void SaveToExcel()
        {

            // Selección del Archivo de Salida
            SaveFileDialog saveForm = new SaveFileDialog();
            saveForm.RestoreDirectory = true;
            saveForm.Filter = "Excell File|*.xlsx";
            saveForm.Title = "Save As Excell File";
            saveForm.ShowDialog();

            if (saveForm.FileName == "")
                return;

            if (File.Exists(saveForm.FileName))
            {
                try
                {
                    File.Delete(saveForm.FileName);
                }
                catch
                {
                    MessageBox.Show("No fue posible eliminar el Archivo existente.\nVerifique que no esté en uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
                

            FileInfo newFile = new FileInfo(saveForm.FileName);
            
            // Obtener Formato de Fecha desde el Sistema
            string dateFormat = $"{CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern} HH:MM:SS";
            //string dateFormat = "MM-dd-yyyy HH:MM:SS";

            // Salida a Excel
            var dt = ds.Tables[current_ds];
            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");
                ws.Cells["A1"].LoadFromDataTable( dt, true);
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
            Program.sql_spid = 0;
        }

        //------------------------
        //frmDespliegue_Closed
        //------------------------
        private void frmDespliegue_Closed(object sender, FormClosedEventArgs e)
        {
            // Program.hSql.DataClose();
            foreach (DataTable dt in ds.Tables)
                dt.Clear();

            ds.Clear();
            ds.Dispose();
            this.dataGridView.Dispose();
            Program.hSqlQuery = null;
            Program.sql_spid = 0;
            Program.CancelQuery = false;
            System.GC.Collect();
        }


        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grabarExcellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToExcel();
            System.GC.Collect();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void verMensajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hSql.Messages == "" || string.IsNullOrEmpty(hSql.Messages))
                MessageBox.Show("No hay mensajes");
            else
            {
                MessageBox.Show(hSql.Messages);
                Clipboard.Clear();
                Clipboard.SetText( hSql.Messages, TextDataFormat.Text);
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }



        private void siguienteResultSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextResultSet();
        }


        private void previoResultSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousResultSet();
        }


        private void nextResultSet()
        {
            if (ds.Tables.Count > current_ds+1)
                current_ds++;

            dataGridView.DataSource = ds.Tables[current_ds];
            toolStripTextBox1.Text = string.Format($"Filas: {dataGridView.Rows.Count}  Result Set {current_ds+1}/{ds.Tables.Count}");
        }


        private void previousResultSet()
        {
            if (current_ds > 0)
                current_ds--;
            
            dataGridView.DataSource = ds.Tables[current_ds];
            toolStripTextBox1.Text = string.Format($"Filas: {dataGridView.Rows.Count}  Result Set {current_ds+1}/{ds.Tables.Count}");
        }

        private void grabarJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveCurrentToJson();
            System.GC.Collect();
        }

    }

}