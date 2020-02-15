using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTracker.Business.Services;
using VehicleTracker.Business.Utility;
using VehicleTracker.Contracts.Models.ResponseModels;
using VehicleTracker.Contracts.Queries;

namespace VehicleTracker.Business.Handlers.QueryHandlers
{
    public class GetCurrentTrackingHandler : IRequestHandler<GetCurrentTrackingQuery, TrackingResponse>
    {
        private readonly ILogger<GetCurrentTrackingHandler> _logger;
        private readonly ITrackingService _trackingService;

        public GetCurrentTrackingHandler(ILogger<GetCurrentTrackingHandler> logger, ITrackingService trackingService)
        {
            _logger = logger;
            _trackingService = trackingService;
        }


        public async Task<TrackingResponse> Handle(GetCurrentTrackingQuery request, CancellationToken cancellationToken)
        {
            var trackingInfo = await _trackingService.GetTrackingInfoAsync(request.RegistrationId);

            if (trackingInfo == null)
            {
                _logger.LogInformation($"New device. Yet no tracking record added for the RegistrationId: {request.RegistrationId}.");
                return null;
            }

            var trackingResponse = trackingInfo.MapToTrackingResponse();
            return trackingResponse;
        }
    }
}
