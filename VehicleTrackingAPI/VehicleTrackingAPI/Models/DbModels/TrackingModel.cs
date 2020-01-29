using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using VehicleTrackingAPI.Models.DbModels.SubModels;

namespace VehicleTrackingAPI.Models.DbModels
{
    public class TrackingModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        
        public List<GeoPointModel> GeoPointModels { get; set; }

        public TrackingModel(string registrationId)
        {
            Id = registrationId;
            GeoPointModels = new List<GeoPointModel>();
        }
    }
}
