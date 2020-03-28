namespace VendorMediaTypes.DependencyInjection
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class ConfigureVendorMediaTypesMvcOptions : IConfigureOptions<MvcOptions>
    {
        private readonly IServiceProvider _serviceProvider;

        public ConfigureVendorMediaTypesMvcOptions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Configure(MvcOptions options)
        {
            var collection = _serviceProvider.GetRequiredService<VendorMediaTypeCollection>();
            options.Conventions.Add(new ConsumesConstraintForVendorMediaTypesConvention(collection));
            options.ModelBinderProviders.Insert(0, new VendorMediaTypeRequestModelBinderProvider());
        }
    }
}