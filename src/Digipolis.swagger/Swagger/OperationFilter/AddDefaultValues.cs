using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digipolis.Swagger.Swagger.Attribute;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Digipolis.Swagger.Swagger.OperationFilter
{
    public class AddDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var defaultValues = context.ApiDescription.ActionDescriptor.FilterDescriptors
                .Where(x => x.Filter.GetType() == typeof(SwashbuckleDefaultValueAttribute))
                .Cast<SwashbuckleDefaultValueAttribute>()
                .ToDictionary(x => x.Parameter.ToLowerInvariant(), x => x.Value);

            foreach (var param in operation.Parameters.Where(x => defaultValues.ContainsKey(x.Name.ToLowerInvariant())))
                param.Description = FormatDescription(param.Description, defaultValues[param.Name]);
            var notFounds = defaultValues.Where(x => !operation.Parameters.Select(y => y.Name.ToLowerInvariant()).Contains(x.Key)).Select(x => x.Key);
            if (notFounds.Any())
                throw new NullReferenceException(FormatError(notFounds, context.ApiDescription.ActionDescriptor.DisplayName));
        }

        private string FormatDescription(string description, string value)
        {
            var dvDescription = $"Default value: {value}";
            if (string.IsNullOrEmpty(description))
                return dvDescription;
            else
                return $"{description}<br />{dvDescription}";
        }

        private string FormatError(IEnumerable<string> notFounds, string action)
        {
            var description = new StringBuilder();
            description.Append(notFounds.Count() > 1 ? "Parameters " : "Parameter ");
            description.Append(string.Join(", ", notFounds));
            description.Append(notFounds.Count() > 1 ? " are" : " is");
            description.Append($" not found in action {action}");
            return description.ToString();
        }
    }
}