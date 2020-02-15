using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VehicleTracker.Contracts.Models.DbModels
{
    public class RegistrationInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleDeviceId { get; set; }

        public string RegistrationId { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime RegistrationDate { get; set; }

        public RegistrationInfo()
        {
            RegistrationId = Guid.NewGuid().ToString("D");
            RegistrationDate = DateTime.Now.Date;
        }
    }
}
