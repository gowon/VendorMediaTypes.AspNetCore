namespace WebApiExample.Controllers
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Queries;
    using VendorMediaTypes;

    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/Ping
        [HttpPost]
        [ConsumesVendorType(typeof(DetailedPing), typeof(Ping))]
        public async Task<IActionResult> ExecutePing(VendorMediaTypeRequest request)
        {
            var query = request.CreateModel();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}