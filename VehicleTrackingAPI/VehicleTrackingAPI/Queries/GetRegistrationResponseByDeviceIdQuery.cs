using MediatR;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Queries
{
    public class GetRegistrationResponseByDeviceIdQuery : IRequest<RegistrationResponse>
    {
        public string DeviceId { get; }

        public GetRegistrationResponseByDeviceIdQuery(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
