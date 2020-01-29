using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Commands;
using VehicleTrackingAPI.Models.ResponseModels;
using VehicleTrackingAPI.Services;

namespace VehicleTrackingAPI.Handlers.CommandHandlers
{
    public class CreateRegistrationHandler : IRequestHandler<CreateRegistrationCommand, RegistrationResponse>
    {
        private readonly ILogger<CreateRegistrationHandler> _logger;
        private readonly IRegistrationService _registrationService;
        
        public CreateRegistrationHandler(ILogger<CreateRegistrationHandler> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public async Task<RegistrationResponse> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            var registeredVehicle = await _registrationService.Get(request.VehicleDeviceId);
            if (registeredVehicle != null)
            {
                _logger.LogWarning($"VehicleDeviceId: {registeredVehicle.VehicleDeviceId} already registered!");
                return new RegistrationResponse(registeredVehicle.VehicleDeviceId, registeredVehicle.RegistrationId);
            }

            var registrationModel = request.GetRegistrationModel(request);
            if (_registrationService.CreateRegistrationAsync(registrationModel))
            {
                _logger.LogInformation(
                    $"VehicleDeviceId: {registrationModel.VehicleDeviceId} has been registered with RegistrationId: {registrationModel.RegistrationId}");
                return new RegistrationResponse(registrationModel.VehicleDeviceId, registrationModel.RegistrationId);
            }

            _logger.LogError($"For VehicleDeviceId: {registrationModel.VehicleDeviceId} registration has failed!");
            return null;
        }
    }
}
