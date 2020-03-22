namespace WebApiExample.Queries
{
    using MediatR;
    using Models;

    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }
}