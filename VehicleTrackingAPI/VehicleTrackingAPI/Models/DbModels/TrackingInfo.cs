using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace VehicleTrackingAPI.Models.DbModels
{
    public class TrackingInfo
    {
        public DateTime TrackingTime { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        public TrackingInfo() {}

        public TrackingInfo(double latitude, double longitude)
        {
            TrackingTime = DateTime.Now;
            Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude, latitude));
        }
    }
}
