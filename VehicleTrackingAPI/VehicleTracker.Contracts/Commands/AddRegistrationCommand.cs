using System.ComponentModel.DataAnnotations;
using MediatR;
using VehicleTracker.Contracts.Models.DbModels;
using VehicleTracker.Contracts.Models.ResponseModels;

namespace VehicleTracker.Contracts.Commands
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
