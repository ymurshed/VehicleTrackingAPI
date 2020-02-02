using System;

namespace VehicleTrackingClient.Models
{
    public class TrackingResponse
    {
        public DateTime TrackingTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
