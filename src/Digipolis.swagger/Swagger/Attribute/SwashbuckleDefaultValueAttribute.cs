using System;

namespace Digipolis.swagger.Swagger.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SwashbuckleDefaultValueAttribute : System.Attribute
    {
        public string Parameter { get; set; }
        public string Value { get; set; }
    }
}
