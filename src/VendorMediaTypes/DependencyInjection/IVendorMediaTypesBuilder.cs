namespace VendorMediaTypes.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IVendorMediaTypesBuilder
    {
        IServiceCollection Services { get; }
    }
}