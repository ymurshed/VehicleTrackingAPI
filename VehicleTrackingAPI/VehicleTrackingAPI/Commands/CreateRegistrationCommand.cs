using System.ComponentModel.DataAnnotations;
using MediatR;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Commands
{
    public class CreateRegistrationCommand : IRequest<RegistrationResponse>
    {
        [Required]
        [MinLength(5)]
        public string VehicleDeviceId { get; set; }

        [MaxLength(200)]
        public string VehicleModel { get; set; }

        public RegistrationModel GetRegistrationModel(CreateRegistrationCommand command)
        {
            var model = new RegistrationModel
            {
                VehicleModel = command.VehicleModel,
                VehicleDeviceId = command.VehicleDeviceId
            };
            return model;
        }
    }
}
