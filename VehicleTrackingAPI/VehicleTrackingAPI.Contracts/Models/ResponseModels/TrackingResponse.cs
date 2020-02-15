using System;

namespace VehicleTracker.Contracts.Models.ResponseModels
{
    public class TrackingResponse
    {
        public DateTime TrackingTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
