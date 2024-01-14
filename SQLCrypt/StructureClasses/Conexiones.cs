using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace SQLCrypt
{

    /// <summary>
    /// Conexiones a la Base de Datos
    /// </summary>
    [Serializable]
    public class Conexion
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public Conexion()
        {

        }

        public Conexion(string Server, string Database, string User, string Password)
        {
            this.Server = Server;
            this.Database = Database;
            this.User = User;
            this.Password = Password;
        }

    }


    /// <summary>
    /// Lista de Conexiones :
    /// </summary>
    [Serializable]
    public class Conexiones:List<Conexion>
    {
        public int Add(string Server, string Database, string User, string Password)
        {
            var obj = this.FirstOrDefault(a => a.Server == Server);
            if (obj == null)
            {
                Conexion Obj = new Conexion(Server, Database, User, Password);
                this.Add(Obj);
                return this.Count - 1;
            }
            
            return -1;
            
        }

        public int findObjectInList( Conexion obj)
        {
            return this.FindIndex(a => a.Server == obj.Server);
        }


        public int LoadElementsFromFile(string MyFileName)
        {
            this.Clear();

            using (FileStream myFileStream = new FileStream(MyFileName, FileMode.Open))
            {
                if (myFileStream.Length == 0)
                {
                    myFileStream.Close();
                    return 0;
                }

                XmlSerializer mySerializer = new XmlSerializer(typeof(Conexiones));
                this.AddRange((Conexiones)mySerializer.Deserialize(myFileStream));
                myFileStream.Close();
            }

            return this.Count;

        }


        public int SaveElementstoFile(string MyFileName)
        {

            using (TextWriter writer = new StreamWriter(MyFileName))
            {
                XmlSerializer x = new XmlSerializer(typeof(Conexiones));
                x.Serialize(writer, this);
                writer.Close();
            }

            return this.Count;

        }


    }


    //---------------------

    /// <summary>
    /// Conexiones a la Base de Datos
    /// </summary>
    [Serializable]
    public class Conexion2
    {
        public string Descripcion { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public Conexion2()
        {

        }

        public Conexion2(string Descripcion, string Server, string Database, string User, string Password)
        {
            this.Descripcion = Descripcion;
            this.Server = Server;
            this.Database = Database;
            this.User = User;
            this.Password = Password;
        }

    }


    /// <summary>
    /// Lista de Conexiones2 :
    /// </summary>
    [Serializable]
    public class Conexiones2:List<Conexion2>
    {
        public int Add(string Descripcion, string Server, string Database, string User, string Password)
        {

            var obj = this.FirstOrDefault(a => a.Descripcion == Descripcion);
            if (obj != null)
            {
                return -1;
            }
            Conexion2 Obj = new Conexion2(Descripcion, Server, Database, User, Password);
            this.Add(Obj);
            return this.Count - 1;
        }

        public int findObjectInList(Conexion2 obj)
        {
            return this.FindIndex(a => a.Descripcion == obj.Descripcion);
        }

        public int findObjectIndexByName(string Description)
        {
            return this.FindIndex(a => a.Descripcion == Description);
        }


        public int LoadElementsFromFile(string MyFileName)
        {
            this.Clear();

            using (FileStream myFileStream = new FileStream(MyFileName, FileMode.Open))
            {
                if (myFileStream.Length == 0)
                {
                    myFileStream.Close();
                    return 0;
                }

                try
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(Conexiones2));
                    this.AddRange((Conexiones2)mySerializer.Deserialize(myFileStream));
                    myFileStream.Close();
                }
                catch
                {
                    myFileStream.Close();
                    Conexiones Paso = new Conexiones();
                    int x = Paso.LoadElementsFromFile(MyFileName);

                    foreach (var elem in Paso)
                    {
                        this.Add(elem.Server, elem.Server, elem.Database, elem.User, elem.Password);
                    }
                }
            }

            return this.Count;

        }


        public int SaveElementstoFile(string MyFileName)
        {

            using (TextWriter writer = new StreamWriter(MyFileName))
            {
                XmlSerializer x = new XmlSerializer(typeof(Conexiones2));
                x.Serialize(writer, this);
                writer.Close();
            }

            return this.Count;

        }


    }

}
