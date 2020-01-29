using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VehicleTrackingAPI.Models.DbModels
{
    public class TrackingModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public TrackingModel()
        {
            
        }
    }
}
