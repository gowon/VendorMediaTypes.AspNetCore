namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Collections.Generic;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MediaTypeAttribute : Attribute
    {
        public MediaTypeAttribute(string type, params string[] types)
        {
            var list = new List<string>
            {
                type
            };

            if (types != null)
            {
                list.AddRange(types);
            }

            Types = list;
        }

        public List<string> Types { get; }
    }
}