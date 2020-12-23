using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Swagger.OperationFilter
{
    public class LowerCaseQueryParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters
                .Where(x => x.In.HasValue 
                            && !string.IsNullOrWhiteSpace(x.In.ToString()) 
                            && x.In.ToString().Equals("Query")))
            {
                parameter.Name = parameter.Name.ToLowerInvariant();
            }
        }
    }
}
