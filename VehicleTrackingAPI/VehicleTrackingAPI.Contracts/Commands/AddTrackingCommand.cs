using System.ComponentModel.DataAnnotations;
using MediatR;

namespace VehicleTracker.Contracts.Commands
{
    public class AddTrackingCommand : IRequest
    {
        [Required]
        public string RegistrationId { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
