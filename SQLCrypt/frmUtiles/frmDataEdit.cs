using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HSql = SQLCrypt.FunctionalClasses.MySql.MySql;

namespace SQLCrypt
{
    public partial class frmDataEdit:Form
    {
        ToolTip MytoolTip = new ToolTip();
        private string _TableName;
        private HSql hSql;
        private TableDef Table;
        DataTable dt;
        private int NumFilas = 0;

        //----------------------
        SqlDataAdapter sqlDataAdapter;
        SqlCommandBuilder sqlCommandBuilder;
        BindingSource bindingSource;
        //----------------------
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hSql"></param>
        /// <param name="TableName"></param>
        public frmDataEdit(HSql hSql, string TableName)
        {
            this.hSql = hSql;
            Table = new TableDef(hSql);
            _TableName = TableName;

            InitializeComponent();
            
            this.Text = "Table Data Edition: " + _TableName;
            Table.Name = _TableName;

            //Carga de Combobox de Columnas
            foreach( ColumnDef c in Table.Columns)
            { 
                cbColumna.Items.Add(c);
            }

            //Cargar Lable de Primary Key
            labelPK.Text = string.Empty;
            foreach ( int x in Table.PrimaryKeysIndexes)
            {
                labelPK.Text += ( labelPK.Text == string.Empty? "Primary Key: ":"," ) + Table.Columns[x].Name;
            }

            if ( labelPK.Text == string.Empty)
               labelPK.Text = "Do not have PK, Edit is not Enabled.";

            //Formatear el Nombre de Tabla a Nombre "Seguro" usando las partes entre paréntesis []
            string[] tableArray = _TableName.Split('.');
            string sTabla = string.Empty;
            bool primero = true;

            for (int x = 0; x < tableArray.Length; ++x)
            {
                sTabla += ( primero ? "" : "." ) + "[" + tableArray[x] + "]";
                primero = false;
            }

            _TableName = sTabla;

        }



        /// <summary>
        /// Carga Datos en la Grilla usando las Condiciones de WHERE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btTraeDatos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txFiltro.Text))
            {
                if (MessageBox.Show(string.Format("Fetch Data from [{0}] without Filters?", _TableName), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Refresh();

            bool primero = true;
            string sComando = "SELECT ";
            for (int x = 0; x < Table.Columns.Count(); ++x)
            {
                if (!checkIncluyeColBinarias.Checked)
                {
                    if (!Table.Columns[x].IsBinary)
                    {
                        sComando += ( primero ? "" : "," ) + "[" + Table.Columns[x].Name + "]";
                        primero = false;
                    }
                }
                else
                {
                    sComando += ( primero ? "" : "," ) + "[" + Table.Columns[x].Name + "]";
                    primero = false;
                }
            }

            sComando += " FROM " + _TableName + ( string.IsNullOrWhiteSpace(txFiltro.Text) ? "" : " WHERE " + txFiltro.Text );

            Clipboard.Clear();
            Clipboard.SetText(sComando);

            hSql.DataClose();

            try
            {
                sqlDataAdapter = new SqlDataAdapter(sComando, hSql.Conn);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                bindingSource = new BindingSource();
                bindingSource.DataSource = dt;

                dgv.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query Error.\n" + ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Columnas de PK no editables.
            for (int x = 0; x < dgv.Columns.Count; ++x)
            {
                for (int i = 0; i < Table.Columns.Count; ++i)
                {
                    if (dgv.Columns[x].Name == Table.Columns[i].Name && Table.Columns[i].IsPrimaryKey)
                    {
                        dgv.Columns[x].DefaultCellStyle.BackColor = Color.Beige;
                        //dgv.Columns[x].ReadOnly = true;
                        break;
                    }
                }
                    
            }

            laFilas.Text = string.Format("Rows: {0}", dgv.Rows.Count);
            NumFilas = dgv.Rows.Count;
        }



        /// <summary>
        /// SALIR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// Limpia el Filtro de las condiciones del WHERE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCleanFilter_Click(object sender, EventArgs e)
        {
            txFiltro.Text = string.Empty;
        }



        /// <summary>
        /// Agrega una nueva condición al WHERE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddFilter_Click(object sender, EventArgs e)
        {
            txFiltro.Text += " " + cbAndOr.Text + " [" + cbColumna.Text + "] " + cbOperador.Text + " " +
                ( ( (ColumnDef)cbColumna.SelectedItem ).IsString ? "'" : "" ) + txFiltroAdd.Text + ( ( (ColumnDef)cbColumna.SelectedItem ).IsString ? "'" : "" );
        }



        /// <summary>
        /// Modifica en Base de Datos las ediciones realizadas en la Grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAplicaCambios_Click(object sender, EventArgs e)
        {

            string sComandoTotal = string.Empty;

            //if (!Table.HasPrimaryKey)
            //{
            DataTable dtChanges;
            string sComando = string.Empty;
            string sWhere = string.Empty;
            dtChanges = dt.GetChanges(DataRowState.Modified);

            if (dtChanges != null)
            {

                //Actualizaciones
                for (int i = 0; i < dtChanges.Rows.Count; i++)
                {
                    bool Primero = true;
                    bool PrimeroW = true;
                    sComando = "UPDATE " + _TableName + "\n";

                    for (int x = 0; x < Table.Columns.Count; ++x)
                    {

                        if (!Table.Columns[x].IsPrimaryKey)
                        {
                            if (( Table.Columns[x].IsBinary && checkIncluyeColBinarias.Checked ) || Table.Columns[x].IsBinary == false)
                            {
                                if (Table.Columns[x].Type.Contains("DATE"))
                                    sComando += ( Primero ? " SET " : " ," ) + Table.Columns[x].Name + " = " + ( Table.Columns[x].IsString ? "'" : "" ) + hSql.fValorASP(Convert.ToDateTime(dtChanges.Rows[i][Table.Columns[x].Name]), HSql.SP_DateFormat.yyyyMMddHHmmssms) + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";
                                else
                                    sComando += ( Primero ? " SET " : " ," ) + Table.Columns[x].Name + " = " + ( Table.Columns[x].IsString ? "'" : "" ) + dtChanges.Rows[i][Table.Columns[x].Name].ToString() + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";
                                Primero = false;
                            }
                        }

                        if (Table.Columns[x].IsPrimaryKey || !Table.HasPrimaryKey)
                        {
                            if (Table.Columns[x].Type.Contains("DATE"))
                                sWhere += ( PrimeroW ? "WHERE " : "  And " ) + Table.Columns[x].Name + " = " + ( Table.Columns[x].IsString ? "'" : "" ) + hSql.fValorASP(Convert.ToDateTime(dtChanges.Rows[i][Table.Columns[x].Name]), HSql.SP_DateFormat.yyyyMMddHHmmssms) + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";
                            else
                                sWhere += ( PrimeroW ? "WHERE " : "  And " ) + Table.Columns[x].Name + " = " + ( Table.Columns[x].IsString ? "'" : "" ) + dtChanges.Rows[i][Table.Columns[x].Name].ToString() + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";
                            PrimeroW = false;
                        }
                    }

                    sComandoTotal += sComando + sWhere + ";\n";
                    //hSql.ExecuteSql(sComando + sWhere);

                    //if (hSql.ErrorExiste)
                    //{
                    //    MessageBox.Show("Error SQL al actualizar Datos\n" + hSql.ErrorString, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    hSql.ErrorClear();
                    //}
                }
            }


            //Registros Insertados
            for (int i = NumFilas - 1; i < dgv.Rows.Count - 1; ++i)
            {

                //Fila Nueva
                bool Primero = true;
                    
                sComando = "INSERT INTO " + _TableName + "\n";
                string sValues = string.Empty;

                for (int x = 0; x < Table.Columns.Count; ++x)
                {
                    sComando += ( Primero ? "(" : " ," ) + Table.Columns[x].Name + "\n";
                    if ( Table.Columns[x].Type.Contains("DATE"))
                        sValues += ( Primero ? " Values (" : " ," ) + ( Table.Columns[x].IsString ? "'" : "" ) + hSql.fValorASP( Convert.ToDateTime(dgv.Rows[i].Cells[x].Value), HSql.SP_DateFormat.yyyyMMddHHmmssms) + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";
                    else
                        sValues += ( Primero ? " Values (" : " ," ) + ( Table.Columns[x].IsString ? "'" : "" ) + dgv.Rows[i].Cells[x].Value.ToString() + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";

                    Primero = false;
                }

                sComando += ")";
                sValues += ")";

                sComandoTotal += sComando + sValues + ";\n";

                hSql.ExecuteSql(sComando + sValues);

                if (hSql.ErrorExiste)
                {
                    MessageBox.Show("SQL Error SQL updating Data\n" + hSql.ErrorString, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hSql.ErrorClear();
                }

            }

            if (string.IsNullOrEmpty(sComandoTotal))
            {
                MessageBox.Show("There are no updates to notify.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Clipboard.Clear();
            Clipboard.SetText(sComandoTotal);

            MessageBox.Show("Generated commands are in the Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            //}

            if (!Table.HasPrimaryKey)
                return;

            try
            {
                sqlDataAdapter.Update(dt);
                MessageBox.Show("Row(s) Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Updating Data.\n" + ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Volver a Leer de la Tabla
            btTraeDatos_Click(null, null);
        }




        /// <summary>
        /// Borra Fila actual de la Grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBorrarActual_Click(object sender, EventArgs e)
        {
            
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount == 0)
            {
                MessageBox.Show("There are no rows selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (!Table.HasPrimaryKey)
            //{
            string sComandoTotal = string.Empty;
            string sComando = string.Empty;
            string sWhere = string.Empty;
                
            //Actualizaciones
            for (int i = dgv.SelectedRows[0].Index; i < dgv.Rows.Count; i++)
            {

                if (dgv.Rows[i].Selected)
                {
                    bool PrimeroW = true;
                    sComando = "DELETE " + _TableName + "\n";
                    sWhere = string.Empty;

                    for (int x = 0; x < Table.Columns.Count; ++x)
                    {

                        if (Table.Columns[x].IsPrimaryKey || !Table.HasPrimaryKey)
                        {
                            if (Table.Columns[x].IsPrimaryKey || !Table.HasPrimaryKey)
                            {
                                if (Table.Columns[x].Type.Contains("DATE"))
                                    sWhere += ( PrimeroW ? "WHERE " : " And " ) + Table.Columns[x].Name + " = " + ( Table.Columns[x].IsString ? "'" : "" ) + hSql.fValorASP(Convert.ToDateTime(dt.Rows[i][Table.Columns[x].Name]), HSql.SP_DateFormat.yyyyMMddHHmmssms) + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";
                                else
                                    sWhere += ( PrimeroW ? "WHERE " : " And " ) + Table.Columns[x].Name + " = " + ( Table.Columns[x].IsString ? "'" : "" ) + dt.Rows[i][Table.Columns[x].Name].ToString() + ( Table.Columns[x].IsString ? "'" : "" ) + "\n";

                                PrimeroW = false;
                            }
                            PrimeroW = false;
                        }
                    }

                    sComandoTotal += sComando + sWhere + ";\n";
                }
                    
                //hSql.ExecuteSql(sComando + sWhere);

                //if (hSql.ErrorExiste)
                //{
                //    MessageBox.Show("Error SQL al actualizar Datos\n" + hSql.ErrorString, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    hSql.ErrorClear();
                //}
            }

                
            Clipboard.Clear();
            Clipboard.SetText(sComandoTotal);

            MessageBox.Show("Generated Commands are in Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            //}


            if (!Table.HasPrimaryKey)
                return;
            

            if (MessageBox.Show("Please Confirm Deletion", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                sqlDataAdapter.Update(dt);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }



        /// <summary>
        /// Define el ToolTipText en el ComboBox de Columnas (Para el Filtro).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbColumna_SelectedIndexChanged(object sender, EventArgs e)
        {
            MytoolTip.SetToolTip(cbColumna, string.Format("{0}; Len:{1}; Prec:{2}; Scale:{3} / Computed:{4}; Nullable:{5}; Identity {6}; Guid {7}; Binary {8} / Collation {9}",
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Type,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Length,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Prec,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Scale,

                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Computed,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Nullable,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).IsIdentity,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).IsGuid,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).IsBinary,
                                                              ( (ColumnDef)cbColumna.Items[cbColumna.SelectedIndex] ).Collation                                                              
                                                          )
                                );
        }
    }
}
