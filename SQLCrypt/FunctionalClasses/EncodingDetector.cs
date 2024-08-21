using System;
using System.IO;
using System.Text;
using Ude;


namespace SQLCrypt.FunctionalClasses
{
    

    public class EncodingDetector
    {
        // Method to detect the encoding using Ude
        public static Encoding DetectEncoding(string filePath)
        {
            // First, try to detect encoding with Ude (Universal Charset Detector)
            using (FileStream fs = File.OpenRead(filePath))
            {
                CharsetDetector cdet = new CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();

                if (cdet.Charset != null)
                {
                    Console.WriteLine($"Ude detected: {cdet.Charset}");
                    return Encoding.GetEncoding(cdet.Charset);
                }
            }

            // If Ude fails or returns null, try common .NET encoding detection
            return DetectEncodingWithFallback(filePath);
        }

        // Fallback method using BOM (Byte Order Mark) detection
        private static Encoding DetectEncodingWithFallback(string filePath)
        {
            using (var reader = new StreamReader(filePath, true))
            {
                reader.Peek(); // This forces StreamReader to detect the encoding
                Console.WriteLine($"Fallback detected: {reader.CurrentEncoding.EncodingName}");
                return reader.CurrentEncoding;
            }
        }
    }

}
