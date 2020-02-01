using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace VehicleTrackingAPI.Models.DbModels
{
    public class TrackingHistory
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public string VehicleDeviceId { get; set; }

        public TrackingInfo LatestTrackingInfo { get; set; }

        public List<TrackingInfo> TrackingInfoHistory { get; set; }

        public TrackingHistory(string registrationId, string vehicleDeviceId)
        {
            Id = registrationId;
            VehicleDeviceId = vehicleDeviceId;
            LatestTrackingInfo = new TrackingInfo();
            TrackingInfoHistory = new List<TrackingInfo>(); 
        }
    }
}
