namespace WebApiExample.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Models;

    public class DetailedPingHandler : IRequestHandler<DetailedPing, DetailedPong>
    {
        private readonly ILogger<PingHandler> _logger;

        public DetailedPingHandler(ILogger<PingHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DetailedPong> Handle(DetailedPing request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"--- Handled Ping: {request.Message}");
            return await new ValueTask<DetailedPong>(new DetailedPong
            {
                Id = Guid.NewGuid(),
                InstanceId = request.InstanceId,
                Message = request.Message + " Pong",
                Timestamp = DateTime.Now
            });
        }
    }
}