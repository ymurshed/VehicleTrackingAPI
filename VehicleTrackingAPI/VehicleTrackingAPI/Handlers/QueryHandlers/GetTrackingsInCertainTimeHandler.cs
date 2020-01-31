using System.Collections.Generic;
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
            var trackingModelList = await _trackingService.GetTrackingModelsInCertainTimeAsync(request.RegistrationId, request.StartTime, request.EndTime);
            if (trackingModelList == null || trackingModelList.Count == 0)
            {
                _logger.LogInformation($"No tracking record found for the RegistrationId: {request.RegistrationId}.");
                return null;
            }

            var trackingResponseList = trackingModelList.MapToTrackingResponseList();
            return trackingResponseList;
        }
    }
}
