using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.Commands;
using VehicleTracker.Contracts.Models.DbModels;
using VehicleTracker.Contracts.Models.ResponseModels;

namespace VehicleTracker.Business.Handlers.CommandHandlers
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

        private async Task CreateTrackingHistoryPlaceholder(string registrationId, string vehicleDeviceId)
        {
            await _trackingService.CreateTrackingHistoryAsync(new TrackingHistory(registrationId, vehicleDeviceId));
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

            var registrationInfo = request.GetRegistrationInfo(request);
            await _registrationService.AddRegistrationAsync(registrationInfo);

            if (registrationInfo.Id != null)
            {
                _logger.LogInformation(
                    $"VehicleDeviceId: {registrationInfo.VehicleDeviceId} has been registered with RegistrationId: {registrationInfo.RegistrationId}.");

                await CreateTrackingHistoryPlaceholder(registrationInfo.RegistrationId, registrationInfo.VehicleDeviceId);
                return new RegistrationResponse(registrationInfo.VehicleDeviceId, registrationInfo.RegistrationId);
            }

            _logger.LogError($"For VehicleDeviceId: {registrationInfo.VehicleDeviceId} registration has failed!");
            return null;
        }
    }
}
