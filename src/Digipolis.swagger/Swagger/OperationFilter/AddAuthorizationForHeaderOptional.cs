using System.Linq;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Swagger.OperationFilter
{
    public class AddAuthorizationForHeaderOptional : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(AuthorizeFilter))
                && !context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(AllowAnonymousFilter)))
            {
                operation.Parameters?.Add(new OpenApiParameter()
                {
                    Name = "Dgp-Authorization-For",
                    Description = "JWT token",
                    Required = false,
                    In = ParameterLocation.Header
                });
            }
        }
    }
}
