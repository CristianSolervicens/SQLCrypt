using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Crypto
{
    /// RatsEncryptionManager is responsible for encrypting and decrypting the files.
    public static class  Cryptus
    {
        private const string passKey = "AthELeIa";

        /// Decrypts the input file (strInputFileName) and creates a new decrypted file (strOutputFileName)
        /// <param name="strInputFileName">input file name</param>
        /// <param name="strOutputFileName">output file name</param>
       public static void DecryptFiletoFile(string strInputFileName, string strOutputFileName)
       {
            string strFileData = "";
            using (FileStream inputStream = new FileStream(strInputFileName, FileMode.Open, FileAccess.Read)){
                DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                cryptic.Key = ASCIIEncoding.ASCII.GetBytes(passKey);
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes(passKey);

                CryptoStream crStream = new CryptoStream(inputStream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(crStream);
                strFileData = reader.ReadToEnd();

                reader.Close();
                inputStream.Close();
            }

            if (File.Exists(strOutputFileName)){
                File.Delete(strOutputFileName);
            }
            using (StreamWriter outputStream = new StreamWriter(strOutputFileName)){
                outputStream.Write(strFileData, 0, strFileData.Length);
                outputStream.Close();
            }

        }//DecryptFiletoFile


        /// <summary>
        /// Encrypts the input file(strInputFileName) and creates a new encrypted file(strOutputFileName)
        /// </summary>
        /// <param name="strInputFileName">input file name</param>
        /// <param name="strOutputFileName">output file name</param>
       public static void EncryptFiletoFile(string strInputFileName, string strOutputFileName)
       {
            byte[] fileBuffer;

            using (FileStream inputStream = new FileStream(strInputFileName, FileMode.Open, FileAccess.Read)){
                fileBuffer = new byte[inputStream.Length];
                inputStream.Read(fileBuffer, 0, fileBuffer.GetLength(0));
                inputStream.Close();
            }
            if (File.Exists(strOutputFileName)){
                File.Delete(strOutputFileName);
            }
            using (FileStream outputStream = new FileStream(strOutputFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)){
                DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                cryptic.Key = ASCIIEncoding.ASCII.GetBytes(passKey);
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes(passKey);

                CryptoStream crStream = new CryptoStream(outputStream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
                crStream.Write(fileBuffer, 0, fileBuffer.Length);
                crStream.Close();
            }

        }//EncryptFiletoFile


        /// Encrypts the input string and creates a new encrypted file(strOutputFileName)
        /// <param name="strInputString">input string name</param>
        /// <param name="strOutputFileName">output file name</param>
       public static void EncryptStringtoFile(string strInputString, string strOutputFileName)
       {
            if (File.Exists(strOutputFileName)){
                File.Delete(strOutputFileName);
            }
            using (FileStream outputStream = new FileStream(strOutputFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)){
                DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                cryptic.Key = ASCIIEncoding.ASCII.GetBytes(passKey);
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes(passKey);

                CryptoStream crStream = new CryptoStream(outputStream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);

                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(strInputString);

                crStream.Write(buffer, 0, buffer.Length);

                crStream.Close();
            }
        }//EncryptStringtoFile


        /// Decrypts the input file (strInputFileName) and creates a new decrypted file (strOutputFileName)
        /// <param name="strInputFileName">input file name</param>
       public static string DecryptFiletoString(string strInputFileName)
       {
            string strFileData = "";
            using (FileStream inputStream = new FileStream(strInputFileName, FileMode.Open, FileAccess.Read)){
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


        //-----------------
        //base64Encode
        //-----------------
       public static string base64Encode(string data)
       {
            try{
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch(Exception e){
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }//base64Encode

        //------------------
        //base64Decode
        //------------------
       public static string base64Decode(string data)
       {
            try{
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch(Exception e){
                throw new Exception("Error in base64Decode" + e.Message);
            }
        } //base64Decode
    }
}
