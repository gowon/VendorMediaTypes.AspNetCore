namespace WebApiExample.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Models;

    public class PingHandler : IRequestHandler<Ping, Pong>
    {
        private readonly ILogger<PingHandler> _logger;

        public PingHandler(ILogger<PingHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"--- Handled Ping: {request.Message}");
            return await new ValueTask<Pong>(new Pong {Message = request.Message + " Pong"});
        }
    }
}