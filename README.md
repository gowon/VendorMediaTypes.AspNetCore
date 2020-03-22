# VendorMediaTypes.AspNetCore

Support content-based action selection in ASP.NET Core using custom vendor media types.

## Usage 

In your `Startup.cs`, apply `AddVendorMediaTypesSupport` when configuring your MVC services. In this method, you can add mappings between your domain objects and the media types:

> While a common practice, use of non-canonical media type is discouraged by RFC 2616 (since above media type is not registered by IANA).

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddMvc(options =>
        {
            //...
        })
        .AddVendorMediaTypesSupport(collection =>
        {
            collection.Add<Ping>("application/vnd.ping+json", "application/vnd.health-check+json");
            collection.Add<DetailedPing>("application/vnd.detailed-ping+json");
        });

    //...
}
```

## References

- Ali Kheyrollahi's articles/code on 5LMT:
  - [5 levels of media type](http://byterot.blogspot.com/2012/12/5-levels-of-media-type-rest-csds.html)
  - [Content-based action selection in ASP.NET Web API](http://byterot.blogspot.com/2013/11/Content-based-action-selection-ASP.NET-Web-API-REST-5LMT-Five-Levels-Of-Media-Type.html)
    - <https://github.com/aliostad/FiveLevelsOfMediaType>
  - [Exposing CQRS Through a RESTful API](https://www.infoq.com/articles/rest-api-on-cqrs/)

## License

MIT
