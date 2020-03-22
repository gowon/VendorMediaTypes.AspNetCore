namespace WebApiExample.Queries
{
    using MediatR;
    using Models;

    public class DetailedPing : IRequest<DetailedPong>
    {
        public string InstanceId { get; set; }
        public string Message { get; set; }
    }
}