using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLCrypt.FunctionalClasses.MySql;

namespace SQLCrypt
{
    public partial class frmDespliegueTxt: Form
    {

        private MySql hSql;
        private string      TextFile;
        private int         WinMinHeight = 0;
        private int         WinMinWidth = 0;
        private int         txtSqlHeightOrig = 0;
        private int         txtSqlWidthhtOrig = 0;

        private bool Cancelar
        {
            get;
            set;
        }

        public frmDespliegueTxt(MySql hSql)
        {
            InitializeComponent();
            laInfo.Text = "";
            this.hSql = hSql;
            this.rtchSalida.Text = string.Empty;
            this.btSalir.Top = -20;
            this.btCancelar.Enabled = false;
            this.Cancelar = false;

            WinMinHeight = this.Height;
            WinMinWidth = this.Width;

            txtSqlHeightOrig = rtchSalida.Height;
            txtSqlWidthhtOrig = rtchSalida.Width;
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0)
                return sValue.Substring(0, length);

            return sValue + new String(' ', dif);
        }

        public void ExecuteSQLStatement(string MyComandoSQL, int TextLimit)
        {

            int query = 0;
            int start = 0;
            int pos;
            string ComandoSQL = "";
            string Salida = "";
            int LinSalida = 0;
            if (TextLimit == 0 || TextLimit > 512)
                TextLimit = 512;

            this.Cancelar = false;
            this.btCancelar.Enabled = true;
            while (true)
            {
                ++query;
                pos = MyComandoSQL.IndexOf("\nGO", start, StringComparison.InvariantCultureIgnoreCase);
                if (pos == -1) pos = MyComandoSQL.Length;

                if (start >= MyComandoSQL.Length)
                {
                    this.rtchSalida.Text += Salida;
                    Salida = "";
                    LinSalida = 0;
                    return;
                }

                if (( pos - start ) <= 0)
                    return;

                ComandoSQL = MyComandoSQL.Substring(start, pos - start);

                if (string.IsNullOrEmpty(ComandoSQL) || string.IsNullOrWhiteSpace(ComandoSQL))
                {
                    this.btCancelar.Enabled = false;
                    return;
                }

                if (!hSql.ExecuteSqlData(ComandoSQL))
                {
                    if (! string.IsNullOrWhiteSpace(hSql.ErrorString) && string.IsNullOrWhiteSpace( hSql.Messages) )
                    this.rtchSalida.Text += "\n\n*** MENSAJES ***\n\n";
                    this.rtchSalida.Text += hSql.ErrorString;
                    this.rtchSalida.Text += hSql.Messages;
                    return;
                }

                //this.rtchSalida.Text += string.Format("\n( Q u e r y   {0} )\n\n", query);
                //Salida += string.Format("\n( Q u e r y   {0} )\n\n", query);
                //++LinSalida;

                if (hSql.ErrorExiste)
                {
                    this.rtchSalida.Text += Salida;
                    Salida = "";
                    LinSalida = 0;
                    this.rtchSalida.Text += hSql.ErrorString;
                    this.btCancelar.Enabled = false;
                    return;
                }

                try
                {

                    do  //ResultSet
                    {
                        int row = 0;
                        List<int> lenC = new List<int>();
                        while (hSql.Data.Read())  //Filas de ResultSet
                        {
                            Application.DoEvents();

                            if (this.Cancelar)
                            {
                                laInfo.Text = string.Format("Filas Afectadas : {0}", row);
                                MessageBox.Show("Query cancelada por Usuario");
                                Salida += this.rtchSalida.Text += "\n\n(Cancelado por Usuario)";
                                break;
                            }

                            //Encabezados Primera Fila
                            if (row == 0)
                            {
                                Salida += "\n";
                                string guiones = "";

                                for (int x = 0; x < hSql.Data.FieldCount; ++x)
                                {

                                    int size = Convert.ToInt32(hSql.Data.GetSchemaTable().Rows[x][3]);
                                    //int size = Convert.ToInt32(hSql.Data.GetSchemaTable().Rows[x]["ColumnSize"]);

                                    if (size > TextLimit && TextLimit > 0)
                                        size = TextLimit;

                                    if (size < hSql.Data.GetName(x).Length)
                                        size = hSql.Data.GetName(x).Length;

                                    lenC.Add(size + 1);
                                    guiones += ((x > 0) ? " " : "") + new String('-', lenC[x]);

                                    //this.rtchSalida.Text += ((x > 0) ? " " : "") + StringComplete(hSql.Data.GetName(x), lenC[x]);
                                    Salida += ((x > 0) ? " " : "") + StringComplete(hSql.Data.GetName(x), lenC[x]);
                                    ++LinSalida;
                                }

                                //this.rtchSalida.Text += "\n" + guiones + "\n";
                                Salida += "\n" + guiones + "\n";
                                ++LinSalida;
                            }


                            for (int x = 0; x < hSql.Data.FieldCount; ++x) //Columnas
                            {
                                if (hSql.Data.IsDBNull(x))
                                {
                                    //this.rtchSalida.Text += ((x > 0) ? " " : "") + StringComplete("null", lenC[x]);
                                    Salida += ((x > 0) ? " " : "") + StringComplete("null", lenC[x]);
                                    ++LinSalida;
                                }
                                else
                                {
                                    //this.rtchSalida.Text += ((x > 0) ? " " : "") + StringComplete(Convert.ToString(hSql.Data[x]), lenC[x]);
                                    Salida += ((x > 0) ? " " : "") + StringComplete(Convert.ToString(hSql.Data[x]), lenC[x]);
                                    ++LinSalida;
                                }
                            }
                            //this.rtchSalida.Text += "\n";
                            Salida += "\n";
                            ++LinSalida;
                            ++row;

                            if (LinSalida > 200)
                            {
                                laInfo.Text = string.Format("Filas Afectadas : {0}", row);
                                this.rtchSalida.Text += Salida;
                                Salida = "";
                                LinSalida = 0;
                            }

                        }

                        //this.rtchSalida.Text += string.Format("\n( {0} Filas afectadas)\n", row);
                        //Salida += string.Format("\n( {0} Filas afectadas)\n", row);
                        ++LinSalida;

                        DisplayMensaje();

                        //this.rtchSalida.Text += "\n";

                        if (this.Cancelar)
                            break;


                    } while (hSql.Data.NextResult());

                }
                catch( Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                if (pos >= MyComandoSQL.Length || this.Cancelar)
                    break;

                start = pos + 3;
            }

            this.btCancelar.Enabled = false;

            if (!string.IsNullOrEmpty( hSql.Messages) )
            {
                this.rtchSalida.Text += Salida + "\n\n *** Mensajes *** \n\n" + hSql.Messages;
            }
            Salida = "";
            LinSalida = 0;

        }


        private void DisplayMensaje()
        {
            if (!string.IsNullOrWhiteSpace(hSql.Messages) )
            {
                this.rtchSalida.Text += "\n\n*** Mensajes ***\n\n";
                this.rtchSalida.Text += hSql.Messages;
                hSql.ClearMessages();
            }
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txFind.Text))
            {
                txFind.Select();
                return;
            }

            if (string.IsNullOrWhiteSpace(txFind.Text))
                return;

            int pos = rtchSalida.SelectionStart + rtchSalida.SelectionLength;

            pos = rtchSalida.Find(txFind.Text, pos, rtchSalida.TextLength, RichTextBoxFinds.None);
            if (pos == -1)
            {
                rtchSalida.SelectionStart = 0;
                rtchSalida.SelectionLength = 0;
            }
            rtchSalida.Refresh();
            Application.DoEvents();
            rtchSalida.Select();
        }

        private void txFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                btFind_Click(sender, e);
        }

        private void rtchSalida_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                btFind_Click(sender, e);
        }

        private void rtchSalida_KeyDown(object sender, KeyEventArgs e)
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

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Cancelar = true;
            this.btCancelar.Enabled = false;
        }

        private void frmDespliegueTxt_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            if (this.Height < WinMinHeight)
            {
                this.Height = WinMinHeight;
                rtchSalida.Height = txtSqlHeightOrig;
            }

            if (this.Width < WinMinWidth)
            {
                this.Width = WinMinWidth;
                rtchSalida.Width = txtSqlWidthhtOrig;
            }

            rtchSalida.Height = txtSqlHeightOrig + ( this.Height - WinMinHeight );
            rtchSalida.Width = txtSqlWidthhtOrig + ( this.Width - WinMinWidth );
        }

        private void btGrabar_Click(object sender, EventArgs e)
        {
            SaveFile();
        }


        private void SaveFile()
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.LocalUserAppDataPath;
            sfd.Filter = "Text Files (*.txt)|*.txt";
            sfd.FilterIndex = 4;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                TextFile = sfd.FileName;
            }
            else
            {
                MessageBox.Show(this, "Cancelado por usuario", "Cancelado", MessageBoxButtons.OK);
                return;
            }
            
            rtchSalida.SaveFile(TextFile, RichTextBoxStreamType.PlainText);
        }

    }
}
