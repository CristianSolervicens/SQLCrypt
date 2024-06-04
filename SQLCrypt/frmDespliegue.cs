using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using System.Globalization;


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
            SaveFileDialog saveForm = new SaveFileDialog();
            saveForm.RestoreDirectory = true;
            saveForm.Filter = "Excell File|*.xlsx";
            saveForm.Title = "Save As Excell File";
            saveForm.ShowDialog();

            if (saveForm.FileName == "")
                return;

            if (File.Exists(saveForm.FileName))
                File.Delete(saveForm.FileName);

            FileInfo newFile = new FileInfo(saveForm.FileName);


            string dateFormat = $"{CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern} HH:MM:SS";
            //string dateFormat = "MM-dd-yyyy HH:MM:SS";
            var dt = (DataTable)dataGridView.DataSource;
            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");
                ws.Cells["A1"].LoadFromDataTable( dt, true);

                ws.Cells.AutoFitColumns();
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Columns[c].DataType == typeof(DateTime))
                    {
                        ws.Column(c + 1).Style.Numberformat.Format = dateFormat;
                    }
                }

                pck.Save();
            }
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