using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.Swagger.Swagger.DocumentFilter
{
    public class SetVersionInPaths : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // TODO: does not seem to work, already broken in toolbox

            foreach (var path in swaggerDoc.Paths)
            {
                //path.Key = path.Key.Replace("{apiversion}", swaggerDoc.Info.Version, StringComparison.CurrentCultureIgnoreCase);
                var pathItem = path.Value;

                foreach(var operation in pathItem.Operations ?? new Dictionary<OperationType, OpenApiOperation>())
                {
                    switch(operation.Key)
                    {
                        case OperationType.Get:
                        case OperationType.Put:
                        case OperationType.Post:
                        case OperationType.Delete:
                        case OperationType.Options:
                        case OperationType.Head:
                        case OperationType.Patch:
                            RemoveVersionParamFrom(operation.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void RemoveVersionParamFrom(OpenApiOperation operation)
        {
            if (operation == null || operation.Parameters == null) return;

            var versionParam = operation.Parameters.FirstOrDefault(param => param.Name.Equals("apiVersion", StringComparison.CurrentCultureIgnoreCase));
            if (versionParam == null) return;

            operation.Parameters.Remove(versionParam);
        }
    }
}
