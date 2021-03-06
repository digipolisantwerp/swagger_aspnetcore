using System;
using Digipolis.Auth.Options;
using Digipolis.Swagger.Startup;
using Digipolis.Swagger.Swagger.OperationFilter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;
using System.Linq;

namespace Digipolis.Swagger.test
{
    public class ServiceCollectionExtensionsTest
    {

        [Fact]
        public void ShouldSetDefaultsOnAddSwagger()
        {
            var services = new ServiceCollection();

            services.AddDigipolisSwagger();

            
            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<SwaggerGenOptions>))
                .ToArray();

            Assert.Single(registrations);
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);

            var configOptions = registrations[0].ImplementationInstance as IConfigureOptions<SwaggerGenOptions>;
            Assert.NotNull(configOptions);

            var swaggerGenOptions = new SwaggerGenOptions();
            configOptions.Configure(swaggerGenOptions);

            Assert.Equal(8, swaggerGenOptions.OperationFilterDescriptors.Count);
            
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(AddAuthorizationHeaderRequired));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(RemoveSyncRootParameter));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(LowerCaseQueryParameterFilter));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(CamelCaseBodyParameterFilter));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(AddDefaultValues));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(AddPagingParameterDescriptions));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(AddCorrelationHeaderRequired));
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(SetDescription));
            
            Assert.Contains(swaggerGenOptions.SwaggerGeneratorOptions.SecuritySchemes, 
                s => s.Key == "Bearer");

            Assert.NotEqual("DefaultSchemaIdSelector", swaggerGenOptions.SchemaGeneratorOptions.SchemaIdSelector.Method.Name);
        }
        
        [Fact]
        public void ShouldSetMissingDefaultsOnAddSwagger()
        {
            var services = new ServiceCollection();

            services.AddDigipolisSwagger(options =>
            {
                options.OperationFilter<AddAuthorizationHeaderRequired>();
                options.OperationFilter<RemoveSyncRootParameter>();
                options.OperationFilter<LowerCaseQueryParameterFilter>();
                options.OperationFilter<AddDefaultValues>();
            });

            
            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<SwaggerGenOptions>))
                .ToArray();

            Assert.Single(registrations);
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);

            var configOptions = registrations[0].ImplementationInstance as IConfigureOptions<SwaggerGenOptions>;
            Assert.NotNull(configOptions);

            var swaggerGenOptions = new SwaggerGenOptions();
            configOptions.Configure(swaggerGenOptions);

            Assert.Equal(8, swaggerGenOptions.OperationFilterDescriptors.Count);
            
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(CamelCaseBodyParameterFilter));
            
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(AddPagingParameterDescriptions));
            
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(SetDescription));
            
            Assert.Contains(swaggerGenOptions.OperationFilterDescriptors,
                f => f.Type == typeof(AddCorrelationHeaderRequired));
        }
        
        [Fact]
        public void ShouldNotSetDefaultsWhenDefaultsDisabledOnAddSwagger()
        {
            var services = new ServiceCollection();

            services.AddDigipolisSwagger(options =>
            {
                options.DefaultAddAuthorizationHeaderRequired = false;
                options.DefaultRemoveSyncRootParameter = false;
                options.DefaultLowerCaseQueryParameterFilter = false;
                options.DefaultCamelCaseBodyParameterFilter = false;
                options.DefaultAddDefaultValues = false;
                options.DefaultAddPagingParameterDescriptions = false;
                options.DefaultSetDescription = false;
                options.DefaultSecurityDefinition = false;
                options.DefaultSchemaIdSelector = false;
                options.DefaultAddCorrelationHeaderRequired = false;
            });

            
            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<SwaggerGenOptions>))
                .ToArray();

            Assert.Single(registrations);
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);

            var configOptions = registrations[0].ImplementationInstance as IConfigureOptions<SwaggerGenOptions>;
            Assert.NotNull(configOptions);

            var swaggerGenOptions = new SwaggerGenOptions();
            configOptions.Configure(swaggerGenOptions);

            Assert.Empty(swaggerGenOptions.OperationFilterDescriptors);
            
            Assert.Empty(swaggerGenOptions.SwaggerGeneratorOptions.SecuritySchemes);
            
            Assert.Equal("DefaultSchemaIdSelector", swaggerGenOptions.SchemaGeneratorOptions.SchemaIdSelector.Method.Name);

        }
    }
}