using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLCrypt.StructureClasses;
using SQLCrypt.FunctionalClasses.MySql;

namespace SQLCrypt
{
    
    public partial class frmExecFiles : Form
    {
        public MySql hSql { get; set; }
        public RichTextBox RTEXT_Salida { get; set; }
        
        internal List<myFile> myFiles = new List<myFile>();

        public frmExecFiles()
        {
            InitializeComponent();
        }

        private void btSelFolders_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Script Files(*.sql)|*.sql";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            lstFiles.Items.Clear();
            myFiles.Clear();
            foreach (var f in ofd.FileNames)
            {
                myFile fle = new myFile(f);
                myFiles.Add(fle);
                lstFiles.Items.Add(f);
            }

        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("No hay archivos para ejecutar", "Atención");
                return;
            }
            ExecuteSqlFiles();

        }

        private void ExecuteSqlFiles()
        {

            for(int x = 0; x < lstFiles.Items.Count; x++)
            {
                this.UseWaitCursor = true;
                lstFiles.SelectedItem = x;
                Application.DoEvents();
                string sComandos = System.IO.File.ReadAllText(lstFiles.Items[x].ToString());
                myFiles[x].result = ExecuteSQLStatement(sComandos, 0);
                this.UseWaitCursor=false;
            }

            if (lstFiles.Items.Count > 0)
                MessageBox.Show("Para ver los Resultados sleccione el Archivo", "Ejecución Completada");
        }

        public string ExecuteSQLStatement(string MyComandoSQL, int TextLimit)
        {
            string sResult = "";
            int query = 0;
            int start = 0;
            int pos;
            string ComandoSQL = "";
            string Salida = "";
            int LinSalida = 0;
            if (TextLimit == 0 || TextLimit > 512)
                TextLimit = 512;

            while (true)
            {
                ++query;
                pos = MyComandoSQL.IndexOf("\nGO", start, StringComparison.InvariantCultureIgnoreCase);
                if (pos == -1) pos = MyComandoSQL.Length;

                if (start >= MyComandoSQL.Length)
                {
                    sResult += Salida;
                    Salida = "";
                    LinSalida = 0;
                    return sResult;
                }

                if ((pos - start) <= 0)
                    return sResult;

                ComandoSQL = MyComandoSQL.Substring(start, pos - start);

                if (string.IsNullOrEmpty(ComandoSQL) || string.IsNullOrWhiteSpace(ComandoSQL))
                {
                    return sResult;
                }

                if (!hSql.ExecuteSqlData(ComandoSQL))
                {
                    sResult += hSql.ErrorString;
                    sResult += "\n\n*** MENSAJES ***\n\n";
                    sResult += hSql.Messages;
                    return sResult;
                }

                //this.rtchSalida.Text += string.Format("\n( Q u e r y   {0} )\n\n", query);
                Salida += string.Format("\n( Q u e r y   {0} )\n\n", query);
                ++LinSalida;

                if (hSql.ErrorExiste)
                {
                    sResult += Salida;
                    Salida = "";
                    LinSalida = 0;
                    sResult += hSql.ErrorString;
                    return sResult;
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

                            //Encabezados Primera Fila
                            if (row == 0)
                            {
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
                                sResult += string.Format("\nFilas Afectadas : {0}", row);
                                sResult += Salida;
                                Salida = "";
                                LinSalida = 0;
                            }

                        }

                        //this.rtchSalida.Text += string.Format("\n( {0} Filas afectadas)\n", row);
                        Salida += string.Format("\n( {0} Filas afectadas)\n", row);
                        ++LinSalida;                        

                    } while (hSql.Data.NextResult());

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                if (pos >= MyComandoSQL.Length )
                    break;

                start = pos + 3;
            }

            sResult += Salida + "\n\n *** Mensajes *** \n\n" + hSql.Messages;
            Salida = "";
            LinSalida = 0;

            return sResult;
        }


        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0) dif = sValue.Length;

            return sValue + new String(' ', dif);
        }


        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtSalida.Text = myFiles[lstFiles.SelectedIndex].result;
        }

        private void btToMainWindow_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < lstFiles.Items.Count; x++)
            {
                RTEXT_Salida.AppendText("\n");
                RTEXT_Salida.AppendText(myFiles[x].name + "\n");
                RTEXT_Salida.AppendText(myFiles[x].result + "\n");
            }
        }

    }
}
