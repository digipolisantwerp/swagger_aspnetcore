using System;

namespace Digipolis.Swagger.Swagger.Attribute
{
    //use this attribute to prevent duplicate schema id's in swagger definition for classes with identical names in different namespaces
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class SwashbuckleCustomSchemaIdAttribute : System.Attribute
    {
        public SwashbuckleCustomSchemaIdAttribute(string schemaId)
        {
            SchemaId = schemaId;
        }
        public string SchemaId { get; set; }
    }
}
