using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HSql = SQLCrypt.FunctionalClasses.MySql.MySql;

namespace SQLCrypt.StructureClasses
{

    /// <summary>
    /// Lista Abstracta de Objetos de Base de Datos
    /// </summary>
    public class DbObjects:List<DBObject>
    {
        private HSql hSql;
        private string _type;

        public string ErrorString
        {
            get;
            internal set;
        }


        public DbObjects(HSql hSql)
        {
            this.hSql = hSql;
        }


        public void Load(string type)
        {
            this.Clear();
            this._type = type;

            this.ErrorString = string.Empty;

            string sCommand = @"select [name],
                                       [object_id],
	                                   SCHEMA_NAME(schema_id) as SchemaName,
                                       [schema_id],
	                                   [parent_object_id],
                                       [type],
                                       [type_desc],
                                       [create_date],
                                       [modify_date]
                              from sys.objects
                              WHERE type = " + hSql.fValorASP(this._type) + @"
                              ORDER BY SCHEMA_NAME(schema_id), [name]";

            hSql.ExecuteSqlData(sCommand);

            if (hSql.ErrorExiste)
            {
                ErrorString = hSql.ErrorString;
                return;
            }

            while (hSql.Data.Read())
            {
                DBObject obj = new DBObject(hSql);
                obj.name = hSql.Data.GetString(0);
                obj.object_id = hSql.Data.GetInt32(1);
                obj.schema_name = hSql.Data.GetString(2);
                obj.schema_id = hSql.Data.GetInt32(3);
                obj.parent_object_id = hSql.Data.GetInt32(4);
                obj.type = hSql.Data.GetString(5).Trim();
                obj.type_desc = hSql.Data.GetString(6);
                obj.create_date = hSql.Data.GetDateTime(7);
                obj.modify_date = hSql.Data.GetDateTime(8);

                this.Add(obj);
            }

        }


        public void FindByColumn(string type, string Column)
        {
            this.Clear();
            this._type = type;

            this.ErrorString = string.Empty;

            string sCommand = @"select obj.[name],
                                       obj.[object_id],
	                                   SCHEMA_NAME(obj.schema_id) as SchemaName,
                                       obj.[schema_id],
	                                   obj.[parent_object_id],
                                       obj.[type],
                                       obj.[type_desc],
                                       obj.[create_date],
                                       obj.[modify_date],
                                       tipo = (select ty.name +' (' + cast(col.max_length as varchar) + ')' from sys.types ty where ty.user_type_id = col.user_type_id)
                              from sys.objects obj
                              join sys.columns col on col.object_id = obj.object_id
                              WHERE type = " + hSql.fValorASP(this._type) + @"
                                 And col.name = '" + Column + @"'
                              ORDER BY SCHEMA_NAME(schema_id), obj.[name]";

            hSql.ExecuteSqlData(sCommand);

            if (hSql.ErrorExiste)
            {
                ErrorString = hSql.ErrorString;
                return;
            }

            while (hSql.Data.Read())
            {
                DBObject obj = new DBObject(hSql);
                obj.name = hSql.Data.GetString(0);
                obj.object_id = hSql.Data.GetInt32(1);
                obj.schema_name = hSql.Data.GetString(2);
                obj.schema_id = hSql.Data.GetInt32(3);
                obj.parent_object_id = hSql.Data.GetInt32(4);
                obj.type = hSql.Data.GetString(5).Trim();
                obj.type_desc = hSql.Data.GetString(6);
                obj.create_date = hSql.Data.GetDateTime(7);
                obj.modify_date = hSql.Data.GetDateTime(8);
                obj.description = hSql.Data.GetString(9);

                this.Add(obj);
            }
        }


        public void FindByText(string type, string text)
        {
            this.Clear();
            type = type.Trim();
            this._type = type;

            this.ErrorString = string.Empty;

            string sCommand = @"select [name],
                                       [object_id],
	                                   SCHEMA_NAME(schema_id) as SchemaName,
                                       [schema_id],
	                                   [parent_object_id],
                                       [type],
                                       [type_desc],
                                       [create_date],
                                       [modify_date]
                              from sys.objects so
                              WHERE type = " + hSql.fValorASP(this._type) + @"
                                 And exists ( select 1 from syscomments sc where text like '%" + text + @"%' and sc.id = so.object_id )
                              Order by SCHEMA_NAME(schema_id), [name]";

            hSql.ExecuteSqlData(sCommand);

            if (hSql.ErrorExiste)
            {
                ErrorString = hSql.ErrorString;
                return;
            }

            while (hSql.Data.Read())
            {
                DBObject obj = new DBObject(hSql);
                obj.name = hSql.Data.GetString(0);
                obj.object_id = hSql.Data.GetInt32(1);
                obj.schema_name = hSql.Data.GetString(2);
                obj.schema_id = hSql.Data.GetInt32(3);
                obj.parent_object_id = hSql.Data.GetInt32(4);
                obj.type = hSql.Data.GetString(5).Trim();
                obj.type_desc = hSql.Data.GetString(6);
                obj.create_date = hSql.Data.GetDateTime(7);
                obj.modify_date = hSql.Data.GetDateTime(8);

                this.Add(obj);
            }
        }

    }


    /// <summary>
    /// Clase Base de Objectos de Base de Datos
    /// </summary>
    public class DBObject
    {
        private HSql hSql;

        public override string ToString()
        {
            return this.schema_name + "." + this.name;
        }

        public DBObject(HSql hSql)
        {
            this.hSql = hSql;
        }

        public string name
        {
            get;
            internal set;
        }

        public string type
        {
            get;
            internal set;
        }

        public string type_desc
        {
            get;
            internal set;
        }

        public Int32 object_id
        {
            get;
            internal set;
        }

        public string description
        {
            get;
            internal set;
        }

        public string collation_name
        {
            get;
            internal set;
        }

        public virtual string GetText()
        {
            if (this.type == "P" || this.type.Trim() == "V" || this.type == "TR" || this.type == "TF" || this.type == "FN")
                return GetFromSysComment();
            else
                return GetSpHelp();
        }


        public string ObjGetCreateTable()
        {
            if (this.type == "U")
                return GetCreateTable();
            else
                return "No se aplica a este Objeto";
        }


        public string GetCreateTable()
        {
            string sCommand;
            string sObjName = "[" + this.schema_name + "].[" + this.name + "]";

            if (this.type != "U")
                return "No Aplica al Objeto";

            string CreateTable = "CREATE TABLE " + sObjName + "\n(\n";

            sCommand = @"select sc.name,
       st.name,
       sc.is_nullable,
       sc.is_identity,
       sc.is_rowguidcol,
       sc.max_length,
       sc.precision,
       sc.scale
       collation_name = ISNULL(st.collation_name, '')
from  sys.columns  sc
 JOIN sys.types    st
   On st.user_type_id = sc.user_type_id
where object_id = OBJECT_ID('" + sObjName + @"')
Order by column_id";
            hSql.ExecuteSqlData(sCommand);

            string DataType;
            string Identity;
            string Nulable;
            string Separador = "";

            if (hSql.ErrorExiste)
                return hSql.ErrorString;

            int nRow = 0;

            while (hSql.Data.Read())  //Filas de ResultSet
            {
                if (nRow != 0)
                    CreateTable += ",\n";

                switch (hSql.Data.GetString(1).ToUpper())
                {
                    case "INT":
                    case "SMALLINT":
                    case "TINYINT":
                    case "BIGINT":
                    case "BIT":
                    case "DATETIME":
                    case "DATE":
                    case "TIME":
                    case "XML":
                    case "MONEY":
                    case "FLOAT":
                    case "DATETIME2":
                    case "TEXT":
                    case "IMAGE":
                    case "NTEXT":
                    case "UNIQUEIDENTIFIER":
                    case "SQL_VARIANT":
                        Identity = hSql.Data.GetBoolean(3) == true ? "IDENTITY" : "        ";
                        Nulable = hSql.Data.GetBoolean(2) == false ? "NOT NULL" : "    NULL";
                        CreateTable += string.Format("   {0, -32} {1,-18} {2,  8} {3, 8}", hSql.Data[0], hSql.Data[1], Identity, Nulable);
                        break;

                    case "CHAR":
                    case "VARCHAR":
                    case "NVARCHAR":
                    case "BINARY":
                    case "VARBINARY":
                        DataType = string.Format("{0}({1})", hSql.Data[1], hSql.Data.GetInt32(5) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(5)));
                        Identity = hSql.Data.GetBoolean(3) == true ? "IDENTITY" : "        ";
                        Nulable = hSql.Data.GetBoolean(2) == false ? "NOT NULL" : "    NULL";
                        CreateTable += string.Format("   {0, -32} {1, -18} {2,  8} {3, 8}", hSql.Data[0], DataType, Identity, Nulable);
                        break;

                    case "NUMERIC":
                    case "DECIMAL":
                        DataType = string.Format("{0}({1},{2})", hSql.Data[1], hSql.Data.GetInt32(6), hSql.Data.GetInt32(7));
                        Identity = hSql.Data.GetBoolean(3) == true ? "IDENTITY" : "        ";
                        Nulable = hSql.Data.GetBoolean(2) == false ? "NOT NULL" : "    NULL";
                        CreateTable += string.Format("   {0, -32} {1, -18} {2,  8} {3, 8}", hSql.Data[0], DataType, Identity, Nulable);
                        break;

                    default:
                        try
                        {
                            if (hSql.Data["collation_name"].ToString() != "")
                                DataType = string.Format("   {0}({1})", hSql.Data[1], hSql.Data.GetInt32(5) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(5)));
                            else
                                DataType = string.Format("   {0}({1},{2},{3})", hSql.Data[1], hSql.Data.GetInt32(5) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(5)), hSql.Data.GetInt32(6), hSql.Data.GetInt32(7));
                        }
                        catch
                        {
                            DataType = string.Format("   {0}", hSql.Data[1]);
                        }
                        Nulable = hSql.Data.GetBoolean(2) == false ? "NOT NULL" : "    NULL";
                        CreateTable += string.Format(" **{0, -32} {1, -18} {2, 8}\n", hSql.Data[0], DataType, Nulable, Separador);
                        break;
                }

                ++nRow;
            }

            CreateTable += "\n)\nGO\n\n";

            sCommand = @"DECLARE @object_id  int
SET @object_id = " + this.object_id.ToString() + @";

;WITH CTE AS ( 
           SELECT  ic.[index_id] + ic.[object_id] AS [IndexId]
                  ,t.[name]      AS [TableName]
                  ,t.[object_id] As [TableId] 
                  ,i.[name]      AS [IndexName]
                  ,c.[name]      AS [ColumnName]
                  ,i.[type_desc] 
                  ,i.[is_primary_key]
                  ,i.[is_unique]
                  ,ic.[is_included_column]
           FROM  [sys].[indexes] i 
           INNER JOIN [sys].[index_columns] ic 
                   ON  i.[index_id]    =   ic.[index_id] 
                   AND i.[object_id]   =   ic.[object_id] 
           INNER JOIN [sys].[columns] c 
                   ON  ic.[column_id]  =   c.[column_id] 
                   AND i.[object_id]   =   c.[object_id] 
           INNER JOIN [sys].[tables] t 
                   ON  i.[object_id] = t.[object_id] 
)
 
SELECT CAST(OBJECT_SCHEMA_NAME(c.[TableId]) + '.' + c.[TableName] as Varchar(40)) As [Tabla]
      ,CAST(c.[IndexName] as Varchar(50)) As [IndexName]
      ,c.[type_desc]
      ,c.[is_primary_key]
      ,c.[is_unique] 
      ,ISNULL(STUFF( ( SELECT ','+ a.[ColumnName] FROM CTE a WHERE c.[IndexId] = a.[IndexId] And a.[is_included_column] = 0 FOR XML PATH('')),1 ,1, ''), '') AS [Columns]
      ,ISNULL(STUFF( ( SELECT ','+ a.[ColumnName] FROM CTE a WHERE c.[IndexId] = a.[IndexId] And a.[is_included_column] = 1 FOR XML PATH('')),1 ,1, ''), '') AS [IncludedColumns]
FROM   CTE c
where c.[TableId] = @object_id
GROUP  BY c.[IndexId],c.[TableName], c.[TableID] ,c.[IndexName],c.[type_desc],c.[is_primary_key],c.[is_unique] 
ORDER  BY CAST(OBJECT_SCHEMA_NAME(c.[TableId]) + '.' + c.[TableName] as Varchar(40)) ASC,
          c.[is_primary_key] DESC;";

            hSql.ExecuteSqlData(sCommand);
            if (hSql.ErrorExiste)
                return hSql.ErrorString;

            while (hSql.Data.Read()) //Filas de ResultSet
            {

                if (Convert.ToBoolean(hSql.Data["is_primary_key"]))
                {
                    CreateTable += "ALTER TABLE " + sObjName + " ADD CONSTRAINT " + hSql.Data["IndexName"] +
                                   " PRIMARY KEY (" + hSql.Data["Columns"] + ")";
                }
                else
                {
                    CreateTable += "CREATE ";
                    if (Convert.ToBoolean(hSql.Data["is_unique"]))
                        CreateTable += "UNIQUE ";

                    if (Convert.ToBoolean(hSql.Data["is_primary_key"]))
                        CreateTable += "PRIMARY KEY ";
                    else
                        CreateTable += "INDEX ";

                    CreateTable += hSql.Data["IndexName"] + " ON " + sObjName + "(" + hSql.Data["Columns"] + ")";

                    if (hSql.Data["IncludedColumns"].ToString() != "")
                        CreateTable += " INCLUDE(" + hSql.Data["IncludedColumns"] + ")";
                }
                CreateTable += "\nGo\n\n";
            }

            return CreateTable;
        }

        public string GetCreateTable2()
        {
            if (this.type != "U")
                return "No Aplica al Objeto";

            string CreateTable = "CREATE TABLE [" + this.schema_name + "." + this.name + "]\n(\n";
            int resultSet = 0;

            hSql.ExecuteSqlData("sp_help N'" + this.schema_name + "." + this.name + "'");

            if (hSql.ErrorExiste)
                return hSql.ErrorString;

            do  //ResultSet
            {
                ++resultSet;

                if (resultSet == 100)
                {
                    continue;
                }

                int row = 0;
                List<int> lenC = new List<int>();
                while (hSql.Data.Read())  //Filas de ResultSet
                {
                    if (resultSet == 2)
                    {
                        string DataType;
                        string Separador = "";

                        if (row != 0)
                            CreateTable += ",\n";

                        switch (hSql.Data.GetString(1).ToUpper())
                        {
                            case "INT":
                            case "SMALLINT":
                            case "TINYINT":
                            case "BIGINT":
                            case "BIT":
                            case "DATETIME":
                            case "DATE":
                            case "TIME":
                            case "XML":
                            case "MONEY":
                            case "FLOAT":
                            case "DATETIME2":
                            case "TEXT":
                            case "NTEXT":
                            case "UNIQUEIDENTIFIER":
                            case "SQL_VARIANT":
                                CreateTable += string.Format("  {0, -32} {1,-18} {2}", hSql.Data[0], hSql.Data[1], hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL");
                                break;

                            case "CHAR":
                            case "VARCHAR":
                            case "NVARCHAR":
                            case "BINARY":
                            case "VARBINARY":
                                DataType = string.Format("{0}({1})", hSql.Data[1], hSql.Data.GetInt32(3) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(3)));
                                CreateTable += string.Format("  {0, -32} {1, -18} {2}", hSql.Data[0], DataType, hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL");
                                break;

                            case "NUMERIC":
                            case "DECIMAL":
                                DataType = string.Format("{0}({1},{2})", hSql.Data[1], hSql.Data.GetInt32(4), hSql.Data.GetInt32(5));
                                CreateTable += string.Format("  {0, -32} {1, -18} {2}", hSql.Data[0], DataType, hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL");
                                break;

                            default:
                                try
                                {
                                    DataType = string.Format("{0}({1},{2},{3})", hSql.Data[1], hSql.Data.GetInt32(3) == -1 ? "MAX" : Convert.ToString(hSql.Data.GetInt32(3)), hSql.Data.GetInt32(4), hSql.Data.GetInt32(5));
                                }
                                catch
                                {
                                    DataType = string.Format("{0}", hSql.Data[1]);
                                }

                                CreateTable += string.Format(" **{0, -32} {1, -18} {2}\n", hSql.Data[0], DataType, hSql.Data.GetString(6) == "no" ? "NOT NULL" : "    NULL", Separador);
                                break;
                        }

                    }

                    ++row;
                }
                if (resultSet == 2)
                {
                    CreateTable += "\n)\nGO";
                }

            } while (hSql.Data.NextResult());

            return CreateTable;
        }



        public string GetData(bool limitedByTen)
        {
            string sComando = "";

            if (limitedByTen)
               sComando = "SELECT TOP(100) * FROM [" + this.schema_name + "].[" + this.name + "]";
            else
               sComando = "SELECT * FROM [" + this.schema_name + "].[" + this.name + "]";

            hSql.ExecuteSqlData(sComando);

            if (hSql.ErrorExiste)
                return hSql.ErrorString;

            return sComando;
        }


        public int GetCountRows()
        {
            string sComando = "";
            int Count = 0;

            sComando = "SELECT COUNT(*) FROM [" + this.schema_name + "].[" + this.name + "]";

            try
            {
                hSql.ExecuteSqlData(sComando);

                if (hSql.ErrorExiste)
                    return -1;

                hSql.Data.Read();
                Count = (int)hSql.Data[0];
            }
            catch
            {
                return -1;
            }
            return Count;
        }


        private string GetSpHelp()
        {
            string Texto = string.Empty;

            hSql.ExecuteSqlData("sp_help N'" + this.schema_name + "." + this.name + "'");

            if (hSql.ErrorExiste)
                return hSql.ErrorString;

            do  //ResultSet
            {
                int row = 0;
                List<int> lenC = new List<int>();
                while (hSql.Data.Read())  //Filas de ResultSet
                {
                    //Encabezados Primera Fila
                    if (row == 0)
                    {
                        string guiones = "";

                        for (int x = 0; x < hSql.Data.FieldCount; ++x)
                        {
                            //object[] meta = new object[hSql.Data.FieldCount];
                            //int NumberOfColums = hSql.Data.GetValues(meta);

                            int size = Convert.ToInt32(hSql.Data.GetSchemaTable().Rows[x]["ColumnSize"]);
                            if (size > 95)
                                size = 95;
                            if (size < hSql.Data.GetName(x).Length)
                                size = hSql.Data.GetName(x).Length;

                            lenC.Add(size + 1);
                            guiones += ( ( x > 0 ) ? " " : "" ) + new String('-', lenC[x]);

                            Texto += ( ( x > 0 ) ? " " : "" ) + StringComplete(hSql.Data.GetName(x), lenC[x]);
                        }

                        Texto += "\n" + guiones + "\n";
                    }


                    for (int x = 0; x < hSql.Data.FieldCount; ++x) //Columnas
                    {
                        if (hSql.Data.IsDBNull(x))
                            Texto += ( ( x > 0 ) ? " " : "" ) + StringComplete("null", lenC[x]);
                        else
                            Texto += ( ( x > 0 ) ? " " : "" ) + StringComplete(Convert.ToString(hSql.Data[x]), lenC[x]);
                    }
                    Texto += "\n";
                    ++row;
                }

                Texto += "\n";

            } while (hSql.Data.NextResult());



            return Texto;
        }


        private string StringComplete(string sValue, int length)
        {
            int dif = length - sValue.Length;

            if (dif < 0)
                dif = sValue.Length;

            return sValue + new String(' ', dif);
        }



        /// <summary>
        /// Lee el texto del objeto de Base de Datos desde la tabla de systema Syscomments
        /// </summary>
        /// <returns></returns>
        private string GetFromSysComment()
        {
            string Drop = string.Empty;
            string Prefijo = string.Empty;
            string Texto = string.Empty;
            string TipoObjecto = string.Empty;

            string SetOptions = "SET ANSI_NULLS ON;\r\nGO\r\nSET QUOTED_IDENTIFIER ON;\r\nGO\r\n\r\n";

            switch (this.type.Trim())
            {
                case "P":  //Procedimiento
                    TipoObjecto = "Proc";
                    Drop = string.Format("\r\nIf OBJECT_ID('{0}.{1}') IS NOT NULL\n   DROP PROC [{0}].[{1}]\r\nGo\r\n\r\n", this.schema_name, this.name);
                    break;

                case "V":  //Vista
                    TipoObjecto = "Vista";
                    Drop += string.Format("\r\nIf OBJECT_ID('{0}.{1}') IS NOT NULL\n   DROP VIEW [{0}].[{1}]\r\nGo\r\n\r\n", this.schema_name, this.name);
                    break;

                case "TF":  //Funcion Tabular
                case "FN":  //Funcion Escalar
                    TipoObjecto = "Function";
                    Drop += string.Format("\r\nIf OBJECT_ID('{0}.{1}') IS NOT NULL\n   DROP FUNCTION [{0}].[{1}]\r\nGo\r\n\r\n", this.schema_name, this.name);
                    break;

                case "TR":  //Trigger
                    TipoObjecto = "Trigger";
                    Drop = string.Format("\r\nIf OBJECT_ID('{0}.{1}') IS NOT NULL\r\n   DROP TRIGGER [{0}].[{1}]\r\nGo\r\n\r\n", this.schema_name, this.name);
                    break;

                default:
                    break;
            }

            Prefijo += "/*===============================================================================\r\n";
            Prefijo += string.Format("  {0}: {1}.{2}\r\n", TipoObjecto, this.schema_name, this.name);
            Prefijo += string.Format("  Fecha : {0:dd/MM/yyyy H:mm:ss}\r\n", DateTime.Now);
            Prefijo += "  ===============================================================================*/\r\n";
            Prefijo += Drop;

            string sCommmand = @"select text
                                from syscomments
                                where id = " + hSql.fValorASP(this.object_id) + @"
                                And encrypted = 0
                                order by number";

            try
            {
                hSql.ExecuteSqlData(sCommmand);

                if (hSql.ErrorExiste)
                {
                    return hSql.ErrorString;
                }

                while (hSql.Data.Read())
                {
                    Texto += hSql.Data.GetString(0);
                }

                //Texto = Prefijo + System.Text.RegularExpressions.Regex.Replace(Texto, @"( |\r?\n)\1+", "$1");
                Texto = System.Text.RegularExpressions.Regex.Replace(Texto, @"[ \t]+\r\n", "\r\n");
                Texto = Prefijo + System.Text.RegularExpressions.Regex.Replace(Texto, @"(\r?\n){3,}", "\r\n\r\n");
                Texto = SetOptions + Texto;
            }
            catch
            {
                if (string.IsNullOrEmpty(Texto) || string.IsNullOrWhiteSpace(Texto))
                    return "No hay Texto disponible para este Objeto";
            }

            Texto += "\r\nGo\r\n\r\n\r\n";
            return Texto;
        }


        public DateTime create_date
        {
            get;
            internal set;
        }

        public DateTime modify_date
        {
            get;
            internal set;
        }


        public Int32 schema_id
        {
            get;
            internal set;
        }

        public string schema_name
        {
            get;
            internal set;
        }

        public Int32 parent_object_id
        {
            get;
            internal set;
        }


    }

    //----------------------------------------------------------------------

}
