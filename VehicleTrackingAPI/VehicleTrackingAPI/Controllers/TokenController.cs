using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleTrackingAPI.Queries;

namespace VehicleTrackingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userName, string password)
        {
            var query = new GetUserTokenQuery(userName, password);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
