using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Models.ResponseModels;
using VehicleTrackingAPI.Queries;
using VehicleTrackingAPI.Services;
using VehicleTrackingAPI.Utility;

namespace VehicleTrackingAPI.Handlers.QueryHandlers
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
            var latestTrackingModel = await _trackingService.GetTrackingModelAsync(request.RegistrationId);

            if (latestTrackingModel == null)
            {
                _logger.LogInformation($"New device. Yet no tracking record added for the RegistrationId: {request.RegistrationId}.");
                return null;
            }

            var trackingResponse = latestTrackingModel.MapToTrackingResponse();
            return trackingResponse;
        }
    }
}
