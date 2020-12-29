using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.Swagger.Swagger.OperationFilter
{
    public class SetDescription : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (string.IsNullOrWhiteSpace(operation.Description))
                operation.Description = operation.Summary;

             else if (string.IsNullOrWhiteSpace(operation.Summary))
                operation.Summary = operation.Description;
        }
    }
}
