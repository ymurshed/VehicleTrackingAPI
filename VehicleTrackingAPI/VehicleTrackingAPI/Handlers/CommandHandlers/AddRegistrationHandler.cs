using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Commands;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.ResponseModels;
using VehicleTrackingAPI.Services;

namespace VehicleTrackingAPI.Handlers.CommandHandlers
{
    public class AddRegistrationHandler : IRequestHandler<AddRegistrationCommand, RegistrationResponse>
    {
        private readonly ILogger<AddRegistrationHandler> _logger;
        private readonly IRegistrationService _registrationService;
        private readonly ITrackingService _trackingService;
        
        public AddRegistrationHandler(ILogger<AddRegistrationHandler> logger, 
                                      IRegistrationService registrationService, ITrackingService trackingService)
        {
            _logger = logger;
            _registrationService = registrationService;
            _trackingService = trackingService;
        }

        private async Task CreateTrackingHistoryPlaceholder(string registrationId)
        {
            await _trackingService.CreateTrackingHistoryAsync(new TrackingHistory(registrationId));
            _logger.LogInformation($"RegistrationId: {registrationId} has been synced with TrackingHistory.");
        }

        public async Task<RegistrationResponse> Handle(AddRegistrationCommand request, CancellationToken cancellationToken)
        {
            var registeredVehicle = await _registrationService.Get(request.VehicleDeviceId);
            if (registeredVehicle != null)
            {
                _logger.LogWarning($"VehicleDeviceId: {registeredVehicle.VehicleDeviceId} already registered!");
                return new RegistrationResponse(registeredVehicle.VehicleDeviceId, registeredVehicle.RegistrationId);
            }

            var registrationModel = request.GetRegistrationModel(request);
            await _registrationService.AddRegistrationAsync(registrationModel);

            if (registrationModel.Id != null)
            {
                _logger.LogInformation(
                    $"VehicleDeviceId: {registrationModel.VehicleDeviceId} has been registered with RegistrationId: {registrationModel.RegistrationId}.");

                await CreateTrackingHistoryPlaceholder(registrationModel.RegistrationId);
                return new RegistrationResponse(registrationModel.VehicleDeviceId, registrationModel.RegistrationId);
            }

            _logger.LogError($"For VehicleDeviceId: {registrationModel.VehicleDeviceId} registration has failed!");
            return null;
        }
    }
}
