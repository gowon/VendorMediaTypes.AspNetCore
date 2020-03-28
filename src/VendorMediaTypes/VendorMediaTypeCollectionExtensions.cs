namespace VendorMediaTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class VendorMediaTypeCollectionExtensions
    {
        public static void Add<T>(this VendorMediaTypeCollection collection, params string[] values)
        {
            collection.Add(typeof(T), values);
        }

        public static void AddAssembly(this VendorMediaTypeCollection collection, Assembly assembly)
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
        }

        public static bool TryGetType(this VendorMediaTypeCollection collection, string value, out Type type)
        {
            type = null;
            var match = collection.SingleOrDefault(pair => pair.Value.Any(value.Contains));
            if (match.Equals(default(KeyValuePair<Type, string[]>)))
            {
                // we couldn't find a matching type
                return false;
            }

            type = match.Key;
            return true;
        }
    }
}