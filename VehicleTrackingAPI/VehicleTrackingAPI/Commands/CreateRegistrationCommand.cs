using MediatR;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Commands
{
    public class CreateRegistrationCommand : IRequest<RegistrationResponse>
    {
        public string VehicleModel { get; set; }
        public string VehicleDeviceId { get; set; }

        public VehicleRegistrationModel GetVehicleRegistrationModel(CreateRegistrationCommand command)
        {
            var model = new VehicleRegistrationModel
            {
                VehicleModel = command.VehicleModel,
                VehicleDeviceId = command.VehicleDeviceId
            };
            return model;
        }
    }
}
