using System;

namespace VehicleTrackingAPI.Models.ResponseModels
{
    public class RegistrationResponse
    {
        public string VehicleDeviceId { get; set; }
        public Guid RegistrationId { get; set; }

        public RegistrationResponse(string vehicleDeviceId, Guid registrationId)
        {
            VehicleDeviceId = vehicleDeviceId;
            RegistrationId = registrationId;
        }
    }
}
