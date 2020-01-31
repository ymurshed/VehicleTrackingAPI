using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace VehicleTrackingAPI.Models.DbModels
{
    public class TrackingHistory
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public TrackingModel LatestTrackingModel { get; set; }

        public List<TrackingModel> TrackingModels { get; set; }

        public TrackingHistory(string registrationId)
        {
            Id = registrationId;
            LatestTrackingModel = new TrackingModel();
            TrackingModels = new List<TrackingModel>(); 
        }
    }
}
