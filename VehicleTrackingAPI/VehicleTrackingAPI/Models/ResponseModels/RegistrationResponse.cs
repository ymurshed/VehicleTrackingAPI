using System;

namespace VehicleTrackingAPI.Models.ResponseModels
{
    public class RegistrationResponse
    {
        public string VehicleDeviceId { get; set; }
        public string RegistrationId { get; set; }

        public RegistrationResponse(string vehicleDeviceId, string registrationId)
        {
            VehicleDeviceId = vehicleDeviceId;
            RegistrationId = registrationId;
        }
    }
}
