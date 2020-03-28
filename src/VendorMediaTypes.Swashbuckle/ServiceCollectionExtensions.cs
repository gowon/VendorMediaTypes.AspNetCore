namespace VendorMediaTypes.Swashbuckle
{
    using DependencyInjection;
    using global::Swashbuckle.AspNetCore.SwaggerGen;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        public static IVendorMediaTypesBuilder AddSwaggerGenSupport(this IVendorMediaTypesBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>());
            return builder;
        }
    }
}