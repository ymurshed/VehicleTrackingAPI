using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Commands;
using VehicleTrackingAPI.Models.DbModels.SubModels;
using VehicleTrackingAPI.Services;

namespace VehicleTrackingAPI.Handlers.CommandHandlers
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
            var geoPointModel = new GeoPointModel(request.Latitude, request.Longitude);
            await _trackingService.AddGeoPointAsync(request.RegistrationId, geoPointModel);

            _logger.LogInformation($"For RegistrationId: {request.RegistrationId} recorded Geo Position: {request.Latitude}, {request.Longitude}.");
            return Unit.Value;
        }
    }
}
