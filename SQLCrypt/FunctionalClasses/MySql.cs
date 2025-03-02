﻿using System;
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
    using System.Security.Policy;
    using System.Windows.Media.Media3D;
    using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
    using System.Data.Odbc;
    using static ScintillaNET.Style;

    namespace MySql
    {
        /// <summary>
        /// Clase de Abstracción para acceder a Base de Datos
        /// </summary>
        public class MySql
        {

            //Cadena que contiene el estado de error (si lo hubiera)
            private String sError = "";
            
            //Objeto de conexión a la Base de Datos.
            public SqlConnection Conn = null;
            
            //Cadena de conexión a la Base de Datos.
            private String sConnectionStr;
            private StringBuilder sMensajes = new StringBuilder();

            //Constante usada como semilla para la encriptación
            private const string passKey = "AthELeIa";
            
            //Path hacia los Archivos de ComandosAlmacenados.
            public string sPathToCommands = "";
            
            //ResultSet para las consultas con datos
            public SqlDataReader Data = null;

            private SqlCommand Command = null;


            void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
            {
                foreach (SqlError err in e.Errors)
                {
                    if (sMensajes.Length != 0)
                        sMensajes.Append("\r\n");
                    sMensajes.Append( $"{err.Message}   (Lin: {err.LineNumber}) {err.Procedure}");
                }
            }

            #region "PROPIEDADES"

            public string UsuarioSPF
            {
                get;
                set;
            }

            public string Messages
            {
                get { return sMensajes.ToString(); }
            }


            public void ClearMessages()
            {
                sMensajes.Clear();
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
                    }
                }
                get { return sConnectionStr; }
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
                get { 
                    if (Conn == null)
                        return false;
                    if (Conn.State == ConnectionState.Broken)
                        return false;
                    if (Conn.State == ConnectionState.Closed)
                        return false;
                    if (Conn.State == ConnectionState.Open)
                        return true;
                    
                    return false;
                }
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


            public void CancellExecution()
            {
                if (Command != null)
                {
                    try { Command.EndExecuteReader(null); }
                    catch { }
                    
                    try { Command.EndExecuteNonQuery(null); }
                    catch { }
                    Command.Cancel();
                    // Command.Dispose();
                    // Command = null;
                }
            }


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
                sError = "";
                sMensajes.Clear();
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
                    return 0;
                }

                Data = null;

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
                    return 0;
                }
                catch
                {
                    sError = "Error desconocido o de formato del String de conexión.";
                    return 0;
                }

                //Capturar el error al conectarse a la Base de Datos
                try
                {
                    Conn.Open();
                }
                catch (SqlException e)
                {
                    sError = $"Error: {e.Message}";
                    return 0;
                }
                catch
                {
                    sError = "Error desconocido o de formato del String de conexión.";
                    return 0;
                }

                return 1;
            }


            /// <summary>
            /// Cierra la conexión a la Base de Datos.
            /// Si se cierra, se pierde el Result Set de ExecuteSqlData.
            /// </summary>
            /// <returns></returns>
            public int CloseDBConn()
            {
                if (ConnectionStatus)
                {
                    DataClose();
                    Data = null;
                    try { Conn.Close(); } catch { }
                    try { Conn.Dispose(); } catch { }
                    Conn = null;
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
                int rows_affected = 0;

                DataClose();
                ErrorClear();
                ClearMessages();

                Command = Conn.CreateCommand();
                Command.CommandText = sComand;
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandTimeout = 0;

                try
                {
                    rows_affected = Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    sError += $"Error: {e.Message}";
                    Command = null;
                    return -1;
                }
                catch (System.Data.Odbc.OdbcException e)
                {
                    sError += $"Error: {e.Message}";
                    Command = null;
                    return 0;
                }
                catch (Exception e)
                {
                    sError += $"Error: {e.Message}";
                    Command = null;
                    return -1;
                }

                Command = null;
                return rows_affected;
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

                Command = Conn.CreateCommand();
                Command.CommandText = sCommand;
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandTimeout = 0;

                try
                {
                    Data = Command.ExecuteReader();
                }
                catch (SqlException e)
                {
                    sError += $"Error: {e.Message}";
                    Command = null;
                }
                catch (System.Data.Odbc.OdbcException e)
                {
                    sError += $"Error: {e.Message}";
                    Command = null;
                    return false;
                }
                catch (Exception exw)
                {
                    sError += $"Error: {exw.Message}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }

                try
                {
                    bool b = Data.HasRows;
                }
                catch (System.Data.Odbc.OdbcException e)
                {
                    foreach (OdbcException err in e.Errors)
                        sError += $"Error: {err.Message}";
                    //sError += $"Error: {e.Message}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }

                Command.Dispose();
                Command = null;
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
                ix = sMensajes.ToString().IndexOf(ToFind);
                if (ix != -1)
                {
                    ix += ToFind.Length;
                    string sAux = sMensajes.ToString().Substring(ix, 12).TrimStart();
                    ix = sAux.IndexOf(" ") != -1 ? sAux.IndexOf(" ") : sAux.IndexOf("\n");
                    sAux = sAux.Substring(0, ix);
                    IndexId = Convert.ToInt32( sAux);
                }

                ix = 0;
                ToFind = "Metadata: ObjectId =";
                ix = sMensajes.ToString().IndexOf(ToFind);
                if (ix != -1)
                {
                    ix += ToFind.Length;
                    string sAux = sMensajes.ToString().Substring(ix, 12).TrimStart();
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

                Command = Conn.CreateCommand();
                Command.CommandText = sComando;
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandTimeout = 0;

                try
                {
                    raf = Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    sError = $"Error: {e.Message}";
                    Command.Dispose();
                    Command = null;
                    return -1;
                }

                Command = null;
                return raf;
            }


            public bool ExecCmdDataWithParam(string sComando, Dictionary<string, string> Params)
            {
                DataClose();
                ErrorClear();
                ClearMessages();

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

                Command = Conn.CreateCommand();
                Command.CommandText = sComando;
                Command.CommandType = System.Data.CommandType.Text;
                Command.CommandTimeout = 0;

                try
                {
                    Data = Command.ExecuteReader();
                }
                catch (SqlException e)
                {
                    sError = $"Error: {e.Message}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }

                Command.Dispose();
                Command = null;
                return true;
            }


            /// <summary>
            /// Ejecuta una Sentencia SQL y retorna el ResultSet asociado.
            /// </summary>
            /// <param name="sCommand"></param>
            /// <returns></returns>
            public bool ExecStoredCmdData(String sCommandFile, Dictionary<string, string> Params)
            {

                string sComando = DecryptFiletoString( $"{sPathToCommands}{sCommandFile}");

                return ExecCmdDataWithParam(sComando, Params);
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

                    byte[] buffer = Encoding.UTF8.GetBytes(strInputString);

                    crStream.Write(buffer, 0, buffer.Length);

                    crStream.Close();
                }
            }



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
            }


            public static string EncryptStringToString(string Message)
            {
                if (Message == "" || Message == null)
                    return "";

                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below

                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passKey));

                // Step 2. Create a new TripleDESCryptoServiceProvider object
                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                // Step 3. Setup the encoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                byte[] DataToEncrypt = UTF8.GetBytes(Message);

                // Step 5. Attempt to encrypt the string
                try
                {
                    ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }

                // Step 6. Return the encrypted string as a base64 encoded string
                return Convert.ToBase64String(Results);
            }


            public static string DecryptStringToString(string Message)
            {
                if (Message == "" || Message == null)
                    return "";

                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below

                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passKey));

                // Step 2. Create a new TripleDESCryptoServiceProvider object
                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                // Step 3. Setup the decoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                byte[] DataToDecrypt = Convert.FromBase64String(Message);

                // Step 5. Attempt to decrypt the string
                try
                {
                    ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }

                // Step 6. Return the decrypted string in UTF8 format
                return UTF8.GetString(Results);
            }

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


            public int GetCurrent_SPID()
            {
                int SPID = 0;
                string comando = "SELECT @@SPID";
                ExecuteSqlData(comando);
                if (Data != null)
                {
                    Data.Read();
                    SPID = Data.GetInt16(0);
                }
                DataClose();
                return SPID;
            }


            public void Kill_SPID(int SPID)
            {
                string comando = $"KILL {SPID};";
                ExecuteSql(comando);
            }


            public bool UseDatabase(string Database)
            {
                this.ErrorClear();
                string sComando = $"USE [{Database}]";
                ExecuteSql(sComando);
                if (this.ErrorExiste || GetCurrentDatabase().ToLower() != Database.ToLower() )
                {
                    sError += (sError!= "")? $"\n{sMensajes.ToString()}": sMensajes.ToString();
                    if (sError == "")
                        sError = $"No se ha podio acceder a la Base de Dato: [{Database}]";
                    return false;
                }
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


            /// <summary>
            /// Indexes of a Table
            /// Mos relevant columns are:
            ///    index_create_statement
            ///    index_name
            ///    user_seeks
            ///    user_scans
            ///    user_lookups
            ///    user_updates
            ///    partition_count
            ///    is_hypothetical
            ///    has_filter
            ///    is_unique
            ///    last_user_read
            ///    last_user_update
            /// </summary>
            /// <param name="table_name"></param>
            /// <returns></returns>
            public DataTable GetTableIndexes( string table_name)
            {
                string comando = $@"
                SELECT 
                    database_name = DB_NAME(),
                    table_name    = sc.name + N'.' + t.name,
                    last_user_read = ( SELECT MAX(user_reads) 
                                      FROM (VALUES (last_user_seek), (last_user_scan), (last_user_lookup)) AS value(user_reads)
                                    ),
                    last_user_update,
                    index_create_statement = CASE si.index_id 
                        WHEN 0 THEN N'/* No create statement (Heap) */'
                        ELSE 
                            CASE is_primary_key WHEN 1 THEN
                                N'ALTER TABLE ' + QUOTENAME(sc.name) + N'.' + QUOTENAME(t.name) + N' ADD CONSTRAINT ' + QUOTENAME(si.name) + N' PRIMARY KEY ' +
                                    CASE WHEN si.index_id > 1 THEN N'NON' ELSE N'' END + N'CLUSTERED '
                                ELSE N'CREATE ' + 
                                    CASE WHEN si.is_unique = 1 then N'UNIQUE ' ELSE N'' END +
                                    CASE WHEN si.index_id > 1 THEN N'NON' ELSE N'' END + N'CLUSTERED ' +
                                    N'INDEX ' + QUOTENAME(si.name) + N' ON ' + QUOTENAME(sc.name) + N'.' + QUOTENAME(t.name) + N' '
                            END +
                         N'(' + key_definition + N')' +
                            CASE 
                                WHEN include_definition IS NOT NULL THEN N' INCLUDE (' + include_definition + N')'
                                ELSE N''
                            END +
                    
                            CASE 
                                WHEN filter_definition IS NOT NULL THEN N' WHERE ' + filter_definition
                                ELSE N''
                            END +
                        /* with clause - compression goes here */
                        CASE 
                            WHEN row_compression_partition_list IS NOT NULL OR page_compression_partition_list IS NOT NULL THEN N' WITH (' +
                                CASE 
                                    WHEN row_compression_partition_list IS NOT NULL THEN N'DATA_COMPRESSION = ROW ' + 
                                         CASE 
                                            WHEN psc.name IS NULL THEN N''
                                            ELSE + N' ON PARTITIONS (' + row_compression_partition_list + N')'
                                         END
                                    ELSE N''
                                END +
                                CASE WHEN row_compression_partition_list IS NOT NULL AND page_compression_partition_list IS NOT NULL THEN N', ' ELSE N'' END +
                                CASE WHEN page_compression_partition_list IS NOT NULL THEN
                                    N'DATA_COMPRESSION = PAGE ' + CASE 
                                                                     WHEN psc.name IS NULL THEN N'' 
                                                                     ELSE + N' ON PARTITIONS (' + page_compression_partition_list + N')'
                                                                  END
                                ELSE N''
                            END
                            + N')'
                            ELSE N''
                        END +
                        /* ON where? filegroup? partition scheme? */
                        ' ON ' + CASE WHEN psc.name is null 
                            THEN ISNULL(QUOTENAME(fg.name),N'')
                            ELSE psc.name + N' (' + partitioning_column.column_name + N')' 
                            END
                        + N';'
                    END,

                    si.index_id,
                    si.name AS index_name,
                    partition_sums.reserved_in_row_GB,
                    partition_sums.reserved_LOB_GB,
                    partition_sums.row_count,
                    stat.user_seeks,
                    stat.user_scans,
                    stat.user_lookups,
                    user_updates,
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

                ExecuteSqlData(comando);
                if (ErrorExiste || Messages != "")
                {
                    MessageBox.Show($"SQL Error finding Indexes {ErrorString}\n{Messages}");
                    ErrorClear();
                    return new DataTable();
                }

                DataTable dt = new DataTable();
                dt.Load(this.Data);
                return dt;
            }



            public string GetCurrentDatabase()
            {
                if (!ConnectionStatus)
                    return "";

                string Comando = "SELECT db_name = DB_NAME()";
                string db_name = "";
                ExecuteSqlData(Comando);
                try
                {
                    if (Data != null && Data.HasRows)
                    {

                        Data.Read();
                        db_name = Data.GetString(0);
                    }
                    DataClose();
                }
                catch { }
                return db_name;
            }


            public string GetServerName()
            {
                string Comando = "SELECT @@SERVERNAME";
                string server_name = "";
                ExecuteSqlData(Comando);
                if (Data != null)
                {
                    Data.Read();
                    server_name = Data.GetString(0);
                }
                DataClose();
                return server_name;
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
                    Command = new SqlCommand(ComandoSQL);
                    SqlParameterCollection parameters = Command.Parameters;
                    parameters.Add(FieldName, SqlDbType.Image);
                    parameters[FieldName].Value = GetPhoto(File);
                    Command.Connection = Conn;
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    this.sError = $"Error grabando BLOB\n{e.ErrorCode}-{e.Message}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }

                Command.Dispose();
                Command = null;
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
                    Command = new SqlCommand(ComandoSQL);
                    SqlParameterCollection parameters = Command.Parameters;
                    parameters.Add(FieldName, SqlDbType.Image);
                    parameters[FieldName].Value = Image;
                    Command.Connection = Conn;
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    this.sError = $"Error grabando BLOB\n{e.ErrorCode}-{e.Message}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }

                Command.Dispose();
                Command = null;
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
                    Command = new SqlCommand(ComandoSQL);
                    SqlParameterCollection parameters = Command.Parameters;
                    Command.Connection = Conn;
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    this.sError = $"Error grabando BLOB\n{e.ErrorCode}-{e.Message}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }

                Command.Dispose();
                Command = null;
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
                Command = new SqlCommand(query);
                Command.Connection = Conn;

                try
                {
                    SqlDataReader reader = Command.ExecuteReader();

                    // check whether there is at least one record
                    if (reader.Read())
                    {
                        // matching record found, read first column as string instance
                        if (reader.GetValue(0) == System.DBNull.Value)
                        {
                            reader.Close();
                            reader.Dispose();
                            Command.Dispose();
                            Command = null;
                            return true;
                        }

                        byte[] value = (byte[])reader.GetValue(0);
                        if (SaveFile(File, value))
                        {
                            reader.Close();
                            reader.Dispose();
                            Command.Dispose();
                            Command = null;
                            return true;
                        }
                    }

                    reader.Close();
                    this.sError = $"Error grabando Archivo: \n{File}";
                    Command.Dispose();
                    Command = null;
                    return false;
                }
                catch (SqlException e)
                {
                    this.sError = $"Error Leyendo BLOB\n{e.ErrorCode}-{e.Message}";
                    Command.Dispose();
                    Command = null;
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
                Command = new SqlCommand(query);
                Command.Connection = Conn;

                try
                {
                    SqlDataReader reader = Command.ExecuteReader();

                    // check whether there is at least one record
                    if (reader.Read())
                    {
                        // matching record found, read first column as string instance
                        byte[] value = (byte[])reader.GetValue(0);
                        reader.Close();
                        Command.Dispose();
                        Command = null;
                        return value;
                    }
                    reader.Close();
                    Command.Dispose();
                    Command = null;
                    return null;
                }
                catch (SqlException e)
                {
                    this.sError = $"Error Leyendo BLOB\n{e.ErrorCode}-{e.Message}";
                    Command.Dispose();
                    Command = null;
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


            public static List<string> ParseSqlCommandGO(string sql)
            {
                List<string> list = new List<string>();
                string[] result = System.Text.RegularExpressions.Regex.Split(sql, @"(^[ ]*GO[ ]*(\r\n|$))", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);


                for (int i = 0; i < result.Length; ++i)
                {
                    if (result[i].Trim() == "" || result[i].Trim() == "\\r\\n" || System.Text.RegularExpressions.Regex.IsMatch(result[i], @"(^[ ]*GO[ ]*)|(^[ ]*GO[ ]*--[\w\W.,\d\D ])", RegexOptions.IgnoreCase))
                        continue;
                    list.Add(result[i]);
                }

                return list;
            }

        } //MyFuncs

    }
}
