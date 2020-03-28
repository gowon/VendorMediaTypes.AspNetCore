namespace VendorMediaTypes
{
    using System;
    using System.Collections.Generic;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ConsumesVendorTypeAttribute : Attribute
    {
        public ConsumesVendorTypeAttribute(Type type, params Type[] types)
        {
            var list = new List<Type>
            {
                type
            };

            if (types != null)
            {
                list.AddRange(types);
            }

            Types = list;
        }

        public List<Type> Types { get; set; }
    }
}