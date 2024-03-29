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

        public static IApplicationBuilder UseDigipolisSwagger(this IApplicationBuilder app, IEnumerable<string> versions = null, bool useDownloadButton = true)
        {
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/{swaggerDoc.Info.Version}" } };
                });
                options.SerializeAsV2 = true;
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

                if (!useDownloadButton) return;
                var outputDir = Path.Combine(AppContext.BaseDirectory, _dir);
                CreateResource(outputDir);
                app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(outputDir),
                    RequestPath = $"/{_dir}",
                    EnableDirectoryBrowsing = false
                });

                options.InjectJavascript($"/{_dir}/{_fileName}");


            });

            return app;
        }

        private static readonly object resourceLock = new object();
        private static void CreateResource(string outputDir)
        {
            // Get the name of the toolbox assembly
            var assembly = Assembly.GetAssembly(typeof(ApplicationBuilderExtensions));
            var asn = assembly.GetName().Name;
            // construct the name of the embedded resource
            var resource = string.Join('.', asn, _dir, _fileName);
            // Read the embedded resource as a stream
            using var stream = assembly.GetManifestResourceStream(resource);
            if (stream == null) return;
            
            lock (resourceLock) 
            {
                // Ensure the directory exists
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
                // Create/open the file for writing
                using (var file = File.Create(Path.Combine(outputDir, _fileName)))
                {
                    // Copy the contents of the embedded resource into the file
                    stream.CopyToAsync(file);
                }
            }
        }
    }
}