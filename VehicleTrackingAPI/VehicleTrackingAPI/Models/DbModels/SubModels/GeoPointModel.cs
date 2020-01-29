using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace VehicleTrackingAPI.Models.DbModels.SubModels
{
    public class GeoPointModel
    {
        public DateTime TrackingTime { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        public GeoPointModel(double latitude, double longitude)
        {
            TrackingTime = DateTime.UtcNow;
            Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude, latitude));
        }
    }
}
