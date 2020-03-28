namespace VendorMediaTypes.AspNetCore
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IVendorMediaTypesBuilder
    {
        IServiceCollection Services { get; }
    }
}