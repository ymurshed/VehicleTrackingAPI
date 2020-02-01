using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleTrackingAPI.Commands;
using VehicleTrackingAPI.Queries;

namespace VehicleTrackingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrackingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task Track([FromBody] AddTrackingCommand command)
        {
            await _mediator.Send(command);
        }


        [HttpGet("CurrentTracking")]
        [Authorize(Policy = Utility.Constants.AdminUserPolicy)]
        public async Task<IActionResult> GetCurrentTracking(string registrationId)
        {
            var query = new GetCurrentTrackingQuery(registrationId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpGet("TrackingsInCertainTime")]
        [Authorize(Policy = Utility.Constants.AdminUserPolicy)]
        public async Task<IActionResult> GetTrackingsInCertainTime(string registrationId, DateTime startTime, DateTime endTime)
        {
            var query = new GetTrackingsInCertainTimeQuery(registrationId, startTime, endTime);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpGet("LocationName")]
        public async Task<IActionResult> GetLocationName(double latitude, double longitude)
        {
            var query = new GetLocationNameQuery(latitude, longitude);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
