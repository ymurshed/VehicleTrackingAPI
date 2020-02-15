using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.Commands;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Business.Handlers.CommandHandlers
{
    public class AddTrackingHandler : IRequestHandler<AddTrackingCommand>
    {
        private readonly ILogger<AddTrackingHandler> _logger;
        private readonly ITrackingService _trackingService;
        
        public AddTrackingHandler(ILogger<AddTrackingHandler> logger, ITrackingService trackingService)
        {
            _logger = logger;
            _trackingService = trackingService;
        }

        public async Task<Unit> Handle(AddTrackingCommand request, CancellationToken cancellationToken)
        {
            var trackingInfo = new TrackingInfo(request.Latitude, request.Longitude);
            await _trackingService.AddItemToTrackingHistoryAsync(request.RegistrationId, trackingInfo);

            _logger.LogInformation($"For RegistrationId: {request.RegistrationId} recorded Geo Position: {request.Latitude}, {request.Longitude}.");
            return Unit.Value;
        }
    }
}
