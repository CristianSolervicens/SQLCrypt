using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SQLCrypt.StructureClasses
{


    /// <summary>
    /// Lista de Tipos de Objetos soportados en SQLServer
    /// </summary>
    public class ObjectTypes : List<ObjectType>
    {
        public ObjectTypes()
        {
            this.Add("U", "USER_TABLE");
            this.Add("P", "SQL_STORED_PROCEDURE");
            this.Add("FN", "SQL_SCALAR_FUNCTION");
            this.Add("TF", "SQL_TABLE_VALUED_FUNCTION");
            this.Add("TR", "SQL_TRIGGER");
            this.Add("V", "VIEW");

            this.Add( "D" , "DEFAULT_CONSTRAINT");
            this.Add( "F" , "FOREIGN_KEY_CONSTRAINT");
            this.Add("PK", "PRIMARY_KEY_CONSTRAINT");
            this.Add("UQ", "UNIQUE_CONSTRAINT");
            
            this.Add("SN", "SYNONYM");

            this.Add("S", "SYSTEM_TABLE");
            // this.Add( "IT", "INTERNAL_TABLE");
            this.Add( "SQ", "SERVICE_QUEUE");
            this.Add("SO", "SEQUENCE_OBJECT");
            
        }

        private void Add( string type, string name)
        {
            ObjectType ObjT = new ObjectType();
            ObjT.name = name;
            ObjT.type = type;

            this.Add( ObjT);
        }
    }



    /// <summary>
    /// Tipos de Objetos soportados por SQLServer
    /// </summary>
    public class ObjectType
    {
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

        public override string ToString()
        {
            return this.name;
        }

    }

}
