namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [DataContract]
    public sealed class VendorMediaTypeRequest
    {
        internal VendorMediaTypeRequest()
        {
        }

        public VendorMediaTypeRequest(string contentType, Type modelType, IDictionary<string, object> properties = null)
        {
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            ModelType = modelType ?? throw new ArgumentNullException(nameof(modelType));
            PropertyBag = new ReadOnlyDictionary<string, object>(properties ?? new Dictionary<string, object>());
        }

        [IgnoreDataMember]
        [JsonIgnore]
        public string ContentType { get; internal set; }
        
        [IgnoreDataMember]
        [JsonIgnore]
        public Type ModelType { get; internal set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public ReadOnlyDictionary<string, object> PropertyBag { get; internal set; }

        public object CreateModel()
        {
            var instance = Activator.CreateInstance(ModelType);
            var properties = ModelType.GetProperties();

            foreach (var pair in PropertyBag)
            {
                var propertyInfo =
                    properties.SingleOrDefault(info => info.Name.Equals(pair.Key, StringComparison.Ordinal));
                if (propertyInfo == null)
                {
                    continue;
                }

                // https://stackoverflow.com/a/41466710
                var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = pair.Value == null ? null : Convert.ChangeType(pair.Value, propertyType);
                propertyInfo.SetValue(instance, safeValue, null);
            }

            return instance;
        }

        public T CreateModel<T>()
        {
            return (T) CreateModel();
        }
    }
}