namespace VendorMediaTypes.DependencyInjection
{
    using System;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        public static IVendorMediaTypesBuilder AddVendorMediaTypes(this IServiceCollection services,
            Action<VendorMediaTypeCollection> setupAction = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Setup media types and add them to the DI
            var collection = new VendorMediaTypeCollection();
            if (setupAction == null)
            {
                collection.AddAssembly(Assembly.GetCallingAssembly());
            }
            else
            {
                setupAction.Invoke(collection);
            }

            services.TryAddSingleton(collection);
            services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IConfigureOptions<MvcOptions>, ConfigureVendorMediaTypesMvcOptions>());
            return new VendorMediaTypesBuilder(services);
        }
    }
}