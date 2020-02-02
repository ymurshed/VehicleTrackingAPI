using System;
namespace VehicleTrackingClient.Models
{
    public class AddTrackingCommand
    {
        public string RegistrationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
