# Digipolis Swagger library

Digipolis Antwerp uses the [Swagger](https://swagger.io) library for API documentation.  
Our library adds a Swagger startup extension that, by default, adds custom Digipolis operator filters that follow the [Digipolis API guidelines](https://acpaas-api.digipolis.be#/).

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Target framework](#target-framework)
- [Installation](#installation)
- [Usage](#usage)
- [Custom options](#custom-options)
- [Contributing](#contributing)
- [Support](#support)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Target framework

This package targets **.NET Standard 2.1**.

## Installation

To add the library to a project, you add the package to the csproj file :

```xml
  <ItemGroup>
    <PackageReference Include="Digipolis.swagger" Version="1.0.0" />
  </ItemGroup>
```

In Visual Studio you can also use the NuGet Package Manager to do this.

## Usage

This library serves as the Digipolis Swagger extensions library. It contains the service collection extension method to register the 
Digipolis Swagger options, to be called in the **ConfigureServices** method of the **Startup** class.

```csharp  
services.AddDigipolisSwagger(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Your API title",
            Description = "Your API description",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Email = "your.email@digipolis.be",
                Name = "Your name"
            },
            License = new OpenApiLicense
            {
                Name = "None",
                Url = null,
            },
        });
    });
```

This method adds basic operator filters that abide the rules set by the [Digipolis API guidelines](https://acpaas-api.digipolis.be#/).
Make sure to fill in the SwaggerDoc option as this one is not automatically set.

## Custom options
This library extends the original SwaggerGenOptions with additional default properties. If you wish to prevent certain default options to be added simply set these options to false, or if you do want them to be added simple set them to true (if they are not yet true by default).

```csharp  
services.AddDigipolisSwagger(options =>
    {
        options.DefaultAddAuthorizationHeaderRequired = false;
    });
```

Here is a complete overview of the additional options to be set:

| Option                                | Description                                                  | Default |
| ------------------------------------- | ------------------------------------------------------------ | ------- |
| DefaultComments                       | When set to true the xml documents will be added.            | true    |
| DefaultSecurityDefinition             | When set to true and when the SwaggerGeneratorOptions.SecuritySchemes have no elements with key 'Bearer' then the default JWT authoriztion header security scheme is added. | true    |
| DefaultSchemaIdSelector               | When set to true the SchemaGeneratorOptions.SchemaIdSelector will be set to the SchemaIdSelector when this option is left null. | true    |
| DefaultAddAuthorizationHeaderRequired | When set to true the AddAuthorizationHeaderRequired class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultRemoveSyncRootParameter        | When set to true the RemoveSyncRootParameter class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultLowerCaseQueryParameterFilter  | When set to true the LowerCaseQueryParameterFilter class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultCamelCaseBodyParameterFilter   | When set to true the CamelCaseBodyParameterFilter class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultAddDefaultValues               | When set to true the AddDefaultValues class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultRemoveVersionFromRoute         | When set to true the RemoveVersionFromRoute class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultAddPagingParameterDescriptions | When set to true the AddPagingParameterDescriptions class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |
| DefaultSetDescription                 | When set to true the SetDescription class will be added to the OperationFilterDescriptors list by default if not yet included. | true    |


## Contributing

Pull requests are always welcome, however keep the following things in mind:

- New features (both breaking and non-breaking) should always be discussed with the [repo's owner](#support). If possible, please open an issue first to discuss what you would like to change.
- Fork this repo and issue your fix or new feature via a pull request.
- Please make sure to update tests as appropriate. Also check possible linting errors and update the CHANGELOG if applicable.

## Support

Paul Hieltjes (<paul.hieltjes@digipolis.be>)
