namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VendorMediaTypesAttribute : ConsumesAttribute
    {
        public VendorMediaTypesAttribute(Type type, params Type[] types) : base(GetFirstContentType(type),
            GetRemainingContentTypes(type, types))
        {
        }

        private static string GetFirstContentType(Type type)
        {
            return VendorMediaTypeCollection.GetInstance()[type].First();
        }

        private static string[] GetRemainingContentTypes(Type type, Type[] types)
        {
            var list = new List<Type> {type};
            list.AddRange(types);
            return list.SelectMany(t => VendorMediaTypeCollection.GetInstance()[t]).Skip(1).ToArray();
        }
    }
}