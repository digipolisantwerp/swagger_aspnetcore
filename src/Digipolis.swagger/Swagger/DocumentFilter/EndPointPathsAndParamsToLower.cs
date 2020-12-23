using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Swagger.DocumentFilter
{
    internal class EndPointPathsAndParamsToLower : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newPaths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                var res = HandlePath(path.Value);
                newPaths.Add(path.Key.ToLowerInvariant(), res);
            }
            swaggerDoc.Paths = newPaths;            
        }        

        private OpenApiPathItem HandlePath(OpenApiPathItem value)
        {
            value.Parameters = handleParameters(value.Parameters);

            foreach (var operation in value.Operations)
            {
                switch (operation.Key)
                {
                    case OperationType.Get:
                    case OperationType.Post:
                    case OperationType.Put:
                    case OperationType.Patch:
                    case OperationType.Delete:
                    case OperationType.Head:
                    case OperationType.Options:
                        if (operation.Value != null)
                            operation.Value.Parameters = handleParameters(operation.Value.Parameters);
                        break;
                }
            }

            return value;
        }

        private IList<OpenApiParameter> handleParameters(IList<OpenApiParameter> parameters)
        {
            if (parameters == null) return null;
            var searchList = new[] { "query", "path", "body" };
            foreach (var item in parameters.Where(x => x.In.HasValue && searchList.Contains(x.In.Value.ToString())))
            {
                item.Name = item.Name?.ToLowerInvariant();
            }
            return parameters;
        }
    }
}