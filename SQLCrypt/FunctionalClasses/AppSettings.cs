using System;
using System.IO;
using System.Reflection;

namespace SQLCrypt.FunctionalClasses
{
    /// <summary>
    /// Constantes y configuraciones centralizadas de la aplicación
    /// </summary>
    internal static class AppSettings
    {
        // Configuración de archivos
        public const string ConfigFileName = "config.json";
        public static readonly string ConfigFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            ConfigFileName
        );

        // Configuración de ventana
        public const int WindowMinWidth = 1173;
        public const int WindowMinHeight = 664;

        // Configuración de archivos recientes
        public const int MaxRecentFiles = 15;

        // Configuración de base de datos
        public const int DefaultCommandTimeout = 0; // Sin timeout
        public const int DefaultTopRows = 1000;

        // Mensajes de error comunes
        public const string ErrorConnectionString = "Error desconocido o de formato del String de conexión.";
        public const string ErrorEmptyCommand = "Error: Comando vacío";
        public const string ErrorDatabaseConnection = "Please check the Database Connection";
    }
}
