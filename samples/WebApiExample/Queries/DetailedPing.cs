namespace WebApiExample.Queries
{
    using MediatR;
    using Models;
    using VendorMediaTypes;

    [MediaType("application/vnd.detailed-ping+json")]
    public class DetailedPing : IRequest<DetailedPong>
    {
        public string InstanceId { get; set; }
        public string Message { get; set; }
    }
}