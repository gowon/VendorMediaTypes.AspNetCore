namespace VendorMediaTypes.DependencyInjection
{
    using System;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

    public class VendorMediaTypeRequestModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(VendorMediaTypeRequest))
            {
                return new BinderTypeModelBinder(typeof(VendorMediaTypeRequestModelBinder));
            }

            return null;
        }
    }
}