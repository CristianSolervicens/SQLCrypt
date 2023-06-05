using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;


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
                    MessageBox.Show("Error SQL en consulta\n\n" + Program.hSql.ErrorString, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.hSql.ErrorClear();
                }
                this.Close();
                return;
            }

            if (Program.hSql.ErrorExiste)
            {
                MessageBox.Show("Error SQL\n\n" + Program.hSql.ErrorString, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dt.Load(Program.hSql.Data);
            dataGridView.DataSource = dt;            

            toolStripTextBox1.Text = string.Format("Filas: {0}",  dataGridView.Rows.Count);

        }


        private void SetRowLines()
        {
            for (int x = 0; x < dataGridView.Rows.Count; ++x)
            {
                dataGridView.Rows[x].HeaderCell.Value = x;
            }
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }


        private void SaveToExcell()
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
            ExcelPackage pck = new ExcelPackage(newFile);

            ExcelWorksheet wks = pck.Workbook.Worksheets.Add("Reporte");

            for (int fila = 0; fila < dataGridView.Rows.Count; ++fila)
            {

                if (fila == 0)
                {
                    for (int i = 0; i < dataGridView.Columns.Count; ++i)
                    {
                        wks.Cells[fila + 1, i + 1].Value = dataGridView.Columns[i].HeaderText;
                    }

                    using (var range = wks.Cells[1, 1, 1, dataGridView.Columns.Count])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                }

                for (int i = 0; i < dataGridView.Columns.Count; ++i)
                {
                    wks.Cells[fila + 2, i + 1].Value = dataGridView.Rows[fila].Cells[i].FormattedValue.ToString();
                }

                using (var range = wks.Cells[fila + 2, 1, fila + 2, dataGridView.Columns.Count])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (fila % 2 == 0)
                        range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                    else
                        range.Style.Fill.BackgroundColor.SetColor(Color.AliceBlue);
                }
            }

            //wks.Cells.AutoFitColumns(0);
            wks.View.PageLayoutView = false;
            pck.Save();

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
            SaveToExcell();
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