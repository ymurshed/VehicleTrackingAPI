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

        /// <summary>
        /// Creates a location record of a registered device.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Track([FromBody] AddTrackingCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Provides the lastest TrackingResponse of a registered device.
        /// Use admin JWT token as http auth header to access this api.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        [HttpGet("CurrentTracking")]
        [Authorize(Policy = Utility.Constants.AdminUserPolicy)]
        public async Task<IActionResult> GetCurrentTracking(string registrationId)
        {
            var query = new GetCurrentTrackingQuery(registrationId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        /// <summary>
        /// Provides the list of TrackingResponse for a given datetime range of a registered device.
        /// Use admin JWT token as http auth header to access this api.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("TrackingsInCertainTime")]
        [Authorize(Policy = Utility.Constants.AdminUserPolicy)]
        public async Task<IActionResult> GetTrackingsInCertainTime(string registrationId, DateTime startTime, DateTime endTime)
        {
            var query = new GetTrackingsInCertainTimeQuery(registrationId, startTime, endTime);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        /// <summary>
        /// Bonus Task
        /// Get the location name using Google Map Api.
        /// Keep the valid ApiKey in appsettings file.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [HttpGet("LocationName")]
        public async Task<IActionResult> GetLocationName(double latitude, double longitude)
        {
            var query = new GetLocationNameQuery(latitude, longitude);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
