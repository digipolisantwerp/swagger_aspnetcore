using System;
using System.Linq;
using System.Reflection;
using Digipolis.Swagger.Swagger.Attribute;

namespace Digipolis.Swagger.Swagger
{
    public static class SchemaIdGenerator
    {
        public static String GenerateSchemaId(Type type)
        {
            if (type.GetTypeInfo().GetCustomAttributes(typeof(SwashbuckleCustomSchemaIdAttribute), true).FirstOrDefault() is SwashbuckleCustomSchemaIdAttribute swashbuckleCustomSchemaIdAttribute)
                return swashbuckleCustomSchemaIdAttribute.SchemaId;
            var arguments = type.GetGenericArguments();
            if (arguments.Any()) return $"{type.Name.Split('`').First()}[{String.Join(",", arguments.Select(z => z.Name))}]";
            return type.Name;
        }

        public static string SchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.Name;

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => SchemaIdSelector(genericArg))
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }
    }
}
