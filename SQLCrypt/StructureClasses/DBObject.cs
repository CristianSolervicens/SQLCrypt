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

            string sCommand = string.Empty;
            if (type == "SYSVIEWS")
            {
                sCommand = $@"select [name],
                                     [object_id],
                                     SCHEMA_NAME(schema_id) as SchemaName,
                                     [schema_id],
                                     [parent_object_id],
                                     [type],
                                     [type_desc],
                                     [create_date],
                                     [modify_date]
                            FROM sys.system_objects
                            ORDER BY SCHEMA_NAME(schema_id), [name]";
            }
            else
            {
                sCommand = $@"select [name],
                                    [object_id],
	                                SCHEMA_NAME(schema_id) as SchemaName,
                                    [schema_id],
	                                [parent_object_id],
                                    [type],
                                    [type_desc],
                                    [create_date],
                                    [modify_date]
                            from sys.objects
                            WHERE type = {hSql.fValorASP(this._type)}
                            ORDER BY SCHEMA_NAME(schema_id), [name]";
            }

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

            string sCommand = $@"select obj.[name],
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
                              WHERE type = {hSql.fValorASP(this._type)}
                                 And col.name = '{Column}'
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

            string sCommand = $@"select [name],
                                       [object_id],
	                                   SCHEMA_NAME(schema_id) as SchemaName,
                                       [schema_id],
	                                   [parent_object_id],
                                       [type],
                                       [type_desc],
                                       [create_date],
                                       [modify_date]
                              from sys.objects so
                              WHERE type = {hSql.fValorASP(this._type)}
                                 And exists ( select 1 from syscomments sc where text like '%{text}%' and sc.id = so.object_id )
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

            // Usar el script centralizado de SqlScripts
            string sCommand = string.Format(SqlScripts.GetCreateTableScript(), sObjName);

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


        public string GetExtendedProperties()
        {
            string salida = string.Empty;
            string sComando = $@"SELECT 
                                --s.name AS [Schema Name],
                                --t.name AS [Table Name],
                                Columna = ISNULL(CAST(c.name AS VARCHAR) + ': ', CAST('Objecto: ' + t.name AS VARCHAR) + ': ') + ' ' +  CAST(ep.value AS VARCHAR)
                            FROM sys.extended_properties ep
                            INNER JOIN sys.tables t ON ep.major_id = t.object_id
                            INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                            LEFT JOIN sys.columns c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id
                            WHERE ep.class = 1
                              AND s.name = '{this.schema_name}'
                              AND t.name = '{this.name}';";
            hSql.ExecuteSqlData(sComando);
            if (hSql.ErrorExiste)
            {
                string sError = hSql.ErrorString;
                hSql.ErrorClear();
                return sError;
            }
            while (hSql.Data.Read())
            {
                salida += $"{hSql.Data.GetString(0)}\n";
            }
            return salida.Trim();

        }


        public string GetData(bool limitedByTen)
        {
            string sComando = "";

            if (limitedByTen)
                sComando = $"SELECT TOP(1000) * FROM [{this.schema_name}].[{this.name}]";
            else
               sComando = $"SELECT * FROM [{this.schema_name}].[{this.name}]";

            hSql.ExecuteSqlData(sComando);

            if (hSql.ErrorExiste)
                return hSql.ErrorString;

            return sComando;
        }


        public int GetCountRows()
        {
            string sComando = "";
            int Count = 0;

            sComando = $"SELECT COUNT(*) FROM [{this.schema_name}].[{this.name}]";

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

            hSql.ExecuteSqlData($"sp_help N'{this.schema_name}.{this.name}'");

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
            Prefijo += $"  {TipoObjecto}: {this.schema_name}.{this.name}{NwLine}";
            Prefijo += $"  Fecha : {DateTime.Now:dd/MM/yyyy H:mm:ss}{NwLine}";
            Prefijo += $"  ===============================================================================*/{NwLine}";
            Prefijo += Drop;
            Prefijo += $"{NwLine}SET ANSI_NULLS ON;{NwLine}GO{NwLine}SET QUOTED_IDENTIFIER ON;{NwLine}GO{NwLine}{NwLine}";

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
