namespace VendorMediaTypes.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public class VendorMediaTypeRequestModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var collection = bindingContext.HttpContext.RequestServices.GetService<VendorMediaTypeCollection>();

            var contentType = bindingContext.HttpContext.Request.ContentType;

            if (!collection.TryGetType(contentType, out var match))
            {
                return;
            }

            Dictionary<string, object> properties;

            // check query string for methods that don't have response body
            if (bindingContext.HttpContext.Request.Method == HttpMethod.Get.Method
                || bindingContext.HttpContext.Request.Method == HttpMethod.Head.Method
                || bindingContext.HttpContext.Request.Method == HttpMethod.Options.Method
                || bindingContext.HttpContext.Request.Method == HttpMethod.Trace.Method)
            {
                properties = new Dictionary<string, object>();

                try
                {
                    foreach (var pair in bindingContext.HttpContext.Request.Query)
                    {
                        properties.Add(pair.Key, JsonConvert.DeserializeObject(pair.Value.ToString()));
                    }
                }
                catch
                {
                    // any deserialization issue
                    return;
                }
            }
            else
            {
                // get properties from request body
                string valueFromBody;
                using (var sr = new StreamReader(bindingContext.HttpContext.Request.Body))
                {
                    valueFromBody = await sr.ReadToEndAsync();
                }

                // must have request body
                if (string.IsNullOrEmpty(valueFromBody))
                {
                    return;
                }

                try
                {
                    properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(valueFromBody);
                }
                catch
                {
                    // any deserialization issue
                    return;
                }
            }

            var result = new VendorMediaTypeRequest
            {
                ContentType = contentType,
                ModelType = match,
                PropertyBag = new ReadOnlyDictionary<string, object>(properties)
            };

            bindingContext.Result = ModelBindingResult.Success(result);
        }
    }
}