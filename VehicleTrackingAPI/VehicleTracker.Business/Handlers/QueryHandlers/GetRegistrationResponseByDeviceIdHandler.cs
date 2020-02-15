using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.Models.ResponseModels;
using VehicleTracker.Contracts.Queries;

namespace VehicleTracker.Business.Handlers.QueryHandlers
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
            _logger.LogInformation($"For DeviceId: {request.DeviceId}, RegistrationId: {registrationInfo?.Id}.");
            return registrationInfo == null ? null : new RegistrationResponse(registrationInfo.VehicleDeviceId, registrationInfo.RegistrationId);
        }
    }
}
