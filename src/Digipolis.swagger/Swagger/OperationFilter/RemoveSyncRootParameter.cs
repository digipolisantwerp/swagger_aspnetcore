using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Swagger.OperationFilter
{
    public class RemoveSyncRootParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters?.Remove(operation.Parameters?.Where(x => x.Name == "SyncRoot").FirstOrDefault());
        }
    }
}
