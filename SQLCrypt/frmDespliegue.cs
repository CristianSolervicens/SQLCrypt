using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using OfficeOpenXml.Style;
using System.Drawing;


namespace SQLCrypt
{
    public partial class frmDespliegue:Form
    {

        public frmDespliegue()
        {
            InitializeComponent();

            btSalir.Top = -20;
        }

        private void frmDespliegue_Load(object sender, EventArgs e)
        {

            if (Program.hSql.Data == null)
            {
                if (Program.hSql != null && Program.hSql.ErrorExiste)
                {
                    MessageBox.Show($"Error SQL en consulta\n\n{Program.hSql.ErrorString}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.hSql.ErrorClear();
                }
                this.Close();
                return;
            }

            if (Program.hSql.ErrorExiste)
            {
                MessageBox.Show($"Error SQL\n\n{Program.hSql.ErrorString}" , "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.hSql.ErrorClear();
                this.Close();
                return;
            }

            try
            {
                if (!Program.hSql.Data.HasRows)
                {
                    if (Program.hSql.Messages != "")
                    {
                        MessageBox.Show(String.Format("No hay resultdos para su consulta\n\n{0}", Program.hSql.Messages),
                            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Program.hSql.ErrorClear();
                        Program.hSql.ClearMessages();
                        this.Close();
                        return;
                    }
                    else 
                    {
                        MessageBox.Show("No hay resultados para su consulta\n\n", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.hSql.ErrorClear();
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
            DataTable dt = new DataTable();
            try
            {
                dt.Load(Program.hSql.Data);
            }
            catch
            {
                MessageBox.Show("La tabla tiene tipos de datos No soportados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dataGridView.DataSource = dt;
                toolStripTextBox1.Text = string.Format("Filas: {0}", dataGridView.Rows.Count);
            }
        }


        private void SetRowLines()
        {
            for (int x = 0; x < dataGridView.Rows.Count; ++x)
            {
                dataGridView.Rows[x].HeaderCell.Value = x;
            }
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
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
            var dt = (DataTable)dataGridView.DataSource;
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
            Program.hSql.DataClose();
        }

        //------------------------
        //frmDespliegue_Closed
        //------------------------
        private void frmDespliegue_Closed(object sender, FormClosedEventArgs e)
        {
            Program.hSql.DataClose();
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grabarExcellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToExcel();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void verMensajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.hSql.Messages == "" || string.IsNullOrEmpty(Program.hSql.Messages))
                MessageBox.Show("No hay mensajes");
            else
            {
                MessageBox.Show(Program.hSql.Messages);
                Clipboard.Clear();
                Clipboard.SetText( Program.hSql.Messages, TextDataFormat.Text);
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
    }

}