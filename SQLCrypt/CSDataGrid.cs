using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HSql = SQLCrypt.FunctionalClasses.MySql.MySql;

namespace SQLCrypt
{


    /// <summary>
    /// Columnas
    /// </summary>
    public class ColumnDef
    {
        public override string ToString()
        {
            return this.Name;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public bool Computed { get; set; }
        public int Length { get; set; }
        public int Prec { get; set; }
        public int Scale { get; set; }
        public bool Nullable { get; set; }
        public bool TrimTrailingBlanks { get; set; }
        public bool FixedLenNullInSource { get; set; }
        public string Collation { get; set; }
        public bool IsBinary { get; set; }
        public bool IsString { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsGuid { get; set; }
        public bool IsPrimaryKey { get; set; }
    }




    /// <summary>
    /// Tabla
    /// </summary>
    public class TableDef
    {
        public string Name
        {
            get { return _Name; }
            set
            {
                if (LoadTableDefinition(value))
                    _Name = value;
                else
                    _Name = string.Empty;
            }
        }
        public int IdentityIndex { get { return _IdentityIndex; } }
        public int GuidIndex { get { return _GuidIndex; } }
        public bool HasPrimaryKey { get { return _HasPrimaryKey; }  }

        public string Filter { get; set; }

        //Elementos Privados
        private HSql hSql;
        private string _Name;
        private Int32 _IdentityIndex;
        private Int32 _GuidIndex;
        private bool _HasPrimaryKey;

        //Columnas
        public List<ColumnDef> Columns;

        //PrimaryKeys
        public List<Int32> PrimaryKeysIndexes = new List<Int32>();

        //Constructor
        public TableDef(HSql hSql)
        {
            this.hSql = hSql;
            Columns = new List<ColumnDef>();
        }

        private bool LoadTableDefinition(string TableName)
        {
            Columns.Clear();
            PrimaryKeysIndexes.Clear();

            hSql.ExecuteSqlData("sp_help N'" + TableName + "'");

            if (hSql.ErrorExiste)
                return false;

            int resultSet = 0;

            _IdentityIndex = -1;
            _GuidIndex = -1;
            _HasPrimaryKey = false;

            do  //ResultSet
            {
                ++resultSet;

                if (resultSet == 100)
                    continue;

                while (hSql.Data.Read())  //Filas de ResultSet
                {
                    if (resultSet == 2)
                    {
                        ColumnDef col = new ColumnDef();
                        col.Name = hSql.Data.GetString(0);
                        col.Type = hSql.Data.GetString(1).ToUpper();
                        col.Computed = hSql.Data.GetString(2) == "no" ? false : true;
                        col.Length = hSql.Data.GetInt32(3);
                        col.Prec = string.IsNullOrWhiteSpace( hSql.Data.GetString(4)) ? 0: Convert.ToInt32(hSql.Data.GetString(4));
                        col.Scale = Convert.ToInt32( (hSql.Data[5] is DBNull) ? "0": ( string.IsNullOrWhiteSpace( hSql.Data.GetString(5))? "0" : hSql.Data.GetString(5)) );
                        col.Nullable = hSql.Data.GetString(6) == "no" ? false : true;
                        col.TrimTrailingBlanks = hSql.Data.GetString(7) != "yes" ? false : true;
                        col.FixedLenNullInSource = hSql.Data.GetString(8) != "yes" ? false : true;
                        col.Collation = (hSql.Data[9] is DBNull) ? "": hSql.Data.GetString(9);
                        col.IsIdentity = false;
                        col.IsBinary = false;
                        col.IsString = false;
                        
                        switch (hSql.Data.GetString(1).ToUpper())
                        {
                            case "SQL_VARIANT":
                            case "BINARY":
                            case "VARBINARY":
                            case "IMAGE":
                                col.IsBinary = true;
                                break;

                            default:
                                col.IsBinary = false;
                                break;
                        }

                        switch (hSql.Data.GetString(1).ToUpper())
                        {
                            case "XML":
                            case "TEXT":
                            case "NTEXT":
                            case "CHAR":
                            case "VARCHAR":
                            case "NVARCHAR":
                            case "UNIQUEIDENTIFIER":
                                col.IsString = true;
                                break;

                            default:
                                if (col.Collation != "")
                                    col.IsString|= true;
                                else
                                    col.IsString = false;
                                break;
                        }

                        //switch (hSql.Data.GetString(1).ToUpper())
                        //{
                        //    case "INT":
                        //    case "SMALLINT":
                        //    case "TINYINT":
                        //    case "BIGINT":
                        //    case "BIT":
                        //    case "DATETIME":
                        //    case "DATE":
                        //    case "TIME":
                        //    case "XML":
                        //    case "MONEY":
                        //    case "FLOAT":
                        //    case "DATETIME2":
                        //    case "TEXT":
                        //    case "NTEXT":
                        //    case "UNIQUEIDENTIFIER":
                        //    case "SQL_VARIANT":
                        //        CreateTable += string.Format("  {0, -32} {1,-18} {2}", hSql.Data[0], hSql.Data[1], hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL");
                        //        break;

                        //    case "CHAR":
                        //    case "VARCHAR":
                        //    case "NVARCHAR":
                        //    case "BINARY":
                        //    case "VARBINARY":
                        //        DataType = string.Format("{0}({1})", hSql.Data[1], hSql.Data.GetInt32(3) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(3)));
                        //        CreateTable += string.Format("  {0, -32} {1, -18} {2}", hSql.Data[0], DataType, hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL");
                        //        break;

                        //    case "NUMERIC":
                        //    case "DECIMAL":
                        //        DataType = string.Format("{0}({1},{2})", hSql.Data[1], hSql.Data.GetInt32(4), hSql.Data.GetInt32(5));
                        //        CreateTable += string.Format("  {0, -32} {1, -18} {2}", hSql.Data[0], DataType, hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL");
                        //        break;

                        //    default:
                        //        try
                        //        {
                        //            DataType = string.Format("{0}({1},{2},{3})", hSql.Data[1], hSql.Data.GetInt32(3) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(3)), hSql.Data.GetInt32(4), hSql.Data.GetInt32(5));
                        //        }
                        //        catch
                        //        {
                        //            DataType = string.Format("{0}", hSql.Data[1]);
                        //        }

                        //        CreateTable += string.Format(" **{0, -32} {1, -18} {2}\n", hSql.Data[0], DataType, hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL", Separador);
                        //        break;
                        //}

                        Columns.Add(col);

                    }

                    if (resultSet == 3)
                    {
                        for (int x = 0; x < Columns.Count; ++x)
                        {
                            if (hSql.Data.GetString(0) == Columns[x].Name)
                            {
                                Columns[x].IsIdentity = true;
                                this._IdentityIndex = x;
                            }
                        }

                    }

                    if (resultSet == 4)
                    {
                        for (int x = 0; x < Columns.Count; ++x)
                        {
                            if (hSql.Data.GetString(0) == Columns[x].Name)
                            {
                                Columns[x].IsGuid = true;
                                this._GuidIndex = x;
                            }
                        }

                    }

                    if (resultSet == 6 )
                    {
                        if( hSql.Data.GetString(1).Contains( "primary key"))
                        {
                            _HasPrimaryKey = true;

                            string[] Index_Keys = hSql.Data.GetString(2).Split(',');
                            for( int x = 0; x < Index_Keys.Count(); ++x )
                            {
                                for( int y = 0; y < Columns.Count; ++y)
                                {
                                    if (Columns[y].Name == Index_Keys[x].Trim())
                                    {
                                        PrimaryKeysIndexes.Add(y);
                                        Columns[y].IsPrimaryKey = true;
                                        break;
                                    }
                                }
                            }
                            
                        }
                    }

                } //Read

            } while (hSql.Data.NextResult());

            return true;
        }


        public class CSDataGrid:DataGridView
        {
            private string _TableName;

            public HSql hSql { get; set; }
            public string TableName
            {
                get { return _TableName; }
                set
                {
                }
            }
        }


    }

}
