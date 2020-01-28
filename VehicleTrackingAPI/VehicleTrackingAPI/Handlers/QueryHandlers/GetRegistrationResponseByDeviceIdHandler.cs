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
        private readonly IVehicleRegistrationService _registrationService;

        public GetRegistrationResponseByDeviceIdHandler(ILogger<GetRegistrationResponseByDeviceIdHandler> logger, IVehicleRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public async Task<RegistrationResponse> Handle(GetRegistrationResponseByDeviceIdQuery request, CancellationToken cancellationToken)
        {
            var registrationModel = await _registrationService.Get(request.DeviceId);
            return registrationModel == null ? null : new RegistrationResponse(registrationModel.VehicleDeviceId, registrationModel.RegistrationId);
        }
    }
}
