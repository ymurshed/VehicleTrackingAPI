using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleTracker.Contracts.Commands;
using VehicleTracker.Contracts.Queries;

namespace VehicleTracker.Api.Controllers
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

        /// <summary>
        /// Register a device by Vehicle DeviceId.
        /// Assuming each Vehicle has it's unique DeviceId. 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AddRegistrationCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("GetRegistrationResponse", new { deviceId = result.VehicleDeviceId }, result);
        }

        /// <summary>
        /// Get the RegistrationId of a device.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRegistrationResponse(string deviceId)
        {
            var query = new GetRegistrationResponseByDeviceIdQuery(deviceId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
