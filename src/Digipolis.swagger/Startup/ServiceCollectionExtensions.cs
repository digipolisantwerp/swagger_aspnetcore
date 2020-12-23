using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Digipolis.swagger.Options;
using Digipolis.swagger.Swagger;
using Digipolis.swagger.Swagger.OperationFilter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.swagger.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDigipolisSwagger(this IServiceCollection services, Action<SwaggerOptions> setupAction = null)
        {
            var options = new SwaggerOptions();

            setupAction?.Invoke(options);

            AddDefaultOperationFilters(options);
            AddDefaultSchemaGeneratorOptions(options);
            AddDefaultSecurityDefinition(options);

            Action<SwaggerGenOptions> newOptions = genOptions =>
            {
                genOptions.SwaggerGeneratorOptions = options.SwaggerGeneratorOptions;
                genOptions.SchemaGeneratorOptions = options.SchemaGeneratorOptions;
                genOptions.ParameterFilterDescriptors = options.ParameterFilterDescriptors;
                genOptions.RequestBodyFilterDescriptors = options.RequestBodyFilterDescriptors;
                genOptions.OperationFilterDescriptors = options.OperationFilterDescriptors;
                genOptions.DocumentFilterDescriptors = options.DocumentFilterDescriptors;
                genOptions.SchemaFilterDescriptors = options.SchemaFilterDescriptors;
            };

            services.AddSwaggerGen(newOptions);

            return services;
        }

        private static void AddDefaultSecurityDefinition(SwaggerOptions options)
        {
            if (options.DefaultSecurityDefinition
                && options.SwaggerGeneratorOptions.SecuritySchemes.All(s => s.Key != "Bearer"))
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });
            }
        }
        
        private static void AddDefaultSchemaGeneratorOptions(SwaggerOptions options)
        {
            if (options.DefaultSchemaIdSelector
                && options.SchemaGeneratorOptions.SchemaIdSelector.Method.Name == "DefaultSchemaIdSelector")
            {
                options.CustomSchemaIds(type => SchemaIdGenerator.SchemaIdSelector(type));
            }
        }
        
        private static void AddDefaultOperationFilters(SwaggerOptions options)
        {
            if (options.DefaultAddAuthorizationHeaderRequired
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(AddAuthorizationHeaderRequired)))
            {
                options.OperationFilter<AddAuthorizationHeaderRequired>();
            }
            
            if (options.DefaultRemoveSyncRootParameter
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(RemoveSyncRootParameter)))
            {
                options.OperationFilter<RemoveSyncRootParameter>();
            }
            
            if (options.DefaultLowerCaseQueryParameterFilter
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(LowerCaseQueryParameterFilter)))
            {
                options.OperationFilter<LowerCaseQueryParameterFilter>();
            }
            
            if (options.DefaultCamelCaseBodyParameterFilter
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(CamelCaseBodyParameterFilter)))
            {
                options.OperationFilter<CamelCaseBodyParameterFilter>();
            }
            
            if (options.DefaultAddDefaultValues
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(AddDefaultValues)))
            {
                options.OperationFilter<AddDefaultValues>();
            }
            
            if (options.DefaultRemoveVersionFromRoute
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(RemoveVersionFromRoute)))
            {
                options.OperationFilter<RemoveVersionFromRoute>();
            }
            
            if (options.DefaultAddPagingParameterDescriptions
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(AddPagingParameterDescriptions)))
            {
                options.OperationFilter<AddPagingParameterDescriptions>();
            }
            
            if (options.DefaultAddCorrelationHeaderRequired
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(AddCorrelationHeaderRequired)))
            {
                options.OperationFilter<AddCorrelationHeaderRequired>();
            }

            if (options.DefaultComments)
            {
                var location = Assembly.GetEntryAssembly()?.Location;
                var xmlComments = Path.Combine(Environment.CurrentDirectory, Path.GetFileNameWithoutExtension(location) + ".xml");

                if (File.Exists(xmlComments))
                {
                    options.IncludeXmlComments(xmlComments);
                }
            }
            
            // Need to be added after IncludeXmlComments or JSON will not show descriptions
            if (options.DefaultSetDescription
                && options.OperationFilterDescriptors.All(o => o.Type != typeof(SetDescription)))
            {
                options.OperationFilter<SetDescription>();
            }
            
        }
    }
}