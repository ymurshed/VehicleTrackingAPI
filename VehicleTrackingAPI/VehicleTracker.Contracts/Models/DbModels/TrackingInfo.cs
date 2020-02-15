using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace VehicleTracker.Contracts.Models.DbModels
{
    /// <summary>
    /// If the customer wants to store more properties (like: fuel, speed, etc), 
    /// then add properties here for extensibility.
    /// </summary>
    public class TrackingInfo
    {
        public DateTime TrackingTime { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        public TrackingInfo() {}

        public TrackingInfo(double latitude, double longitude)
        {
            TrackingTime = DateTime.Now;
            TrackingTime = TrackingTime.AddMilliseconds(-TrackingTime.Millisecond); // Remove ms
            Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude, latitude));
        }
    }
}
