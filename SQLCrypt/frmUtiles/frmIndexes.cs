using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using SQLCrypt.FunctionalClasses.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using static ScintillaNET.Style;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQLCrypt.frmUtiles
{
    
    public partial class frmIndexes : Form
    {

        private MySql hSql;
        
        public frmIndexes(MySql hSql)
        {
            InitializeComponent();
            this.hSql = hSql;
            laStatus.Text = "";
            lsExistingIndexesContextMenu();
        }

        public frmIndexes(MySql hSql, string tableName) 
        {
            InitializeComponent();
            this.hSql = hSql;
            laStatus.Text = "";
            txtTableName.Text = tableName;
            
            lsExistingIndexesContextMenu();

            lsCurrentIndex.Items.Clear();
            lsExistingIndexes.Items.Clear();

            if (String.IsNullOrEmpty(txtTableName.Text))
                return;

            LoadExistingIndexes(txtTableName.Text);
        }




        private void lsExistingIndexesContextMenu()
        {
            lsExistingIndexes.ContextMenu = null;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Selected To Clipboard", new EventHandler(ExistingIndexesToClipboard));
            cm.MenuItems.Add("Select All", new EventHandler(ExistingIndexesSelectAll));
            cm.MenuItems.Add("Invert Selection", new EventHandler(ExistingIndexesInvertSelection));
            lsExistingIndexes.ContextMenu = cm;
        }


        private void ExistingIndexesSelectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < lsExistingIndexes.Items.Count; i++)
                lsExistingIndexes.SetSelected(i, true);
        }


        private void ExistingIndexesInvertSelection(object sender, EventArgs e)
        {
            for (int i = 0; i < lsExistingIndexes.Items.Count; i++)
                lsExistingIndexes.SetSelected(i, !lsExistingIndexes.GetSelected(i));
        }


        private void ExistingIndexesToClipboard(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string Elementos = "";
            foreach (var a in lsExistingIndexes.SelectedItems)
            {
                Elementos += (Elementos != ""? "\n": "") + a.ToString();
            }
            if (Elementos != "")
                Clipboard.SetText(Elementos);
            else
                laStatus.Text = "NADA SELECCIONADO !!!";
        }


        private void btPaste_Click(object sender, EventArgs e)
        {
            laStatus.Text = "Iniciando...";
            this.Update();
            this.Refresh();

            txtIndex.Text = Clipboard.GetText();
            if (! string.IsNullOrEmpty(txtIndex.Text))
            {
                ProcessIndexStatement();
            }
            laStatus.Text = "Análisis realizado.";
        }

        private void btParse_Click(object sender, EventArgs e)
        {
            laStatus.Text = "Iniciando...";
            this.Update();
            this.Refresh();
            
            ProcessIndexStatement();
            laStatus.Text = "Análisis realizado.";
        }

        private void ProcessIndexStatement()
        {
            lsCurrentIndex.Items.Clear();
            lsExistingIndexes.Items.Clear();

            int pos_on = txtIndex.Text.ToLower().IndexOf(" on ");
            if (pos_on == -1)
            {
                laStatus.Text = "No se encontró \"ON\" para identificar la tabla";
                return;
            }
            var index_text = txtIndex.Text.ToLower().Substring(pos_on + 3).Trim();
            int pos_o_p = index_text.IndexOf("(");
            var tabla = index_text.Substring(0, pos_o_p - 1);
            txtTableName.Text = tabla;

            index_text = index_text.Substring(pos_o_p + 1);
            int pos_c_p = index_text.IndexOf(")");
            index_text = index_text.Substring(0, pos_c_p);

            var columnas = index_text.Split(',');

            int rows = CountAllRows(tabla);

            lsCurrentIndex.Items.Add($"{tabla:40} {rows}");
            foreach (var col in columnas)
            {
                var col_name = col.Trim();
                int col_rows = CountColumnRows(tabla, col_name);
                lsCurrentIndex.Items.Add($"{col_name:40} {col_rows}");
            }

            LoadExistingIndexes(tabla);
        }

        private void LoadExistingIndexes(string tabla)
        {
            lsExistingIndexes.Items.Clear();

            var table_name = tabla.Replace("[", "").Replace("]", "");

            string comando = $@"
    SELECT 
    DB_NAME() AS database_name,
    sc.name + N'.' + t.name AS table_name,
    (SELECT MAX(user_reads) 
        FROM (VALUES (last_user_seek), (last_user_scan), (last_user_lookup)) AS value(user_reads)) AS last_user_read,
    last_user_update,
    CASE si.index_id WHEN 0 THEN N'/* No create statement (Heap) */'
    ELSE 
        CASE is_primary_key WHEN 1 THEN
            N'ALTER TABLE ' + QUOTENAME(sc.name) + N'.' + QUOTENAME(t.name) + N' ADD CONSTRAINT ' + QUOTENAME(si.name) + N' PRIMARY KEY ' +
                CASE WHEN si.index_id > 1 THEN N'NON' ELSE N'' END + N'CLUSTERED '
            ELSE N'CREATE ' + 
                CASE WHEN si.is_unique = 1 then N'UNIQUE ' ELSE N'' END +
                CASE WHEN si.index_id > 1 THEN N'NON' ELSE N'' END + N'CLUSTERED ' +
                N'INDEX ' + QUOTENAME(si.name) + N' ON ' + QUOTENAME(sc.name) + N'.' + QUOTENAME(t.name) + N' '
        END +
        /* key def */ N'(' + key_definition + N')' +
        /* includes */ CASE WHEN include_definition IS NOT NULL THEN 
            N' INCLUDE (' + include_definition + N')'
            ELSE N''
        END +
        /* filters */ CASE WHEN filter_definition IS NOT NULL THEN 
            N' WHERE ' + filter_definition ELSE N''
        END +
        /* with clause - compression goes here */
        CASE WHEN row_compression_partition_list IS NOT NULL OR page_compression_partition_list IS NOT NULL 
            THEN N' WITH (' +
                CASE WHEN row_compression_partition_list IS NOT NULL THEN
                    N'DATA_COMPRESSION = ROW ' + CASE WHEN psc.name IS NULL THEN N'' ELSE + N' ON PARTITIONS (' + row_compression_partition_list + N')' END
                ELSE N'' END +
                CASE WHEN row_compression_partition_list IS NOT NULL AND page_compression_partition_list IS NOT NULL THEN N', ' ELSE N'' END +
                CASE WHEN page_compression_partition_list IS NOT NULL THEN
                    N'DATA_COMPRESSION = PAGE ' + CASE WHEN psc.name IS NULL THEN N'' ELSE + N' ON PARTITIONS (' + page_compression_partition_list + N')' END
                ELSE N'' END
            + N')'
            ELSE N''
        END +
        /* ON where? filegroup? partition scheme? */
        ' ON ' + CASE WHEN psc.name is null 
            THEN ISNULL(QUOTENAME(fg.name),N'')
            ELSE psc.name + N' (' + partitioning_column.column_name + N')' 
            END
        + N';'
    END AS index_create_statement,
    si.index_id,
    si.name AS index_name,
    partition_sums.reserved_in_row_GB,
    partition_sums.reserved_LOB_GB,
    partition_sums.row_count,
    stat.user_seeks,
    stat.user_scans,
    stat.user_lookups,
    user_updates AS queries_that_modified,
    partition_sums.partition_count,
    si.allow_page_locks,
    si.allow_row_locks,
    si.is_hypothetical,
    si.has_filter,
    si.fill_factor,
    si.is_unique,
    ISNULL(pf.name, '/* Not partitioned */') AS partition_function,
    ISNULL(psc.name, fg.name) AS partition_scheme_or_filegroup,
    t.create_date AS table_created_date,
    t.modify_date AS table_modify_date
FROM sys.indexes AS si
JOIN sys.tables AS t ON si.object_id=t.object_id
JOIN sys.schemas AS sc ON t.schema_id=sc.schema_id
LEFT JOIN sys.dm_db_index_usage_stats AS stat ON 
    stat.database_id = DB_ID() 
    and si.object_id=stat.object_id 
    and si.index_id=stat.index_id
LEFT JOIN sys.partition_schemes AS psc ON si.data_space_id=psc.data_space_id
LEFT JOIN sys.partition_functions AS pf ON psc.function_id=pf.function_id
LEFT JOIN sys.filegroups AS fg ON si.data_space_id=fg.data_space_id
/* Key list */ OUTER APPLY ( SELECT STUFF (
    (SELECT N', ' + QUOTENAME(c.name) +
        CASE ic.is_descending_key WHEN 1 then N' DESC' ELSE N'' END
    FROM sys.index_columns AS ic 
    JOIN sys.columns AS c ON 
        ic.column_id=c.column_id  
        and ic.object_id=c.object_id
    WHERE ic.object_id = si.object_id
        and ic.index_id=si.index_id
        and ic.key_ordinal > 0
    ORDER BY ic.key_ordinal FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'')) AS keys ( key_definition )
/* Partitioning Ordinal */ OUTER APPLY (
    SELECT MAX(QUOTENAME(c.name)) AS column_name
    FROM sys.index_columns AS ic 
    JOIN sys.columns AS c ON 
        ic.column_id=c.column_id  
        and ic.object_id=c.object_id
    WHERE ic.object_id = si.object_id
        and ic.index_id=si.index_id
        and ic.partition_ordinal = 1) AS partitioning_column
/* Include list */ OUTER APPLY ( SELECT STUFF (
    (SELECT N', ' + QUOTENAME(c.name)
    FROM sys.index_columns AS ic 
    JOIN sys.columns AS c ON 
        ic.column_id=c.column_id  
        and ic.object_id=c.object_id
    WHERE ic.object_id = si.object_id
        and ic.index_id=si.index_id
        and ic.is_included_column = 1
    ORDER BY c.name FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'')) AS includes ( include_definition )
/* Partitions */ OUTER APPLY ( 
    SELECT 
        COUNT(*) AS partition_count,
        CAST(SUM(ps.in_row_reserved_page_count)*8./1024./1024. AS NUMERIC(32,1)) AS reserved_in_row_GB,
        CAST(SUM(ps.lob_reserved_page_count)*8./1024./1024. AS NUMERIC(32,1)) AS reserved_LOB_GB,
        SUM(ps.row_count) AS row_count
    FROM sys.partitions AS p
    JOIN sys.dm_db_partition_stats AS ps ON
        p.partition_id=ps.partition_id
    WHERE p.object_id = si.object_id
        and p.index_id=si.index_id
    ) AS partition_sums
/* row compression list by partition */ OUTER APPLY ( SELECT STUFF (
    (SELECT N', ' + CAST(p.partition_number AS VARCHAR(32))
    FROM sys.partitions AS p
    WHERE p.object_id = si.object_id
        and p.index_id=si.index_id
        and p.data_compression = 1
    ORDER BY p.partition_number FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'')) AS row_compression_clause ( row_compression_partition_list )
/* data compression list by partition */ OUTER APPLY ( SELECT STUFF (
    (SELECT N', ' + CAST(p.partition_number AS VARCHAR(32))
    FROM sys.partitions AS p
    WHERE p.object_id = si.object_id
        and p.index_id=si.index_id
        and p.data_compression = 2
    ORDER BY p.partition_number FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'')) AS page_compression_clause ( page_compression_partition_list )
WHERE 
    si.type IN (0,1,2) /* heap, clustered, nonclustered */
    and sc.name + '.' + t.name = '{table_name}'
ORDER BY table_name, si.index_id
    OPTION (RECOMPILE);
            ";

            hSql.ExecuteSqlData(comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error SQL Buscando Índices {hSql.ErrorString}");
                hSql.ErrorClear();
                return;
            }

            while(hSql.Data.Read())
            {
                lsExistingIndexes.Items.Add(hSql.Data.GetString(4));
            }

        }


        private int CountColumnRows(string tabla, string columna)
        {
            int count = 0;
            string comando = $"SELECT COUNT( distinct {columna}) FROM {tabla}";
            hSql.ExecuteSqlData(comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error SQL: {hSql.ErrorString}");
                hSql.ErrorClear();
                return -1;
            }

            hSql.Data.Read();
            count = hSql.Data.GetInt32(0);

            return count;
        }


        private int CountAllRows(string tabla)
        { 
            int count = 0;
            string comando = $"SELECT COUNT(*) FROM {tabla}";
            hSql.ExecuteSqlData(comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error SQL: {hSql.ErrorString}");
                hSql.ErrorClear();
                return -1;
            }

            hSql.Data.Read();
            count = hSql.Data.GetInt32(0);

            return count;
        }

        private void btCreateIndex_Click(object sender, EventArgs e)
        {
            hSql.ExecuteSql(txtIndex.Text);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error Creando Índice:\n{hSql.ErrorString}");
                hSql.ErrorClear();
                return;
            }
            MessageBox.Show("Index Created !");
        }

        private void btGetIndexes_Click(object sender, EventArgs e)
        {
            lsCurrentIndex.Items.Clear();
            lsExistingIndexes.Items.Clear();

            if (String.IsNullOrEmpty(txtTableName.Text))
                return;
            
            LoadExistingIndexes(txtTableName.Text);
        }


        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
