using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace Digipolis.Swagger.Startup
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDigipolisSwagger(this IApplicationBuilder app, IEnumerable<string> versions = null)
        {
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });

            app.UseSwaggerUI(options =>
            {
                if (versions != null)
                {
                    foreach (var version in versions)
                    {
                        options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version.ToUpperInvariant());
                        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    }
                }
                else
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                }
                
                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(AppContext.BaseDirectory, "CustomJs")),
                    RequestPath = "/CustomJs",
                    EnableDirectoryBrowsing = false
                });

                options.InjectJavascript("/CustomJs/AddPublishJsonButton.js");//"Digipolis.Swagger.CustomJs.AddPublishJsonButton.js");
                
            });

            return app;
        }
    }
}