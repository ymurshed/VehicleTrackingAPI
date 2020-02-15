using System.Collections.Generic;
using System.Linq;
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
    public class GetTrackingsInCertainTimeHandler : IRequestHandler<GetTrackingsInCertainTimeQuery, List<TrackingResponse>>
    {
        private readonly ILogger<GetTrackingsInCertainTimeHandler> _logger;
        private readonly ITrackingService _trackingService;

        public GetTrackingsInCertainTimeHandler(ILogger<GetTrackingsInCertainTimeHandler> logger, ITrackingService trackingService)
        {
            _logger = logger;
            _trackingService = trackingService;
        }

        public async Task<List<TrackingResponse>> Handle(GetTrackingsInCertainTimeQuery request, CancellationToken cancellationToken)
        {
            var trackingInfoList = await _trackingService.GetTrackingInfoHistoryAsync(request.RegistrationId, request.StartTime, request.EndTime);
            if (trackingInfoList == null || !trackingInfoList.Any())
            {
                _logger.LogInformation($"No tracking record found for the RegistrationId: {request.RegistrationId}.");
                return null;
            }

            var trackingResponseList = trackingInfoList.MapToTrackingResponseList();
            return trackingResponseList;
        }
    }
}
