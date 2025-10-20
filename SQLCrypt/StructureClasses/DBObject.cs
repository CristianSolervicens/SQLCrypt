using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                DBObject.hSql = hSql; 
                this.Add( new DBObject
                    {
                        name = hSql.Data.GetString(0),
                        object_id = hSql.Data.GetInt32(1),
                        schema_name = hSql.Data.GetString(2),
                        schema_id = hSql.Data.GetInt32(3),
                        parent_object_id = hSql.Data.GetInt32(4),
                        type = hSql.Data.GetString(5).Trim(),
                        type_desc = hSql.Data.GetString(6),
                        create_date = hSql.Data.GetDateTime(7),
                        modify_date = hSql.Data.GetDateTime(8),
                        description = ""
                    }
                );
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
                DBObject.hSql = hSql;
                this.Add(new DBObject
                    {
                        name = hSql.Data.GetString(0),
                        object_id = hSql.Data.GetInt32(1),
                        schema_name = hSql.Data.GetString(2),
                        schema_id = hSql.Data.GetInt32(3),
                        parent_object_id = hSql.Data.GetInt32(4),
                        type = hSql.Data.GetString(5).Trim(),
                        type_desc = hSql.Data.GetString(6),
                        create_date = hSql.Data.GetDateTime(7),
                        modify_date = hSql.Data.GetDateTime(8),
                        description = hSql.Data.GetString(9)
                    }
                );
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
                DBObject.hSql = hSql;
                this.Add(new DBObject
                    {
                        name = hSql.Data.GetString(0),
                        object_id = hSql.Data.GetInt32(1),
                        schema_name = hSql.Data.GetString(2),
                        schema_id = hSql.Data.GetInt32(3),
                        parent_object_id = hSql.Data.GetInt32(4),
                        type = hSql.Data.GetString(5).Trim(),
                        type_desc = hSql.Data.GetString(6),
                        create_date = hSql.Data.GetDateTime(7),
                        modify_date = hSql.Data.GetDateTime(8),
                        description = ""
                    }
                );
            }
        }

    }


    /// <summary>
    /// Clase Base de Objectos de Base de Datos
    /// </summary>
    public class DBObject
    {
        public static HSql hSql;
        private string NwLine = Environment.NewLine;

        public override string ToString()
        {
            return $"{this.schema_name}.{this.name}";
        }

        public DBObject()
        {
        }

        public string name
        {
            get; internal set;
        }

        public string type
        {
            get; internal set;
        }

        public string type_desc
        {
            get; internal set;
        }

        public Int32 object_id
        {
            get; internal set;
        }

        public string description
        {
            get; internal set;
        }

        public string collation_name
        {
            get; internal set;
        }

        public DateTime create_date
        {
            get; internal set;
        }

        public DateTime modify_date
        {
            get; internal set;
        }


        public Int32 schema_id
        {
            get; internal set;
        }

        public string schema_name
        {
            get; internal set;
        }

        public Int32 parent_object_id
        {
            get; internal set;
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
            string sObjName = this.schema_name + "." + this.name;

            if (this.type != "U")
                return "No Aplica al Objeto";

            string sCommand = $@"
                DECLARE @table_name SYSNAME
                SELECT @table_name = '{sObjName}'

                DECLARE 
                      @object_name SYSNAME
                     ,@object_id   INT

                SELECT 
                      @object_name = '[' + s.name + '].[' + o.name + ']'
                    , @object_id = o.[object_id]
                FROM sys.objects o WITH (NOWAIT)
                JOIN sys.schemas s WITH (NOWAIT) ON o.[schema_id] = s.[schema_id]
                WHERE s.name + '.' + o.name = @table_name
                    AND o.[type] = 'U'
                    --AND o.is_ms_shipped = 0

                DECLARE @SQL NVARCHAR(MAX) = ''

                ;WITH index_column AS 
                (
                    SELECT 
                          ic.[object_id]
                        , ic.index_id
                        , ic.is_descending_key
                        , ic.is_included_column
                        , c.name
                    FROM sys.index_columns ic WITH (NOWAIT)
                    JOIN sys.columns c WITH (NOWAIT) ON ic.[object_id] = c.[object_id] AND ic.column_id = c.column_id
                    WHERE ic.[object_id] = @object_id
                ),
                fk_columns AS 
                (
                     SELECT 
                          k.constraint_object_id
                        , cname = c.name
                        , rcname = rc.name
                    FROM sys.foreign_key_columns k WITH (NOWAIT)
                    JOIN sys.columns rc WITH (NOWAIT) ON rc.[object_id] = k.referenced_object_id AND rc.column_id = k.referenced_column_id 
                    JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] = k.parent_object_id AND c.column_id = k.parent_column_id
                    WHERE k.parent_object_id = @object_id
                )
                SELECT sqlString = 'CREATE TABLE ' + @object_name + CHAR(13) + '(' + CHAR(13) + STUFF((
                    SELECT CHAR(9) + ', [' + c.name + '] ' + 
                        CASE WHEN c.is_computed = 1
                            THEN 'AS ' + cc.[definition] 
                            ELSE UPPER(tp.name) + 
                                CASE WHEN tp.name IN ('varchar', 'char', 'varbinary', 'binary', 'text')
                                       THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(5)) END + ')'
                                     WHEN tp.name IN ('nvarchar', 'nchar', 'ntext')
                                       THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length / 2 AS VARCHAR(5)) END + ')'
                                     WHEN tp.name IN ('datetime2', 'time2', 'datetimeoffset') 
                                       THEN '(' + CAST(c.scale AS VARCHAR(5)) + ')'
                                    WHEN tp.name IN ('decimal', 'numeric')
                                       THEN '(' + CAST(c.[precision] AS VARCHAR(5)) + ',' + CAST(c.scale AS VARCHAR(5)) + ')'
                                    ELSE ''
                                END +
                                CASE WHEN c.collation_name IS NOT NULL THEN ' COLLATE ' + c.collation_name ELSE '' END +
                                CASE WHEN c.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' END +
                                CASE WHEN dc.[definition] IS NOT NULL THEN ' DEFAULT' + dc.[definition] ELSE '' END + 
                                CASE WHEN ic.is_identity = 1 THEN ' IDENTITY(' + CAST(ISNULL(ic.seed_value, '0') AS CHAR(1)) + ',' + CAST(ISNULL(ic.increment_value, '1') AS CHAR(1)) + ')' ELSE '' END 
                        END + CHAR(13)
                    FROM sys.columns c WITH (NOWAIT)
                    JOIN sys.types tp WITH (NOWAIT) ON c.user_type_id = tp.user_type_id
                    LEFT JOIN sys.computed_columns cc WITH (NOWAIT) ON c.[object_id] = cc.[object_id] AND c.column_id = cc.column_id
                    LEFT JOIN sys.default_constraints dc WITH (NOWAIT) ON c.default_object_id != 0 AND c.[object_id] = dc.parent_object_id AND c.column_id = dc.parent_column_id
                    LEFT JOIN sys.identity_columns ic WITH (NOWAIT) ON c.is_identity = 1 AND c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id
                    WHERE c.[object_id] = @object_id
                    ORDER BY c.column_id
                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, CHAR(9) + ' ')
                    + ISNULL((SELECT CHAR(9) + ', CONSTRAINT [' + k.name + '] PRIMARY KEY (' + 
                                    (SELECT STUFF((
                                         SELECT ', [' + c.name + '] ' + CASE WHEN ic.is_descending_key = 1 THEN 'DESC' ELSE 'ASC' END
                                         FROM sys.index_columns ic WITH (NOWAIT)
                                         JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id
                                         WHERE ic.is_included_column = 0
                                             AND ic.[object_id] = k.parent_object_id 
                                             AND ic.index_id = k.unique_index_id     
                                         FOR XML PATH(N''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''))
                            + ')' + CHAR(13)
                            FROM sys.key_constraints k WITH (NOWAIT)
                            WHERE k.parent_object_id = @object_id 
                                AND k.[type] = 'PK'), '') + ')'  + CHAR(13)
                    + ISNULL((SELECT (
                        SELECT CHAR(13) +
                             'ALTER TABLE ' + @object_name + ' WITH' 
                            + CASE WHEN fk.is_not_trusted = 1 
                                THEN ' NOCHECK' 
                                ELSE ' CHECK' 
                              END + 
                              ' ADD CONSTRAINT [' + fk.name  + '] FOREIGN KEY(' 
                              + STUFF((
                                SELECT ', [' + k.cname + ']'
                                FROM fk_columns k
                                WHERE k.constraint_object_id = fk.[object_id]
                                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')
                               + ')' +
                              ' REFERENCES [' + SCHEMA_NAME(ro.[schema_id]) + '].[' + ro.name + '] ('
                              + STUFF((
                                SELECT ', [' + k.rcname + ']'
                                FROM fk_columns k
                                WHERE k.constraint_object_id = fk.[object_id]
                                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')
                               + ')'
                            + CASE 
                                WHEN fk.delete_referential_action = 1 THEN ' ON DELETE CASCADE' 
                                WHEN fk.delete_referential_action = 2 THEN ' ON DELETE SET NULL'
                                WHEN fk.delete_referential_action = 3 THEN ' ON DELETE SET DEFAULT' 
                                ELSE '' 
                              END
                            + CASE 
                                WHEN fk.update_referential_action = 1 THEN ' ON UPDATE CASCADE'
                                WHEN fk.update_referential_action = 2 THEN ' ON UPDATE SET NULL'
                                WHEN fk.update_referential_action = 3 THEN ' ON UPDATE SET DEFAULT'  
                                ELSE '' 
                              END 
                            + CHAR(13) + 'ALTER TABLE ' + @object_name + ' CHECK CONSTRAINT [' + fk.name  + ']' + CHAR(13)
                        FROM sys.foreign_keys fk WITH (NOWAIT)
                        JOIN sys.objects ro WITH (NOWAIT) ON ro.[object_id] = fk.referenced_object_id
                        WHERE fk.parent_object_id = @object_id
                        FOR XML PATH(N''), TYPE).value('.', 'NVARCHAR(MAX)')), '')
                    + ISNULL(((SELECT
                         CHAR(13) + 'CREATE' + CASE WHEN i.is_unique = 1 THEN ' UNIQUE' ELSE '' END 
                                + ' NONCLUSTERED INDEX [' + i.name + '] ON ' + @object_name + ' (' +
                                STUFF((
                                SELECT ', [' + c.name + ']' + CASE WHEN c.is_descending_key = 1 THEN ' DESC' ELSE ' ASC' END
                                FROM index_column c
                                WHERE c.is_included_column = 0
                                    AND c.index_id = i.index_id
                                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')'  
                                + ISNULL(CHAR(13) + 'INCLUDE (' + 
                                    STUFF((
                                    SELECT ', [' + c.name + ']'
                                    FROM index_column c
                                    WHERE c.is_included_column = 1
                                        AND c.index_id = i.index_id
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')', '') + CASE WHEN ISNULL(i.filter_definition,'') = '' THEN '' ELSE ' WHERE ' + i.filter_definition END + CHAR(13)
                        FROM sys.indexes i WITH (NOWAIT)
                        WHERE i.[object_id] = @object_id
                            AND i.is_primary_key = 0
                            AND i.[type] = 2
                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)')
                    ), '')
                    ";
            hSql.ExecuteSqlData(sCommand);
            if (hSql.ErrorExiste)
            {
                MessageBox.Show(hSql.ErrorString, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hSql.ErrorClear();
                return "";
            }

            hSql.Data.Read();
            if (hSql.Data != null && hSql.Data.HasRows)
                if (hSql.Data.IsDBNull(0))
                    return $"No se ha podido obtener la data para {sObjName}{NwLine}";
                else
                    return hSql.Data.GetString(0) + $"{NwLine}{NwLine}";
            return $"No se ha podido obtener la data para {sObjName}{NwLine}";
        }


        
        public string GetData(bool limitedByTen)
        {
            string sComando = "";

            if (limitedByTen)
               sComando = "SELECT TOP(1000) * FROM [" + this.schema_name + "].[" + this.name + "]";
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

            string SetOptions = $"SET ANSI_NULLS ON;{NwLine}GO{NwLine}SET QUOTED_IDENTIFIER ON;{NwLine}GO{NwLine}{NwLine}";

            switch (this.type.Trim())
            {
                case "P":  //Procedimiento
                    TipoObjecto = "Proc";
                    Drop = $"{NwLine}If OBJECT_ID('{this.schema_name}.{this.name}') IS NOT NULL{NwLine}   DROP PROC [{this.schema_name}].[{this.name}]{NwLine}Go{NwLine}{NwLine}";
                    break;

                case "V":  //Vista
                    TipoObjecto = "Vista";
                    Drop += $"{NwLine}If OBJECT_ID('{this.schema_name}.{this.name}') IS NOT NULL{NwLine}   DROP VIEW [{this.schema_name}].[{this.name}]{NwLine}Go{NwLine}{NwLine}";
                    break;

                case "TF":  //Funcion Tabular
                case "FN":  //Funcion Escalar
                    TipoObjecto = "Function";
                    Drop += $"{NwLine}If OBJECT_ID('{this.schema_name}.{this.name}') IS NOT NULL{NwLine}   DROP FUNCTION [{this.schema_name}].[{this.name}]{NwLine}Go{NwLine}{NwLine}";
                        break;

                case "TR":  //Trigger
                    TipoObjecto = "Trigger";
                    Drop = $"{NwLine}If OBJECT_ID('{this.schema_name}.{this.name}') IS NOT NULL{NwLine}   DROP TRIGGER [{this.schema_name}].[{this.name}]{NwLine}Go{NwLine}{NwLine}";
                    break;

                default:
                    break;
            }

            Prefijo += $"/*==============================================================================={NwLine}";
            Prefijo += $"  Generated By SQL Crypt - Cristian Solervicéns {NwLine}";
            Prefijo += string.Format("  {0}: {1}.{2}{3}", TipoObjecto, this.schema_name, this.name, NwLine);
            Prefijo += string.Format("  Fecha : {0:dd/MM/yyyy H:mm:ss}{1}", DateTime.Now, NwLine);
            Prefijo += $"  ===============================================================================*/{NwLine}";
            Prefijo += Drop;

            string sCommmand = $@"SELECT text
                                FROM syscomments
                                WHERE id = {this.object_id}
                                AND encrypted = 0
                                ORDER BY number";

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

            Texto += $"{NwLine}Go{NwLine}{NwLine}{NwLine}";
            return Texto;
        }

    }

    //----------------------------------------------------------------------

}
