namespace VendorMediaTypes.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddVendorMediaTypesSupport(this IMvcBuilder builder,
            Action<VendorMediaTypeCollection> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Setup media types and add them to the DI
            var collection = VendorMediaTypeCollection.GetInstance();
            setupAction.Invoke(collection);
            builder.Services.TryAddSingleton(collection);

            // Add the model binder provider to MVC
            builder.AddMvcOptions(options =>
                options.ModelBinderProviders.Insert(0, new VendorMediaTypeRequestModelBinderProvider()));

            return builder;
        }
    }
}