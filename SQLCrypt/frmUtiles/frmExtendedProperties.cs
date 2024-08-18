using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using HSql = SQLCrypt.FunctionalClasses.MySql.MySql;


namespace SQLCrypt
{

    public partial class frmExtendedProperties : Form
    {
        HSql hSql;
        TreeNode RootNode = null;
        TreeNode TablesNode = null;
        TreeNode ViewsNode = null;
        TreeNode ProcsNode = null;
        TreeNode SclrFuncNode = null;
        TreeNode TblFuncNode = null;

        change_control controller = null;

        public frmExtendedProperties(HSql hSql)
        {
            this.hSql = hSql;
            InitializeComponent();

            ContextMenu txm = new ContextMenu();
            txm.MenuItems.Add("DEPRECADO", new EventHandler(txtDeprecado));
            txm.MenuItems.Add("EN DESUSO", new EventHandler(txtEnDesuso));
            txm.MenuItems.Add("INTERNA", new EventHandler(txtInterna));
            txm.MenuItems.Add("RESPALDO", new EventHandler(txtRespaldo));


            txtObjDescription.ContextMenu = txm;

            TreeNode RootNode = new TreeNode(hSql.GetCurrentDatabase());
            RootNode.Name = hSql.GetCurrentDatabase();
            RootNode.Text = hSql.GetCurrentDatabase();
            dbObjs.Nodes.Add(RootNode);

            TablesNode = new TreeNode("Tables");
            RootNode.Nodes.Add(TablesNode);

            ViewsNode = new TreeNode("Views");
            RootNode.Nodes.Add(ViewsNode);

            ProcsNode = new TreeNode("Procedures");
            RootNode.Nodes.Add(ProcsNode);

            SclrFuncNode = new TreeNode("Scalar Functions");
            RootNode.Nodes.Add(SclrFuncNode);

            TblFuncNode = new TreeNode("Table Functions");
            RootNode.Nodes.Add(TblFuncNode);

            LoadTables();
            LoadViews();
            LoadProcs();
            LoadScalarFuncs();
            LoadTblFuncs();

            controller = new change_control(hSql);
            controller.Clear();
            RootNode.Expand();
            cbLabel.SelectedIndex = 0;

        }


        private void txtDeprecado(object sender, EventArgs e)
        {
            txtObjDescription.Text = "DEPRECADO";
        }


        private void txtEnDesuso(object sender, EventArgs e)
        {
            txtObjDescription.Text = "EN DESUSO";
        }

        private void txtInterna(object sender, EventArgs e)
        {
            txtObjDescription.Text = "INTERNA";
        }

        private void txtRespaldo(object sender, EventArgs e)
        {
            txtObjDescription.Text = "RESPALDO";
        }

        private bool LoadTables()
        {
            string Comando = @"SELECT tbl_name = OBJECT_SCHEMA_NAME(object_id) + '.' + name 
                               FROM sys.tables
                               ORDER BY OBJECT_SCHEMA_NAME(object_id), name";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show( $" Error loading Tables: {hSql.ErrorString}");
                return false;
            }
            while (hSql.Data.Read())
            {
                string tbl_name = hSql.Data[0].ToString();
                TreeNode node = new TreeNode(tbl_name);
                node.Name = "TABLE";
                TablesNode.Nodes.Add(node);
            }
            return true;
        }


        private bool LoadViews()
        {
            string Comando = @"SELECT OBJECT_SCHEMA_NAME(object_id) + '.' + name
                               FROM sys.views
                               ORDER BY OBJECT_SCHEMA_NAME(object_id), name
                             ";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($" Error loading Views: {hSql.ErrorString}");
                return false;
            }
            while (hSql.Data.Read())
            {
                string tbl_name = hSql.Data[0].ToString();
                TreeNode node = new TreeNode(tbl_name);
                node.Name = "VIEW";
                ViewsNode.Nodes.Add(node);
            }
            return true;
        }

        private bool LoadProcs()
        {
            string Comando = @"SELECT OBJECT_SCHEMA_NAME(object_id) + '.' + name
                               FROM sys.procedures
                               ORDER BY OBJECT_SCHEMA_NAME(object_id), name
                             ";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($" Error loading Stored Procedures: {hSql.ErrorString}");
                return false;
            }
            while (hSql.Data.Read())
            {
                string tbl_name = hSql.Data[0].ToString();
                TreeNode node = new TreeNode(tbl_name);
                node.Name = "PROCEDURE";
                ProcsNode.Nodes.Add(node);
            }
            return true;
        }

        private bool LoadScalarFuncs()
        {
            string Comando = @"SELECT OBJECT_SCHEMA_NAME(object_id) + '.' + name
                               FROM sys.objects
                               WHERE type = 'FN'
                               ORDER BY OBJECT_SCHEMA_NAME(object_id), name
                             ";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($" Error loading Scalar Functions: {hSql.ErrorString}");
                return false;
            }
            while (hSql.Data.Read())
            {
                string tbl_name = hSql.Data[0].ToString();
                TreeNode node = new TreeNode(tbl_name);
                node.Name = "FUNCTION";
                SclrFuncNode.Nodes.Add(node);
            }
            return true;
        }

        private bool LoadTblFuncs()
        {
            string Comando = @"SELECT OBJECT_SCHEMA_NAME(object_id) + '.' + name
                               FROM sys.objects
                               WHERE type = 'TF'
                               ORDER BY OBJECT_SCHEMA_NAME(object_id), name
                             ";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($" Error loading Table Functions: {hSql.ErrorString}");
                return false;
            }
            while (hSql.Data.Read())
            {
                string tbl_name = hSql.Data[0].ToString();
                TreeNode node = new TreeNode(tbl_name);
                node.Name = "FUNCTION";
                TblFuncNode.Nodes.Add(node);
            }
            return true;
        }

        //---------------------------------------------------------------
        //---------------------------------------------------------------

        private bool LoadFuncDef(TreeNode node)
        {
            string[] arr = node.Text.Split('.');
            if (arr.Length != 2)
                return false;

            string Schema = arr[0];
            string TableName = arr[1];

            laObjDescription.Text = $"Function: [{Schema}].[{TableName}]";

            DataTable dt = new DataTable();
            dgObject.DataSource = dt;
            dgObject.AutoResizeColumns();
            controller.Clear();

            string Comando = $@"SELECT Value
            FROM ::fn_listextendedproperty ('{cbLabel.Text}', 'Schema', '{Schema}', 'FUNCTION', '{TableName}', Null, Null)";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error loading Function [{Schema}].[{TableName}]\n{hSql.ErrorString}");
                hSql.ErrorClear();
            }

            string TableDescription = null;
            if (hSql.Data.Read())
                TableDescription = hSql.Data[0].ToString();

            txtObjDescription.Text = TableDescription;

            controller.Set(node, TableDescription, dgObject, 0);

            return true;
        }



        private bool LoadProcDef(TreeNode node)
        {
            string[] arr = node.Text.Split('.');
            if (arr.Length != 2)
                return false;

            string Schema = arr[0];
            string TableName = arr[1];

            laObjDescription.Text = $"Proc: [{Schema}].[{TableName}]";

            DataTable dt = new DataTable();
            dgObject.DataSource = dt;
            dgObject.AutoResizeColumns();
            controller.Clear();

            string Comando = $@"SELECT Value
            FROM ::fn_listextendedproperty ('{cbLabel.Text}', 'Schema', '{Schema}', 'PROCEDURE', '{TableName}', Null, Null)";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error loading Procedure [{Schema}].[{TableName}]\n{hSql.ErrorString}");
                hSql.ErrorClear();
            }

            string TableDescription = null;
            if (hSql.Data.Read())
                TableDescription = hSql.Data[0].ToString();

            txtObjDescription.Text = TableDescription;

            controller.Set(node, TableDescription, dgObject, 0);

            return true;
        }


        private bool LoadViewDef(TreeNode node)
        {

            string[] arr = node.Text.Split('.');
            if (arr.Length != 2)
                return false;

            string Schema = arr[0];
            string TableName = arr[1];

            laObjDescription.Text = $"Table: [{Schema}].[{TableName}]";

            string Comando = $@"
            select 
                   [Columna] = col.name,
                   [Tipo Dato] = t.name + 
                            case when t.is_user_defined = 0 then 
                                        isnull('(' + 
                                        case when t.name in ('binary', 'char', 'nchar',
                                                'varchar', 'nvarchar', 'varbinary') then
                                                case col.max_length 
                                                    when -1 then 'MAX' 
                                                    else 
                                                            case 
                                                                when t.name in ('nchar', 
                                                                    'nvarchar') then
                                                                    cast(col.max_length/2 
                                                                    as varchar(4))
                                                                else cast(col.max_length 
                                                                    as varchar(4))
                                                            end
                                                end
                                            when t.name in ('datetime2', 
                                                'datetimeoffset', 'time') then 
                                                cast(col.scale as varchar(4))
                                            when t.name in ('decimal', 'numeric') then 
                                                cast(col.precision as varchar(4)) + ', ' +
                                                cast(col.scale as varchar(4))
                                        end + ')', '')        
                                else ':' +
                                        (select c_t.name + 
                                                isnull('(' + 
                                                case when c_t.name in ('binary', 'char',
                                                        'nchar', 'varchar', 'nvarchar',
                                                        'varbinary') then
                                                        case c.max_length
                                                            when -1 then 'MAX'
                                                            else case when t.name in
                                                                            ('nchar',
                                                                            'nvarchar')
                                                                        then cast(c.max_length/2
                                                                            as varchar(4))
                                                                        else cast(c.max_length
                                                                            as varchar(4))
                                                                    end
                                                        end
                                                    when c_t.name in ('datetime2', 
                                                        'datetimeoffset', 'time') then
                                                        cast(c.scale as varchar(4))
                                                    when c_t.name in ('decimal', 'numeric') then
                                                        cast(c.precision as varchar(4)) +
                                                        ', ' + cast(c.scale as varchar(4))
                                                end + ')', '')
                                        from sys.columns as c
                                                inner join sys.types as c_t 
                                                    on c.system_type_id = c_t.user_type_id
                                        where c.object_id = col.object_id
                                            and c.column_id = col.column_id
                                            and c.user_type_id = col.user_type_id
                                        ) 
                            end,
                    [Nullable] = case 
                                    when col.is_nullable = 0 then 'N'
                                    else 'Y'
                                 end,
                    [Comment] = ep.value
                from sys.views as v
                    join sys.columns as col
                        on v.object_id = col.object_id
                    left join sys.types as t
                        on col.user_type_id = t.user_type_id
                    left join sys.extended_properties as ep 
                        on v.object_id = ep.major_id
                        and col.column_id = ep.minor_id
                        and ep.name = '{cbLabel.Text}'        
                        and ep.class_desc = 'OBJECT_OR_COLUMN'
                where v.schema_id = SCHEMA_ID('{Schema}')
                  And v.name = '{TableName}'
                order by col.name; ";

            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste && hSql.Messages != string.Empty)
            {
                MessageBox.Show($"Error Loading View definition: [{Schema}.{TableName}]");
                hSql.ErrorClear();
                return false;
            }

            DataTable dt = new DataTable();
            dt.Load(hSql.Data);
            dgObject.DataSource = dt;
            dgObject.AutoResizeColumns();
            controller.Clear();

            Comando = $@"SELECT Value
            FROM ::fn_listextendedproperty ('{cbLabel.Text}', 'Schema', '{Schema}', 'VIEW', '{TableName}', Null, Null)";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error loading View [{Schema}].[{TableName}]\n{hSql.ErrorString}");
                hSql.ErrorClear();
            }

            string TableDescription = null;
            if (hSql.Data.Read())
                TableDescription = hSql.Data[0].ToString();

            txtObjDescription.Text = TableDescription;

            controller.Set(node, TableDescription, dgObject, 3);

            return true;
        }


        private bool LoadTableDef(TreeNode node)
        {

            string[] arr = node.Text.Split('.');
            if (arr.Length != 2)
                return false;

            string Schema = arr[0];
            string TableName = arr[1];

            laObjDescription.Text = $"Table: [{Schema}].[{TableName}]";

            string Comando = $@"
                   select 
                   --[Schema] = schema_name(tab.schema_id),
                   --[Table] = tab.name, 
                   [Column] = col.name, 
                   --t.name as data_type,    
                   [Tipo] =  t.name + 
                       case when t.is_user_defined = 0 then 
                                 isnull('(' + 
                                 case when t.name in ('binary', 'char', 'nchar', 
                                           'varchar', 'nvarchar', 'varbinary') then
                                           case col.max_length 
                                                when -1 then 'MAX' 
                                                else 
                                                     case when t.name in ('nchar', 
                                                               'nvarchar') then
                                                               cast(col.max_length/2 
                                                               as varchar(4)) 
                                                          else cast(col.max_length 
                                                               as varchar(4)) 
                                                     end
                                           end
                                      when t.name in ('datetime2', 'datetimeoffset', 
                                           'time') then 
                                           cast(col.scale as varchar(4))
                                      when t.name in ('decimal', 'numeric') then
                                            cast(col.precision as varchar(4)) + ', ' +
                                            cast(col.scale as varchar(4))
                                 end + ')', '')        
                            else ':' + 
                                 (select c_t.name + 
                                         isnull('(' + 
                                         case when c_t.name in ('binary', 'char', 
                                                   'nchar', 'varchar', 'nvarchar', 
                                                   'varbinary') then 
                                                    case c.max_length 
                                                         when -1 then 'MAX' 
                                                         else   
                                                              case when t.name in 
                                                                        ('nchar', 
                                                                        'nvarchar') then 
                                                                        cast(c.max_length/2
                                                                        as varchar(4))
                                                                   else cast(c.max_length
                                                                        as varchar(4))
                                                              end
                                                    end
                                              when c_t.name in ('datetime2', 
                                                   'datetimeoffset', 'time') then 
                                                   cast(c.scale as varchar(4))
                                              when c_t.name in ('decimal', 'numeric') then
                                                   cast(c.precision as varchar(4)) + ', ' 
                                                   + cast(c.scale as varchar(4))
                                         end + ')', '') 
                                    from sys.columns as c
                                         inner join sys.types as c_t 
                                             on c.system_type_id = c_t.user_type_id
                                   where c.object_id = col.object_id
                                     and c.column_id = col.column_id
                                     and c.user_type_id = col.user_type_id
                                 )
                        end,
                    [Nullable] = case 
                                  when col.is_nullable = 0 then 'N' 
                                  else 'Y'
                               end,
                    [Default] = case 
                                 when def.definition is not null then def.definition 
                                 else '' 
                              end,
                    [PK] = case 
                            when pk.column_id is not null then 'PK' 
                            else '' 
                         end, 
                    [FK] = case 
                            when fk.parent_column_id is not null then 'FK' 
                            else ''
                         end, 
                    UniqueKey = case 
                                   when uk.column_id is not null then 'UK' 
                                   else ''
                                end,
                    [Check] = case 
                               when ch.check_const is not null then ch.check_const 
                               else ''
                            end,
                    [Computed] = cc.definition,
                    [Comments] = ep.value
               from sys.tables as tab
                    left join sys.columns as col
                        on tab.object_id = col.object_id
                    left join sys.types as t
                        on col.user_type_id = t.user_type_id
                    left join sys.default_constraints as def
                        on def.object_id = col.default_object_id
                    left join (
                              select index_columns.object_id, 
                                     index_columns.column_id
                                from sys.index_columns
                                     inner join sys.indexes 
                                         on index_columns.object_id = indexes.object_id
                                        and index_columns.index_id = indexes.index_id
                               where indexes.is_primary_key = 1
                              ) as pk 
                        on col.object_id = pk.object_id 
                       and col.column_id = pk.column_id
                    left join (
                              select fc.parent_column_id, 
                                     fc.parent_object_id
                                from sys.foreign_keys as f 
                                     inner join sys.foreign_key_columns as fc 
                                         on f.object_id = fc.constraint_object_id
                               group by fc.parent_column_id, fc.parent_object_id
                              ) as fk
                        on fk.parent_object_id = col.object_id 
                       and fk.parent_column_id = col.column_id    
                    left join (
                              select c.parent_column_id, 
                                     c.parent_object_id, 
                                     'Check' check_const
                                from sys.check_constraints as c
                               group by c.parent_column_id,
                                     c.parent_object_id
                              ) as ch
                        on col.column_id = ch.parent_column_id
                       and col.object_id = ch.parent_object_id
                    left join (
                              select index_columns.object_id, 
                                     index_columns.column_id
                                from sys.index_columns
                                     inner join sys.indexes 
                                         on indexes.index_id = index_columns.index_id
                                        and indexes.object_id = index_columns.object_id
                                where indexes.is_unique_constraint = 1
                                group by index_columns.object_id, 
                                      index_columns.column_id
                              ) as uk
                        on col.column_id = uk.column_id 
                       and col.object_id = uk.object_id
                    left join sys.extended_properties as ep 
                        on tab.object_id = ep.major_id
                       and col.column_id = ep.minor_id
                       and ep.name = '{cbLabel.Text}'
                       and ep.class_desc = 'OBJECT_OR_COLUMN'
                    left join sys.computed_columns as cc
                        on tab.object_id = cc.object_id
                       and col.column_id = cc.column_id
              where tab.schema_id = SCHEMA_ID('{Schema}')
                And tab.name = '{TableName}'
              order by --[Schema],
                       --[Table], 
                       [Column];";

            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste && hSql.Messages != string.Empty)
            {
                MessageBox.Show($"Error Loading Table Definition: [{Schema}.{TableName}]");
                hSql.ErrorClear();
                return false;
            }
            
            DataTable dt = new DataTable();
            dt.Load(hSql.Data);
            dgObject.DataSource = dt;
            dgObject.AutoResizeColumns();
            controller.Clear();

            Comando = $@"SELECT Value
            FROM ::fn_listextendedproperty ('{cbLabel.Text}', 'Schema', '{Schema}', 'Table', '{TableName}', Null, Null)";
            hSql.ExecuteSqlData(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error Loading Table [{Schema}].[{TableName}]\n{hSql.ErrorString}");
                hSql.ErrorClear();
            }

            string TableDescription = null;
            if (hSql.Data.Read())
                TableDescription = hSql.Data[0].ToString();
            
            txtObjDescription.Text = TableDescription;

            controller.Set(node, TableDescription, dgObject, 9);

            return true;
        }


        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dbObjs_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = dbObjs.SelectedNode;
            if (node == null)
                return;

            controller.CommentLabel = cbLabel.Text;

            switch (node.Name)
            {
                case "TABLE":
                    LoadTableDef(node);
                    break;

                case "VIEW":
                    LoadViewDef(node);
                    break;

                case "PROCEDURE":
                    LoadProcDef(node);
                    break;

                case "FUNCTION":
                    LoadFuncDef(node);
                    break;

                default:
                    //MessageBox.Show($"Tipo de Objeto no soportado {node.Name}");
                    break;
            }
            
        }

        private void btGrabar_Click(object sender, EventArgs e)
        {
            controller.WriteInfo(txtObjDescription.Text, dgObject);
            System.Media.SystemSounds.Beep.Play();
        }

    }


    /// <summary>
    /// change_control
    /// </summary>
    public class change_control
    {
        public string CommentLabel { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ObjComment { get; set; }
        public int DescColumn { get; set; }

        public List<string> desc_track = new List<string>();

        private HSql hSql;

        public change_control(HSql hSql)
        {
            this.hSql = hSql;
            this.Clear();
        }

        public void Clear()
        {
            Schema = string.Empty;
            Name = string.Empty;
            Type = string.Empty;
            DescColumn = 0;
            ObjComment = string.Empty;
            desc_track.Clear();
        }

        public void Set(TreeNode node, string ObjComment, DataGridView grid, int DescColumn)
        {
            string[] arr = node.Text.Split('.');
            Schema = arr[0];
            Name = arr[1];
            Type = node.Name;
            this.ObjComment = ObjComment;
            this.DescColumn = DescColumn;

            if (DescColumn > grid.Columns.Count)
            {
                MessageBox.Show($"Seting Change_Control:\nDescColumn is {DescColumn} and the Grid contains {grid.Columns.Count} columns");
                return;
            }

            if (grid.Rows.Count == 0)
                return;

            grid.Columns[DescColumn].Width = 450;
            for( int row=0; row < grid.Rows.Count; ++row )
            {
                for (int col =0; col < grid.Columns.Count; ++col)
                {
                    if (col == DescColumn)
                    {
                        grid.Rows[row].Cells[col].ReadOnly = false;
                        if (grid.Rows[row].Cells[col].Value is System.DBNull)
                        {
                            grid.Rows[row].Cells[col].Value = (string)null;
                            desc_track.Add(null);
                        }
                        else
                            desc_track.Add((string)grid.Rows[row].Cells[col].Value);
                    }
                    else
                        grid.Rows[row].Cells[col].ReadOnly = true;
                }
            }

        }

        public bool AddObjectComment(string Schema, string Type, string ObjectName, string Valor)
        {
            string Comando = $@"exec sp_addextendedproperty
                                 @name = N'{CommentLabel}' 
                                ,@value = N'{Valor}' 
                                ,@level0type= N'SCHEMA'
                                ,@level0name = N'{Schema}'
                                ,@level1type = N'{Type}'
                                ,@level1name = N'{ObjectName}'
                            ";
            hSql.ExecuteSql(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error adding Extended Propertie to [{Schema}].[{ObjectName}]");
                hSql.ErrorClear();
                return false;
            }
            return true;
        }

        private bool AddObjectComment(string Schema, string Type, string ObjectName, string ColumnName, string Valor)
        {
            string Comando = $@"exec sp_addextendedproperty
                                 @name = N'{CommentLabel}' 
                                ,@value = N'{Valor}' 
                                ,@level0type= N'SCHEMA'
                                ,@level0name = N'{Schema}'
                                ,@level1type = N'{Type}'
                                ,@level1name = N'{ObjectName}'
                                ,@level2type = N'COLUMN'
                                ,@level2name = N'{ColumnName}'
                            ";
            hSql.ExecuteSql(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error adding Extended Propertie to [{Schema}].[{ObjectName}] Column {ColumnName}");
                hSql.ErrorClear();
                return false;
            }
            return true;
        }

        private bool ModifyObjectComment(string Schema, string Type, string ObjectName, string Valor)
        {
            string Comando = $@"exec sp_updateextendedproperty
                                 @name = N'{CommentLabel}' 
                                ,@value = N'{Valor}'
                                ,@level0type= N'SCHEMA'
                                ,@level0name = N'{Schema}'
                                ,@level1type = N'{Type}'
                                ,@level1name = N'{ObjectName}'
                            ";
            hSql.ExecuteSql(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error adding Extended Propertie to [{Schema}].[{ObjectName}]");
                hSql.ErrorClear();
                return false;
            }
            return true;
        }

        private bool ModifyObjectComment(string Schema, string Type, string ObjectName, string ColumnName, string Valor)
        {
            string Comando = $@"exec sp_updateextendedproperty
                                 @name = N'{CommentLabel}' 
                                ,@value = N'{Valor}'
                                ,@level0type= N'SCHEMA'
                                ,@level0name = N'{Schema}'
                                ,@level1type = N'{Type}'
                                ,@level1name = N'{ObjectName}'
                                ,@level2type = N'COLUMN'
                                ,@level2name = N'{ColumnName}'
                            ";
            hSql.ExecuteSql(Comando);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show($"Error adding Extended Propertie to [{Schema}].[{ObjectName}] Column {ColumnName}");
                hSql.ErrorClear();
                return false;
            }
            return true;
        }

        public bool WriteInfo(string objComment, DataGridView grid)
        {
            if (this.ObjComment != objComment)
            {
                if (this.ObjComment == null)
                {
                    AddObjectComment(this.Schema, this.Type, this.Name, objComment);
                }
                else
                {
                    ModifyObjectComment(this.Schema, this.Type, this.Name, objComment);
                }
            }
            this.ObjComment = objComment;

            for ( int row = 0; row < this.desc_track.Count; ++row)
            {
                var paso = (grid.Rows[row].Cells[this.DescColumn].Value is System.DBNull) ? null: (string)grid.Rows[row].Cells[this.DescColumn].Value;

                if (desc_track[row] != paso)
                {
                    if (desc_track[row] == null)
                    {
                        if (paso != null)
                            try
                            {
                                AddObjectComment(this.Schema, this.Type, this.Name,
                                    (string)grid.Rows[row].Cells[0].Value, (string)grid.Rows[row].Cells[this.DescColumn].Value);
                            }
                            catch { }
                    }
                    else
                        ModifyObjectComment(this.Schema, this.Type, this.Name,
                            (string)grid.Rows[row].Cells[0].Value, (string)grid.Rows[row].Cells[this.DescColumn].Value);
                }
                desc_track[row] = paso;
            }
            return true;
        }

    }

}
