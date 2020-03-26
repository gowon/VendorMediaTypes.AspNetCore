namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Collections.Generic;

    public sealed class VendorMediaTypeCollection : Dictionary<Type, string[]>
    {
        public void Add<T>(params string[] values)
        {
            Add(typeof(T), values);
        }
    }
}