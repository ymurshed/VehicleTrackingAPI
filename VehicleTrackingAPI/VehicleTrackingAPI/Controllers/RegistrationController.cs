using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleTrackingAPI.Commands;
using VehicleTrackingAPI.Queries;

namespace VehicleTrackingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateRegistrationCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("GetRegistrationResponse", new { deviceId = result.VehicleDeviceId }, result);
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetRegistrationResponse(string deviceId)
        {
            var query = new GetRegistrationResponseByDeviceIdQuery(deviceId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
