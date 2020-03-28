namespace VendorMediaTypes.Swashbuckle
{
    using System;
    using global::Swashbuckle.AspNetCore.SwaggerGen;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;

    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IServiceProvider _serviceProvider;

        public ConfigureSwaggerGenOptions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Configure(SwaggerGenOptions options)
        {
            var collection = _serviceProvider.GetRequiredService<VendorMediaTypeCollection>();

            options.OperationFilter<VendorMediaTypesOperationFilter>(collection);
            options.MapType<VendorMediaTypeRequest>(() => new OpenApiSchema
                {Type = "object", AdditionalProperties = new OpenApiSchema {Type = "object"}});
        }
    }
}