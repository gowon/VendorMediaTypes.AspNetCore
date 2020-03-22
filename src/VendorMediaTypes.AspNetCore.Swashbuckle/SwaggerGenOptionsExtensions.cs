namespace VendorMediaTypes.AspNetCore.Swashbuckle
{
    using global::Swashbuckle.AspNetCore.SwaggerGen;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerGenOptionsExtensions
    {
        public static void MaskVendorMediaTypeRequests(this SwaggerGenOptions options)
        {
            options.MapType<VendorMediaTypeRequest>(() => new OpenApiSchema
                {Type = "object", AdditionalProperties = new OpenApiSchema {Type = "object"}});
        }
    }
}