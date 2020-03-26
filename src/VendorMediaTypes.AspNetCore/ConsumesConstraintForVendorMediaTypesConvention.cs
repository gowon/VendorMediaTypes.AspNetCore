namespace VendorMediaTypes.AspNetCore
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    // https://github.com/dotnet/aspnetcore/blob/f79f2e3b1200f8e672b77583a54e6157e49da9e4/src/Mvc/Mvc.Core/src/ApplicationModels/ConsumesConstraintForFormFileParameterConvention.cs
    public class ConsumesConstraintForVendorMediaTypesConvention : IActionModelConvention
    {
        private readonly VendorMediaTypeCollection _mediaTypeCollection;

        public ConsumesConstraintForVendorMediaTypesConvention(VendorMediaTypeCollection mediaTypeCollection)
        {
            _mediaTypeCollection = mediaTypeCollection ?? throw new ArgumentNullException(nameof(mediaTypeCollection));
        }

        public void Apply(ActionModel action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            AddVendorConsumesAttribute(action);
        }

        internal void AddVendorConsumesAttribute(ActionModel action)
        {
            var types = action.Attributes.OfType<ConsumesVendorTypeAttribute>()
                .SelectMany(attribute => attribute.Types)
                .SelectMany(type => _mediaTypeCollection[type]).ToList();

            if (types.Any())
            {
                var existing = action.Filters.SingleOrDefault(metadata =>
                    metadata.GetType().IsAssignableFrom(typeof(ConsumesAttribute)));

                if (existing is ConsumesAttribute attribute)
                {
                    action.Filters.Remove(attribute);
                    var existingTypes = attribute.ContentTypes.Cast<string>();
                    types.AddRange(existingTypes);
                }

                action.Filters.Add(new ConsumesAttribute(types[0], types.Skip(1).ToArray()));
            }
        }
    }
}