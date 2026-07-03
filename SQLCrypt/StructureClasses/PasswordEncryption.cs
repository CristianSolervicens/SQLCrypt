using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SQLCrypt
{
    /// <summary>
    /// Provides portable password encryption/decryption using AES-256
    /// The encrypted file can be used on any computer
    /// </summary>
    public static class PasswordEncryption
    {
        // AES key and IV - Change these to your own values for security
        // These are used for portable encryption across different machines
        private static readonly byte[] Key = new byte[32] 
        { 
            0x51, 0x71, 0x43, 0x72, 0x79, 0x70, 0x74, 0x32, 
            0x30, 0x32, 0x34, 0x4B, 0x65, 0x79, 0x53, 0x65,
            0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x50, 0x61,
            0x73, 0x73, 0x77, 0x6F, 0x72, 0x64, 0x33, 0x32
        };

        private static readonly byte[] IV = new byte[16] 
        { 
            0x49, 0x56, 0x53, 0x51, 0x4C, 0x43, 0x72, 0x79,
            0x70, 0x74, 0x31, 0x36, 0x42, 0x79, 0x74, 0x65
        };

        /// <summary>
        /// Encrypts a password using AES-256
        /// </summary>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Encryption error: {ex.Message}");
                return plainText; // Fallback to plain text if encryption fails
            }
        }

        /// <summary>
        /// Decrypts a password using AES-256
        /// Automatically detects if password is encrypted or plain text
        /// </summary>
        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return string.Empty;

            // Check if this looks like an encrypted password
            if (!IsLikelyEncrypted(encryptedText))
            {
                System.Diagnostics.Debug.WriteLine("Password appears to be plain text, will be encrypted on next save.");
                return encryptedText; // Plain text password
            }

            try
            {
                byte[] buffer = Convert.FromBase64String(encryptedText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(buffer))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                string decrypted = srDecrypt.ReadToEnd();
                                System.Diagnostics.Debug.WriteLine("Password successfully decrypted.");
                                return decrypted;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If decryption fails, treat as plain text
                System.Diagnostics.Debug.WriteLine($"Decryption failed, treating as plain text: {ex.Message}");
                return encryptedText;
            }
        }

        /// <summary>
        /// Checks if a string is likely encrypted (more robust than just base64 check)
        /// </summary>
        private static bool IsLikelyEncrypted(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            // Encrypted passwords are typically longer and base64 encoded
            // Plain text passwords are usually shorter and contain normal characters
            if (text.Length < 16)
                return false; // Too short to be AES encrypted

            // Check if it's valid base64
            try
            {
                byte[] data = Convert.FromBase64String(text);

                // AES block size is 16 bytes, encrypted data should be a multiple of this
                if (data.Length % 16 != 0)
                    return false;

                // Additional check: encrypted base64 shouldn't have spaces or special chars
                // that wouldn't appear in base64
                if (text.Contains(" ") || text.Contains("\t") || text.Contains("\n"))
                    return false;

                return true;
            }
            catch
            {
                return false; // Not valid base64, so not encrypted
            }
        }

        /// <summary>
        /// Checks if a string is encrypted (base64 format)
        /// </summary>
        public static bool IsEncrypted(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            try
            {
                Convert.FromBase64String(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
