using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HSql = SQLCrypt.FunctionalClasses.MySql.MySql;

namespace SQLCrypt
{
    public partial class frmIndexes:Form
    {
        HSql hSql;

        public frmIndexes(HSql hSql)
        {
            InitializeComponent();
            HideLabels();
            this.hSql = hSql;
        }

        private void btProcesar_Click(object sender, EventArgs e)
        {
            HideLabels();

            if (txIndexName.Text == "" || txTableName.Text == "")
                return;

            string sql = string.Format("IF EXISTS( SELECT 1 from sys.indexes WHERE object_id = OBJECT_ID('{0}') And name = '{1}') DROP INDEX {2}.{3}", txTableName.Text, txIndexName.Text, txTableName.Text, txIndexName.Text);

            hSql.ExecuteSql(sql);
            if ( hSql.ErrorExiste)
            {
                MessageBox.Show( "Error Eliminando Indice: " + hSql.ErrorString);
                hSql.ErrorClear();
                return;
            }

            laDropped.Visible = true;
            laDropped.Refresh();
            Application.DoEvents();

            if (txColumns.Text == "")
                return;
            
            sql = "CREATE INDEX " + txIndexName.Text + " ON " + txTableName.Text + "(" + txColumns.Text + ")";
            if ( txInclude.Text != "")
            {
                sql += " INCLUDE (" + txInclude.Text + ")";
            }

            hSql.ExecuteSql(sql);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString);
                hSql.ErrorClear();
                return;
            }

            laCreated.Visible = true;
            laCreated.Refresh();
            Application.DoEvents();

        }

        private void txTableName_TextChanged(object sender, EventArgs e)
        {
            HideLabels();
        }

        private void HideLabels()
        {
            laDropped.Visible = false;
            laCreated.Visible = false;
            laDropped.Refresh();
            laCreated.Refresh();
            Application.DoEvents();
        }

        private void txIndexName_TextChanged(object sender, EventArgs e)
        {
            HideLabels();
        }

        private void txColumns_TextChanged(object sender, EventArgs e)
        {
            HideLabels();
        }

        private void txInclude_TextChanged(object sender, EventArgs e)
        {
            HideLabels();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Indice
            txIndexName.Text = Clipboard.GetText();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //Columnas
            txColumns.Text = Clipboard.GetText();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //Include
            txInclude.Text = Clipboard.GetText();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Tabla
            txTableName.Text = Clipboard.GetText();
        }

        private void btCleanTabla_Click(object sender, EventArgs e)
        {
            txTableName.Text = "";
        }

        private void btCleanIndex_Click(object sender, EventArgs e)
        {
            txIndexName.Text = "";
        }

        private void btCleanCols_Click(object sender, EventArgs e)
        {
            txColumns.Text = "";
        }

        private void btCleanInclude_Click(object sender, EventArgs e)
        {
            txInclude.Text = "";
        }

        private void btCleanAll_Click(object sender, EventArgs e)
        {
            txTableName.Text = "";
            txIndexName.Text = "";
            txColumns.Text = "";
            txInclude.Text = "";
        }
    }
}
