using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.Swagger.Options
{
    public class SwaggerOptions : SwaggerGenOptions
    {
        
        /// <summary>
        /// When set to true the xml documents will be added
        /// Default value: true
        /// </summary>
        public bool DefaultComments { get; set; } = true;

        /// <summary>
        /// When set to true and when the SwaggerGeneratorOptions.SecuritySchemes have no elements with key 'Bearer'
        /// then the default JWT authoriztion header security scheme is added
        /// Default value: true
        /// </summary>
        public bool DefaultSecurityDefinition { get; set; } = true;

        /// <summary>
        /// When set to true the SchemaGeneratorOptions.SchemaIdSelector will be set to the SchemaIdSelector
        /// when this option is not set with a different schemaId Selector function
        /// Default value: true
        /// </summary>
        public bool DefaultSchemaIdSelector { get; set; } = true;

        /// <summary>
        /// When set to true the AddAuthorizationHeaderRequired class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultAddAuthorizationHeaderRequired { get; set; } = true;
        
        /// <summary>
        /// When set to true the RemoveSyncRootParameter class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultRemoveSyncRootParameter { get; set; } = true;
        
        /// <summary>
        /// When set to true the LowerCaseQueryParameterFilter class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultLowerCaseQueryParameterFilter { get; set; } = true;
        
        /// <summary>
        /// When set to true the CamelCaseBodyParameterFilter class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultCamelCaseBodyParameterFilter { get; set; } = true;
        
        /// <summary>
        /// When set to true the AddDefaultValues class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultAddDefaultValues { get; set; } = true;
        
        /// <summary>
        /// When set to true the RemoveVersionFromRoute class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultRemoveVersionFromRoute { get; set; } = true;
        
        /// <summary>
        /// When set to true the AddPagingParameterDescriptions class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultAddPagingParameterDescriptions { get; set; } = true;
        
        /// <summary>
        /// When set to true the SetDescription class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultSetDescription { get; set; } = true;
        
        /// <summary>
        /// When set to true the AddCorrelationHeaderRequired class will be added to the
        /// OperationFilterDescriptors list by default if not yet included
        /// Default value: true
        /// </summary>
        public bool DefaultAddCorrelationHeaderRequired { get; set; } = true;

    }
}