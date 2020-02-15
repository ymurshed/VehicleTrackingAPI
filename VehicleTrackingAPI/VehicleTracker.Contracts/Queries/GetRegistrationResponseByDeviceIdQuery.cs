using MediatR;
using VehicleTracker.Contracts.Models.ResponseModels;

namespace VehicleTracker.Contracts.Queries
{
    public class GetRegistrationResponseByDeviceIdQuery : IRequest<RegistrationResponse>
    {
        public string DeviceId { get; set; }

        public GetRegistrationResponseByDeviceIdQuery(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
