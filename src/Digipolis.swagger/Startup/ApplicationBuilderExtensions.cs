using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Digipolis.Swagger.Startup
{
    public static class ApplicationBuilderExtensions
    {
        private static string _fileName = "AddPublishJsonButton.js";
        private static string _dir = "CustomJs";

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
                CreateResource(_dir, _fileName);
                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(AppContext.BaseDirectory, _dir)),
                    RequestPath = $"/{_dir}",
                    EnableDirectoryBrowsing = false
                });

                options.InjectJavascript(Path.Combine(_dir, _fileName));
                
            });

            return app;
        }

        private static void CreateResource(string dir, string filename)
        {
            // Get the name of the toolbox assembly
            var assembly = Assembly.GetAssembly(typeof(ApplicationBuilderExtensions));
            var asn = assembly.GetName().Name;
            // construct the name of the embedded resource
            var resource = string.Join('.', asn, dir, filename);
            // Read the embedded resource as a stream
            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream == null) return; 
                // Ensure the directory exists
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                // Create/open the file for writing
                using (var file = File.Create(Path.Combine(dir, filename)))
                {
                    // Copy the contents of the embedded resource into the file
                    stream.CopyToAsync(file);
                }
            }
        }
    }
}