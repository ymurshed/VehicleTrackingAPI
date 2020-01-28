using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VehicleTrackingAPI.Models.DbModels
{
    public class VehicleTrackingModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public VehicleTrackingModel()
        {
            
        }
    }
}
