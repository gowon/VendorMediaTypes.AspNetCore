namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class StartupExtensions
    {
        public static IServiceCollection AddVendorMediaTypesSupport(this IServiceCollection services,
            Assembly assembly)
        {
            return services.AddVendorMediaTypesSupport(collection =>
            {
                var list = assembly.GetTypes()
                    .Where(type => Attribute.IsDefined(type, typeof(MediaTypeAttribute)))
                    .Select(type => new
                    {
                        Type = type,
                        Attributes = type.GetCustomAttributes<MediaTypeAttribute>()
                            .SelectMany(attribute => attribute.Types).ToArray()
                    })
                    .ToList();

                foreach (var item in list)
                {
                    collection.Add(item.Type, item.Attributes);
                }
            });
        }

        public static IServiceCollection AddVendorMediaTypesSupport(this IServiceCollection services,
            Action<VendorMediaTypeCollection> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Setup media types and add them to the DI
            var collection = new VendorMediaTypeCollection();
            setupAction.Invoke(collection);
            services.TryAddSingleton(collection);
            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureVendorMediaTypesMvcOptions>();
            return services;
        }
    }
}