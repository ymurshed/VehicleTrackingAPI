using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Models.ResponseModels;
using VehicleTrackingAPI.Queries;
using VehicleTrackingAPI.Services;

namespace VehicleTrackingAPI.Handlers.QueryHandlers
{
    public class GetRegistrationResponseByDeviceIdHandler : IRequestHandler<GetRegistrationResponseByDeviceIdQuery, RegistrationResponse>
    {
        private readonly ILogger<GetRegistrationResponseByDeviceIdHandler> _logger;
        private readonly IRegistrationService _registrationService;

        public GetRegistrationResponseByDeviceIdHandler(ILogger<GetRegistrationResponseByDeviceIdHandler> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public async Task<RegistrationResponse> Handle(GetRegistrationResponseByDeviceIdQuery request, CancellationToken cancellationToken)
        {
            var registrationInfo = await _registrationService.Get(request.DeviceId);
            _logger.LogInformation($"For DeviceId: {request.DeviceId}, RegistrationId: {registrationInfo?.Id}");
            return registrationInfo == null ? null : new RegistrationResponse(registrationInfo.VehicleDeviceId, registrationInfo.RegistrationId);
        }
    }
}
