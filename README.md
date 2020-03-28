# VendorMediaTypes

Support content-based action selection in ASP.NET Core using custom vendor media types. VendorMediaTypes creates a clear path for exposing CQRS through a RESTful API.

| Package | NuGet | Description |
| --- | --- | --- |
| VendorMediaTypes | [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/VendorMediaTypes?color=blue)](https://www.nuget.org/packages/VendorMediaTypes) [![Nuget](https://img.shields.io/nuget/dt/VendorMediaTypes?color=blue)](https://www.nuget.org/packages/VendorMediaTypes) | .NET Standard project containing common code |
| VendorMediaTypes.Swashbuckle | [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/VendorMediaTypes.Swashbuckle?color=blue)](https://www.nuget.org/packages/VendorMediaTypes.Swashbuckle) [![Nuget](https://img.shields.io/nuget/dt/VendorMediaTypes.Swashbuckle?color=blue)](https://www.nuget.org/packages/VendorMediaTypes.Swashbuckle) | .NET Standard project containing filters to help Swashbuckle properly render endpoints using VendorMediaTypes |

## Usage

### Setup custom media types

Add associations to your endpoint objects to custom media types by decorating those classes with the MediaTypeAttribute.

```csharp
// ping.cs

    [MediaType("application/vnd.ping+json", "application/vnd.health-check+json")]
    public class Ping
    {
        public string Message { get; set; }
    }

// detailedping.cs

    [MediaType("application/vnd.detailed-ping+json")]
    public class DetailedPing
    {
        public string InstanceId { get; set; }
        public string Message { get; set; }
    }
```

> While the use of non-canonical media types is a common practice and this package is designed to facilitate that, it is discouraged by RFC 2616 because they are not registered by IANA.

### Register VendorMediaTypes with the application host

Register VendorMediaTypes in the generic dependency injection container by using `AddVendorMediaTypes` on your service collection. This is typically handled in the `ConfigureServices` method in your `Startup.cs`. By default, the extension method will scan the calling assembly for any classes decorated with `[MediaType]` and register them. This behavior can be overriden by manually registering your Type:ContentTypes manually.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddMvc(options =>
        {
            //...
        });

    services.AddVendorMediaTypes(collection =>
        {
            collection.Add<Ping>("application/vnd.ping+json", "application/vnd.health-check+json");
            collection.Add<DetailedPing>("application/vnd.detailed-ping+json");
        });

    //...
}
```

### Use VendorMediaTypeRequest in controller action

On your controller action, decorate the controller action with the `ConsumesVendorType` attribute and use the `VendorMediaTypeRequest` as the Action parameter. After successful routing, the `VendorMediaTypeRequest` can be used to generate the domain model defined in the request.

```csharp
// GET: api/ping
[HttpPost]
[ConsumesVendorType(typeof(Ping), typeof(DetailedPing))]
public async Task<IActionResult> ExecutePing(VendorMediaTypeRequest request)
{
    var query = request.CreateModel();
    // ...
}
```

### Add support for SwaggerGen

If you are using [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) to generate OpenAPI spec and Swagger UI documentation, it is important to use the `VendorMediaTypes.Swashbuckle` package to configure Swashbuckle on how to properly render endpoints that use VendorMediaTypes. This is accomplished by using `AddSwaggerGenSupport` during registraion.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddMvc(options =>
        {
            //...
        });

    services.AddVendorMediaTypes()
        .AddSwaggerGenSupport();

    //...

    services.AddSwaggerGen(/* ... */);
}
```

## References

Ali Kheyrollahi's articles/code on 5LMT:

- [5 levels of media type](http://byterot.blogspot.com/2012/12/5-levels-of-media-type-rest-csds.html)
- [Content-based action selection in ASP.NET Web API](http://byterot.blogspot.com/2013/11/Content-based-action-selection-ASP.NET-Web-API-REST-5LMT-Five-Levels-Of-Media-Type.html)
  - <https://github.com/aliostad/FiveLevelsOfMediaType>
- [Exposing CQRS Through a RESTful API](https://www.infoq.com/articles/rest-api-on-cqrs/)

## License

MIT
