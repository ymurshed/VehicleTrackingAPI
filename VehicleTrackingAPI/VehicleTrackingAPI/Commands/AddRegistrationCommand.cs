using System.ComponentModel.DataAnnotations;
using MediatR;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Commands
{
    public class AddRegistrationCommand : IRequest<RegistrationResponse>
    {
        [Required]
        [MaxLength(7)]
        public string VehicleDeviceId { get; set; }

        [MaxLength(100)]
        public string VehicleModel { get; set; }

        public RegistrationInfo GetRegistrationInfo(AddRegistrationCommand command)
        {
            var model = new RegistrationInfo
            {
                VehicleModel = command.VehicleModel,
                VehicleDeviceId = command.VehicleDeviceId
            };
            return model;
        }
    }
}
