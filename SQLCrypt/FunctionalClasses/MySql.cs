using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLCrypt.FunctionalClasses
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Security.Cryptography;
    using System.IO;
    using System.Data.SqlClient;
    using System.Data;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    namespace MySql
    {
        /// <summary>
        /// Clase de Abstracción para acceder a Base de Datos
        /// </summary>
        public class MySql
        {

            //Archivo encriptado que contiene el String de Conexión a la BD
            private string sConnectionFile = "ConnectionString.cfg";             //Crypt Connection String File
                                                                                 //Mantiene el estado de conexión
            private Boolean bConnected = false;
            //Cadena que contiene el estado de error (si lo hubiera)
            private String sError = "";
            //Objeto de conexión a la Base de Datos.
            public SqlConnection Conn = null;
            //Cadena de conexión a la Base de Datos.
            private String sConnectionStr;
            private String sMensajes;

            //Constante usada como semilla para la encriptación
            private const string passKey = "AthELeIa";
            //Path hacia los Archivos de ComandosAlmacenados.
            public string sPathToCommands = "";
            //ResultSet para las consultas con datos
            public SqlDataReader Data = null;

            void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
            {
                sMensajes += e.Message;
            }

            #region "PROPIEDADES"

            public string UsuarioSPF
            {
                get;
                set;
            }

            public string Messages
            {
                get { return sMensajes; }
            }


            public void ClearMessages()
            {
                sMensajes = "";
            }


            //ConnectionString
            public string ConnectionString
            {
                set
                {
                    sConnectionStr = value;

                    //Si no es una cadena correctamente compuesta, la asignación levanta un error.
                    try
                    {
                        Conn = new SqlConnection(sConnectionStr);
                        Conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
                        Conn.FireInfoMessageEventOnUserErrors = true;
                    }
                    catch (SqlException e)
                    {
                        sError = "Error: " + e.Message;
                        bConnected = false;
                    }
                }
            }

            //ConnectionFile
            public string ConnectionFile
            {
                get { return sConnectionFile; }
                set { sConnectionFile = value; }
            }

            //Entrega el String con el último error
            public string ErrorString
            {
                get { return sError; }
            }

            public bool ErrorExiste
            {
                get { return sError != "" ? true : false; }
            }


            /// <summary>
            /// Propiedad Sólo lectura: Indica si está conectado a una base de datos (booleano)
            /// </summary>
            public Boolean ConnectionStatus
            {
                get { return bConnected; }
            }


            //DataExiste (Solo lectura)
            public bool DataExiste
            {
                get
                {
                    if (Data != null)
                    {
                        if (!Data.IsClosed)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }

            #endregion

            #region "METODOS"

            /// <summary>
            /// Arma String de Conexión a Base de Datos
            /// </summary>
            /// <param name="ServerName"></param>
            /// <param name="DatabaseName"></param>
            /// <param name="Username"></param>
            /// <param name="Clave"></param>
            /// <returns></returns>
            static public string SQLServerConnectionString(string ServerName, string DatabaseName, string Username, string Clave, bool Async)
            {
                string sConnStr = "";

                if (ServerName == null)
                    ServerName = "";

                if (DatabaseName == null)
                    DatabaseName = "";

                if (Username == null)
                    Username = "";

                if (Clave == null)
                    Clave = "";

                //--------------------------

                 string post;

                 post = "";
                //server=(local);user id=ab;password= a!Pass113;initial catalog=AdventureWorks\n
                sConnStr = "";
                 if (ServerName == "")
                    sConnStr += "server=.";
                 else
                    sConnStr += "server=" + ServerName;

                 if (!(DatabaseName == "" || string.IsNullOrWhiteSpace(DatabaseName)))
                    sConnStr += ";initial catalog=" + DatabaseName;

                 if (!(string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username)))
                    sConnStr += ";user id=" + Username;
                 else
                     post = ";Trusted_Connection = true";

                 if (!(string.IsNullOrEmpty(Clave) || string.IsNullOrWhiteSpace(Clave)))
                    sConnStr += ";password=" + Clave;

                 sConnStr = ";Application Name=" + Application.ProductName + post;

                 if (Async)
                     sConnStr += ";Asynchronous Processing=True";

                 return sConnStr;
            }


            /// <summary>
            /// Constructor
            /// </summary>
            public MySql()
            {
                Data = null;
                sConnectionStr = "";
                sConnectionFile = "";
                sError = "";
                sMensajes = "";
                sPathToCommands = "";
            }


            /// <summary>
            /// Limpia la condición de Error
            /// </summary>
            public void ErrorClear()
            {
                sError = "";
            }


            /// <summary>
            /// Conectarse a la Base de Datos.
            /// </summary>
            /// <returns></returns>
            public int ConnectToDB()
            {

                if (sConnectionStr == "")
                {
                    if (sConnectionFile != "")
                    {
                        if (System.IO.File.Exists(sConnectionFile))
                            sConnectionStr = DecryptFiletoString(sConnectionFile);
                        else
                        {
                            sError = $"El Archivo de conexión indicado no existe\n  [{sConnectionFile}]";
                            return 0;
                        }

                    }

                }

                //Si no es una cadena correctamente compuesta, la asignación levanta un error.
                try
                {
                    Conn = new SqlConnection(sConnectionStr);
                    Conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
                    Conn.FireInfoMessageEventOnUserErrors = true;
                }
                catch (System.Data.Odbc.OdbcException e)
                {
                    sError = $"Error: {e.Message}";
                    bConnected = false;
                    return 0;
                }
                catch
                {
                    sError = "Error desconocido o de formato del String de conexión.";
                    bConnected = false;
                    return 0;
                }

                //Capturar el error al conectarse a la Base de Datos
                try
                {
                    Conn.Open();
                }
                catch (SqlException e)
                {
                    bConnected = false;
                    sError = $"Error: {e.Message}";
                    return 0;
                }
                catch
                {
                    sError = "Error desconocido o de formato del String de conexión.";
                    bConnected = false;
                    return 0;
                }

                bConnected = true;
                return 1;
            }


            /// <summary>
            /// Cierra la conexión a la Base de Datos.
            /// Si se cierra, se pierde el Result Set de ExecuteSqlData.
            /// </summary>
            /// <returns></returns>
            public int CloseDBConn()
            {
                if (bConnected)
                {
                    Conn.Close();
                    bConnected = false;
                    return 1;
                }
                else
                    return 0;
            }


            /// <summary>
            /// Ejecuta una sentencia SQL que NO retorna columnas.
            /// Retorna el número de filas afectadas.
            /// </summary>
            /// <param name="sComand"></param>
            /// <returns></returns>
            public int ExecuteSql(String sComand)
            {
                int raf;

                DataClose();
                ErrorClear();
                ClearMessages();

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = sComand;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandTimeout = 0;

                try
                {
                    raf = Cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    sError = $"Error: {e.Message}";
                    return -1;
                }
                catch (Exception e)
                {
                    sError = $"Error: {e.Message}";
                    return -1;
                }

                return raf;
            }


            /// <summary>
            /// Ejecuta una Sentencia SQL y retorna el ResultSet asociado.
            /// </summary>
            /// <param name="sCommand"></param>
            /// <returns></returns>
            public bool ExecuteSqlData(String sCommand)
            {

                DataClose();
                ErrorClear();
                ClearMessages();

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = sCommand;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandTimeout = 0;

                try
                 {
                    Data = Cmd.ExecuteReader();
                }
                catch (System.Data.Odbc.OdbcException e)
                {
                    sError = $"Error: {e.Message}";
                    return false;
                }
                catch (Exception exw)
                {
                    sError = $"Error: {exw.Message}";
                    this.CloseDBConn();
                    this.bConnected = false;
                    Cmd.Dispose();
                    return false;
                }

                try
                {
                    bool b = Data.HasRows;
                }
                catch (System.Data.Odbc.OdbcException e)
                {
                    sError = $"Error: {e.Message}";
                    Cmd.Dispose();
                    return false;
                }

                Cmd.Dispose();
                return true;
            }


            /// <summary>
            /// Obtiene Objeto almacenado en una página determinada de la Base de Datos
            /// </summary>
            /// <param name="DataBaseId"></param>
            /// <param name="FileId"></param>
            /// <param name="PageId"></param>
            /// <returns></returns>
            public string BuscaPagina( string DataBaseId, string FileId, string PageId)
            {
                string salida = "";
                string sComando = "DBCC TRACEON (3604);\n";
                sComando += $"DBCC PAGE ({DataBaseId}, {FileId}, {PageId}, 0);\n";
                sComando += "DBCC TRACEOFF (3604);";

                int IndexId = -1;
                int Object_id = -1;

                ExecuteSqlData(sComando);
                if (ErrorExiste)
                    return ErrorString;

                int ix = 0;
                string ToFind = "Metadata: IndexId =";
                ix = sMensajes.IndexOf(ToFind);
                if (ix != -1)
                {
                    ix += ToFind.Length;
                    string sAux = sMensajes.Substring(ix, 12).TrimStart();
                    ix = sAux.IndexOf(" ") != -1 ? sAux.IndexOf(" ") : sAux.IndexOf("\n");
                    sAux = sAux.Substring(0, ix);
                    IndexId = Convert.ToInt32( sAux);
                }

                ix = 0;
                ToFind = "Metadata: ObjectId =";
                ix = sMensajes.IndexOf(ToFind);
                if (ix != -1)
                {
                    ix += ToFind.Length;
                    string sAux = sMensajes.Substring(ix, 12).TrimStart();
                    ix = sAux.IndexOf(" ") != -1 ? sAux.IndexOf(" ") : sAux.IndexOf("\n");
                    sAux = sAux.Substring(0, ix);
                    Object_id = Convert.ToInt32(sAux);
                }

                if (Object_id != -1)
                {
                    string DB_Name = GetDBNameById(DataBaseId);
                    sComando = $"USE {DB_Name}; SELECT '{DB_Name}.' + ISNULL(OBJECT_SCHEMA_NAME({Object_id}), '') + '.'+ ISNULL(OBJECT_NAME({Object_id}), '')";
                    ExecuteSqlData(sComando);

                    if (ErrorExiste)
                        return ErrorString;

                    if (Data != null)
                    {
                        Data.Read();
                        salida = Data.GetString(0).Trim();
                    }
                    else
                        return "Objeto no encontrado";

                }
                else
                    return "Objeto no encontrado";


                if (IndexId != -1)
                {
                    sComando = $"SELECT name FROM sys.indexes WHERE Object_id = {Object_id} And Index_id = {IndexId}";
                    ExecuteSqlData(sComando);

                    if (ErrorExiste)
                        return ErrorString;

                    if (Data != null)
                    {
                        Data.Read();
                        salida += $" Indice: {Data.GetString(0)}";
                    }
                    else
                        return "Objeto no encontrado";

                }

                return salida;
            }


            /// <summary>
            /// Obtiene el Nombre de la Base de Dato a través de su Id
            /// </summary>
            /// <param name="DB_Id"></param>
            /// <returns></returns>
            public string GetDBNameById( string DB_Id)
            {
                string sComando = $"SELECT db_name({DB_Id})";
                ExecuteSqlData(sComando);
                if (ErrorExiste)
                    return "";

                if (Data != null)
                {
                    Data.Read();
                    return Data.GetString(0).Trim();
                }
                else
                    return "";

            }

            /// <summary>
            /// Retorna el Schema.Nombre del Objeto
            /// </summary>
            /// <param name="DataBaseId"></param>
            /// <param name="Object_id"></param>
            /// <returns></returns>
            public string BuscaObjeto(string DataBaseId, string Object_id)
            {

                string DB_Name = GetDBNameById(DataBaseId);
                string sComando = $"USE {DB_Name};";
                sComando +=  $"SELECT '{DB_Name}.' + ISNULL(OBJECT_SCHEMA_NAME({Object_id}), '') + '.'+ ISNULL(OBJECT_NAME({Object_id}), '')";
                ExecuteSqlData(sComando);

                if (ErrorExiste)
                    return ErrorString;

                if (Data != null)
                {
                    Data.Read();
                    return Data.GetString(0);
                }
                else
                    return "Objeto no encontrado";

            }



            /// <summary>
            /// Ejecuta una sentencia SQL que NO retorna columnas.
            /// Retorna el número de filas afectadas.
            /// </summary>
            /// <param name="sComand"></param>
            /// <returns></returns>
            public int ExecStoredCmd(String sCommandFile, params String[] Params)
            {
                int raf;
                string sComando = "";
                ErrorClear();
                ClearMessages();

                sComando = DecryptFiletoString(sPathToCommands + sCommandFile);

                if (sComando == "")
                {
                    sError = "Error: Comando vacío";
                    return -1;
                }

                string sOldPar;
                int y;

                if (Params != null)
                {
                    for (int x = 0; x < Params.Length; ++x)
                    {
                        y = x + 1;
                        sOldPar = $"#{y.ToString()}#";
                        sComando = sComando.Replace(sOldPar, Params[x]);
                    }
                }

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = sComando;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandTimeout = 0;

                try
                {
                    raf = Cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    sError = $"Error: {e.Message}";
                    return -1;
                }

                return raf;
            }


            /// <summary>
            /// Ejecuta una Sentencia SQL y retorna el ResultSet asociado.
            /// </summary>
            /// <param name="sCommand"></param>
            /// <returns></returns>
            public bool ExecStoredCmdData(String sCommandFile, Dictionary<string, string> Params)
            {
                string sComando = "";

                DataClose();
                ErrorClear();
                ClearMessages();

                sComando = DecryptFiletoString(sPathToCommands + sCommandFile);

                if (sComando == "")
                {
                    sError = "Error: Comando vacío";
                    return false;
                }

                if (Params.Count != 0)
                {
                    List<string> keys = new List<string>(Params.Keys);
                    for (int x = 0; x < Params.Count; ++x)
                    {
                        sComando = sComando.Replace(keys[x], Params[keys[x]]);
                    }
                }

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = sComando;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandTimeout = 0;

                try
                {
                    Data = Cmd.ExecuteReader();
                }
                catch (SqlException e)
                {
                    sError = $"Error: {e.Message}";
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Cierra el objeto Data (Retorno de Resultados).
            /// </summary>
            public void DataClose()
            {
                try
                {
                    if (Data == null)
                        return;

                    if (!Data.IsClosed)
                        Data.Close();
                }
                catch { }
            }


            #endregion

            #region "Encriptación"

            /// <summary>
            /// Encrypts the input string and creates a new encrypted file(strOutputFileName) 
            /// </summary>
            /// <param name="strInputString"></param>
            /// <param name="strOutputFileName"></param>
            public void EncryptStringtoFile(string strInputString, string strOutputFileName)
            {
                if (File.Exists(strOutputFileName))
                {
                    File.Delete(strOutputFileName);
                }
                using (FileStream outputStream = new FileStream(strOutputFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                    cryptic.Key = ASCIIEncoding.ASCII.GetBytes(passKey);
                    cryptic.IV = ASCIIEncoding.ASCII.GetBytes(passKey);

                    CryptoStream crStream = new CryptoStream(outputStream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);

                    byte[] buffer = ASCIIEncoding.ASCII.GetBytes(strInputString);

                    crStream.Write(buffer, 0, buffer.Length);

                    crStream.Close();
                }
            }//EncryptStringtoFile



            /// <summary>
            /// Función para desencriptar el Archivo de conexión a la BD.
            /// </summary>
            /// <param name="strInputFileName"></param>
            /// <returns></returns>
            public string DecryptFiletoString(string strInputFileName)
            {
                string strFileData = "";
                using (FileStream inputStream = new FileStream(strInputFileName, FileMode.Open, FileAccess.Read))
                {
                    DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                    cryptic.Key = ASCIIEncoding.ASCII.GetBytes(passKey);
                    cryptic.IV = ASCIIEncoding.ASCII.GetBytes(passKey);

                    CryptoStream crStream = new CryptoStream(inputStream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

                    StreamReader reader = new StreamReader(crStream);

                    strFileData = reader.ReadToEnd();

                    reader.Close();
                    inputStream.Close();
                }

                return strFileData;
            }//DecryptFiletoString

            #endregion

            #region "Funciones fValorASP"

            public enum SP_DateFormat
            {
                yyyyMMdd = 1,
                yyyyMMddHHmmss = 2,
                yyyyMMddHHmmssms = 3
            }


            /// <summary>
            /// Convierte Valor a representación apta para Base de Datos
            /// </summary>
            /// <param name="dtValor"></param>
            /// <param name="Formato"></param>
            /// <returns></returns>
            public string fValorASP(DateTime dtValor, SP_DateFormat Formato)
            {
                string sAux = "";
                string sAux2 = "";

                if (dtValor == null)
                    return "NULL";

                sAux = dtValor.Year.ToString();

                sAux2 = $"0{dtValor.Month}";
                sAux += sAux2.Substring(sAux2.Length - 2);

                sAux2 = $"0{dtValor.Day}";
                sAux += sAux2.Substring(sAux2.Length - 2);

                if (Formato == SP_DateFormat.yyyyMMddHHmmss || Formato == SP_DateFormat.yyyyMMddHHmmssms)
                {

                    sAux2 = $"0{dtValor.Hour}";
                    sAux += $" {sAux2.Substring(sAux2.Length - 2)}";

                    sAux2 = $"0{dtValor.Minute}";
                    sAux += $":{sAux2.Substring(sAux2.Length - 2)}";

                    sAux2 = $"0{dtValor.Second}";
                    sAux += $":{sAux2.Substring(sAux2.Length - 2)}";

                    if (Formato == SP_DateFormat.yyyyMMddHHmmssms)
                    {
                        sAux2 = $"00{dtValor.Millisecond}";
                        sAux += $":{sAux2.Substring(sAux2.Length - 2)}";
                    }

                }

                sAux = $"'{sAux}'";
                return sAux;
            }


            /// <summary>
            /// Convierte Valor a representación apta para Base de Datos
            /// </summary>
            /// <param name="sValor"></param>
            /// <returns></returns>
            public string fValorASP(string sValor)
            {
                string sAux = "";

                if (sValor == null)
                    return "NULL";

                sAux = sValor.TrimEnd();
                sAux = $"'{sAux.Replace("'", "#")}'";

                return sAux;
            }


            /// <summary>
            /// Convierte Valor a representación apta para Base de Datos
            /// </summary>
            /// <param name="Valor"></param>
            /// <returns></returns>
            public string fValorASP(Int32 Valor)
            {
                string sAux;
                sAux = Valor.ToString();
                return sAux;
            }


            /// <summary>
            /// Convierte Valor a representación apta para Base de Datos
            /// </summary>
            /// <param name="Valor"></param>
            /// <returns></returns>
            public string fValorASP(Int64 Valor)
            {
                string sAux;
                sAux = Valor.ToString();
                return sAux;
            }


            /// <summary>
            /// Convierte Valor a representación apta para Base de Datos
            /// </summary>
            /// <param name="Valor"></param>
            /// <param name="decimales"></param>
            /// <returns></returns>
            public string fValorASP(decimal Valor, int decimales)
            {
                string sAux;
                string sAuxF;

                sAuxF = "0";
                if (decimales > 0)
                {
                    sAuxF += ".";

                    for (int x = 0; x < decimales; ++x)
                        sAuxF += "0";
                }

                sAux = Valor.ToString(sAuxF);
                sAux = sAux.Replace(",", ".");

                return sAux;
            }


            /// <summary>
            /// Convierte Valor a representación apta para Base de Datos
            /// </summary>
            /// <param name="Val"></param>
            /// <returns></returns>
            public string fValorASP(bool Val)
            {
                if (Val == true)
                    return "1";
                else
                    return "0";
            }

            #endregion

            #region "Funciones SQL Generales"


            /// <summary>
            /// Obtiene Fecha Hora del Servidor de Base de Datos
            /// </summary>
            /// <returns></returns>
            public DateTime GetDateTime()
            {
                DateTime Ahora = System.DateTime.Now;

                string comando = "SELECT getdate()";

                ExecuteSqlData(comando);

                if (Data != null)
                {
                    Data.Read();
                    Ahora = Data.GetDateTime(0);
                }

                DataClose();
                return Ahora;

            }

            public bool SetDatabase(string Database)
            {
                string sComando = $"USE [{Database}]";
                ExecuteSql(sComando);
                if (this.ErrorExiste)
                    return false;
                return true;
            }


            public List<string> GetDatabases()
            {
                List<string> databases = new List<string>();
                string Comando = "select name from sys.databases order by name";
                ExecuteSqlData(Comando);
                if (Data != null)
                {
                    while (Data.Read())
                    {
                        databases.Add(Data.GetString(0));
                    }
                }

                return databases;
            }


            public string GetCurrentDatabase()
            {
                string Comando = "SELECT db_name = DB_NAME()";
                string db_name = "";
                ExecuteSqlData(Comando);
                if (Data != null)
                {
                    Data.Read();
                    db_name = Data.GetString(0);
                }
                DataClose();
                return db_name;
            }


            /// <summary>
            /// Lee campo String de Base de Datos a String (Reemplaza # por ')
            /// </summary>
            /// <param name="Valor"></param>
            /// <returns></returns>
            public static string DbStringToString(string Valor)
            {
                return Valor.Replace("#", "'");
            }


            /// <summary>
            /// Escribe Un Campo BLOB desde un Archivo
            /// </summary>
            /// <param name="File"></param>
            /// <param name="Tabla"></param>
            /// <param name="FieldName"></param>
            /// <param name="WhereConditions"></param>
            /// <returns></returns>
            public bool WriteBlob(string File, string Tabla, string FieldName, string WhereConditions)
            {
                if (!System.IO.File.Exists(File))
                {
                    this.sError = "Archivo de Imagen no Existe";
                    return false;
                }

                try
                {
                    string ComandoSQL = $"UPDATE {Tabla} SET {FieldName} = ? WHERE {WhereConditions}";
                    SqlCommand command = new SqlCommand(ComandoSQL);
                    SqlParameterCollection parameters = command.Parameters;
                    parameters.Add(FieldName, SqlDbType.Image);
                    parameters[FieldName].Value = GetPhoto(File);
                    command.Connection = Conn;
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    this.sError = $"Error grabando BLOB\n{e.ErrorCode}-{e.Message}";
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Escribe campo BLOB desde byte[] Imagen
            /// </summary>
            /// <param name="Image"></param>
            /// <param name="Tabla"></param>
            /// <param name="FieldName"></param>
            /// <param name="WhereConditions"></param>
            /// <returns></returns>
            public bool WriteBlob(byte[] Image, string Tabla, string FieldName, string WhereConditions)
            {
                try
                {
                    string ComandoSQL = $"UPDATE {Tabla} SET {FieldName} = ? WHERE {WhereConditions}";
                    SqlCommand command = new SqlCommand(ComandoSQL);
                    SqlParameterCollection parameters = command.Parameters;
                    parameters.Add(FieldName, SqlDbType.Image);
                    parameters[FieldName].Value = Image;
                    command.Connection = Conn;
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    this.sError = $"Error grabando BLOB\n{e.ErrorCode}-{e.Message}";
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Limpia una Imagen en BLOB
            /// </summary>
            /// <param name="Tabla"></param>
            /// <param name="FieldName"></param>
            /// <param name="WhereConditions"></param>
            /// <returns></returns>
            public bool WriteBlob(string Tabla, string FieldName, string WhereConditions)
            {
                try
                {
                    string ComandoSQL = $"UPDATE {Tabla} SET {FieldName} = NULL WHERE {WhereConditions}";
                    SqlCommand command = new SqlCommand(ComandoSQL);
                    SqlParameterCollection parameters = command.Parameters;
                    command.Connection = Conn;
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    this.sError = $"Error grabando BLOB\n{e.ErrorCode}-{e.Message}";
                    return false;
                }

                return true;
            }


            /// <summary>
            /// Lee un Campo BLOB a un Archivo
            /// </summary>
            /// <param name="File"></param>
            /// <param name="Tabla"></param>
            /// <param name="FieldName"></param>
            /// <param name="WhereConditions"></param>
            /// <returns></returns>
            public bool ReadBlob(string File, string Tabla, string FieldName, string WhereConditions)
            {
                string query = $"SELECT {FieldName} FROM {Tabla} WHERE {WhereConditions}";

                // create ODBC command, execute the query and get the reader for it
                SqlCommand command = new SqlCommand(query);
                command.Connection = Conn;

                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    // check whether there is at least one record
                    if (reader.Read())
                    {
                        // matching record found, read first column as string instance
                        if (reader.GetValue(0) == System.DBNull.Value)
                        {
                            reader.Close();
                            reader.Dispose();
                            return true;
                        }

                        byte[] value = (byte[])reader.GetValue(0);
                        if (SaveFile(File, value))
                        {
                            reader.Close();
                            reader.Dispose();
                            return true;
                        }
                    }

                    reader.Close();
                    this.sError = $"Error grabando Archivo: \n{File}";
                    return false;
                }
                catch (SqlException e)
                {
                    this.sError = $"Error Leyendo BLOB\n{e.ErrorCode}-{e.Message}";
                    return false;
                }
            }


            /// <summary>
            /// Lee un Blob desde Base de Datos a byte[]
            /// </summary>
            /// <param name="Tabla"></param>
            /// <param name="FieldName"></param>
            /// <param name="WhereConditions"></param>
            /// <returns></returns>
            public byte[] ReadBlob(string Tabla, string FieldName, string WhereConditions)
            {
                string query = $"SELECT {FieldName} FROM {Tabla} WHERE {WhereConditions}";

                // create ODBC command, execute the query and get the reader for it
                SqlCommand command = new SqlCommand(query);
                command.Connection = Conn;

                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    // check whether there is at least one record
                    if (reader.Read())
                    {
                        // matching record found, read first column as string instance
                        byte[] value = (byte[])reader.GetValue(0);
                        reader.Close();
                        return value;
                    }
                    reader.Close();
                    return null;
                }
                catch (SqlException e)
                {
                    this.sError = $"Error Leyendo BLOB\n{e.ErrorCode}-{e.Message}";
                    return null;
                }
            }


            /// <summary>
            /// Escribe Imagen desde byte[] a Archivo.
            /// </summary>
            /// <param name="File"></param>
            /// <param name="ImageContent"></param>
            /// <returns></returns>
            public bool SaveFile(string File, byte[] ImageContent)
            {
                try
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(
                                                                                File,
                                                                                System.IO.FileMode.Create,
                                                                                System.IO.FileAccess.ReadWrite))
                    {

                        using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                        {
                            bw.Write(ImageContent);
                            bw.Close();
                        }

                    }
                }
                catch
                {
                    return false;
                }

                return true;

            }


            /// <summary>
            ///Lee Imagen a byte[] desde Filesystem 
            /// </summary>
            /// <param name="filePath"></param>
            /// <returns></returns>
            public byte[] GetPhoto(string filePath)
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                byte[] photo = br.ReadBytes((int)fs.Length);

                br.Close();
                fs.Close();

                return photo;
            }

            #endregion


            public class strList : List<string>
            {
                public bool ElementExists(string Elemento)
                {
                    foreach (string m in this)
                    {
                        if (string.Compare(m, Elemento, true) == 0)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            /// <summary>
            /// GetParameters( string sComando)
            /// </summary>
            /// <param name="sComando"></param>
            /// <returns></returns>
            public strList GetParameters(string sComando)
            {
                strList lsParametros = new strList();

                Match match = Regex.Match(sComando, @"#.*#", RegexOptions.IgnoreCase);

                while (match.Success)
                {
                    if (!lsParametros.ElementExists(match.Value))
                        lsParametros.Add(match.Value);

                    match = match.NextMatch();
                }

                return lsParametros;
            }



        }  //MSql



        static class MyFuncs
        {

            public static int StrLen(string myStr)
            {
                int nRes, i;

                if (myStr[0] == '\0')
                {
                    nRes = 0;
                    return nRes;
                }

                for (i = 0; myStr[i] != '\0'; ++i)
                    nRes = i;
                nRes = i;

                return nRes;
            }

        } //MyFuncs

    }
}
