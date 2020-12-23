using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Swagger.OperationFilter
{
    public class RemoveVersionFromRoute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var regex = new Regex(@"^(v)\d*[\/]");
            var match = regex.Match(context.ApiDescription.RelativePath);
            if (match.Success)
                context.ApiDescription.RelativePath = context.ApiDescription.RelativePath.Replace(match.Value, string.Empty);
        }
    }
}
