using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.Swagger.Swagger.OperationFilter
{
    public class RemoveVersionFromRoute : IOperationFilter
    {
        private readonly IHostEnvironment _environment;

        public RemoveVersionFromRoute(IServiceProvider serviceProvider)
        {
            _environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        }
        
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (_environment.IsDevelopment()) return;
            
            var regex = new Regex(@"^(v)\d*[\/]");

            var match = regex.Match(context.ApiDescription.RelativePath);
            if (match.Success)
                context.ApiDescription.RelativePath = context.ApiDescription.RelativePath.Replace(match.Value, string.Empty);
        }
    }
}
