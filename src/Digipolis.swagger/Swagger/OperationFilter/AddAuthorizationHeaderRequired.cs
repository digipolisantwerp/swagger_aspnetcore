using System;
using System.Collections.Generic;
using System.Linq;
using Digipolis.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Swagger.OperationFilter
{
    public class AddAuthorizationHeaderRequired : IOperationFilter
    {
        private readonly IHostEnvironment _environment;

        public AddAuthorizationHeaderRequired(IServiceProvider serviceProvider)
        {
            _environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        }
        
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(AuthorizeFilter))
                && context.ApiDescription.ActionDescriptor.FilterDescriptors.All(x => x.Filter.GetType() != typeof(AllowAnonymousFilter)))
            {
          
                operation.Parameters?.Add(new OpenApiParameter()
                {
                    Name = "Authorization",
                    Description = "JWT token",
                    Required = !_environment.IsDevelopment(),
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString("Bearer ")
                    }
                });
            }
        }
    }
}
