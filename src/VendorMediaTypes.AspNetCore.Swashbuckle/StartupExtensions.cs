namespace VendorMediaTypes.AspNetCore.Swashbuckle
{
    using System;
    using global::Swashbuckle.AspNetCore.SwaggerGen;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class StartupExtensions
    {
        public static IServiceCollection AddVendorMediaTypesToSwaggerGen(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureVendorMediaTypesSwaggerGenOptions>();
            return services;
        }
    }
}