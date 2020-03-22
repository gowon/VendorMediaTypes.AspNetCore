namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Collections.Generic;

    public sealed class VendorMediaTypeCollection : Dictionary<Type, string[]>
    {
        private static readonly VendorMediaTypeCollection Instance = new VendorMediaTypeCollection();

        private VendorMediaTypeCollection()
        {
        }

        public static VendorMediaTypeCollection GetInstance() => Instance;

        public void Add<T>(params string[] value)
        {
            Add(typeof(T), value);
        }
    }
}