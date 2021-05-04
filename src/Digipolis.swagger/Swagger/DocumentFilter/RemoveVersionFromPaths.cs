using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Digipolis.Swagger.Swagger.DocumentFilter
{
    public class RemoveVersionFromPaths : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var version = swaggerDoc.Info.Version; 
            foreach (var (k, v) in swaggerDoc.Paths.ToList())
            {
                if (k.TrimStart('/').StartsWith(version, StringComparison.InvariantCultureIgnoreCase))
                {
                    swaggerDoc.Paths.Remove(k, out var value);
                    var key = k.Substring(k.IndexOf("/", 1));
                    swaggerDoc.Paths.Add(key, value);
                }
            }
        }
    }
}
