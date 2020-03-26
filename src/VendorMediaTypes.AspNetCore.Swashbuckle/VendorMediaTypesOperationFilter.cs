namespace VendorMediaTypes.AspNetCore.Swashbuckle
{
    using System;
    using System.Linq;
    using global::Swashbuckle.AspNetCore.SwaggerGen;
    using Microsoft.OpenApi.Models;

    public class VendorMediaTypesOperationFilter : IOperationFilter
    {
        private readonly VendorMediaTypeCollection _collection;

        public VendorMediaTypesOperationFilter(VendorMediaTypeCollection collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<ConsumesVendorTypeAttribute>().SelectMany(attribute => attribute.Types)
                .SelectMany(type => _collection[type]);

            if (attributes == null)
            {
                return;
            }

            var schema = new OpenApiSchema
            {
                Type = "object",
                AdditionalProperties = new OpenApiSchema {Type = "object"}
            };

            var mediaTypes = attributes.ToDictionary(
                contentType => contentType,
                contentType => new OpenApiMediaType
                {
                    Schema = schema,
                    Encoding = schema.Properties.ToDictionary(
                        entry => entry.Key,
                        entry => new OpenApiEncoding {Style = ParameterStyle.Form}
                    )
                });

            foreach (var mediaType in mediaTypes)
            {
                if (!operation.RequestBody.Content.ContainsKey(mediaType.Key))
                {
                    operation.RequestBody.Content.Add(mediaType.Key, mediaType.Value);
                }
            }
        }
    }
}