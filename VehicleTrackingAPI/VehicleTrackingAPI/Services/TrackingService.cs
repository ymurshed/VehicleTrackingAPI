using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IMongoCollection<TrackingModel> _trackingModel;

        public TrackingService(IVehicleTrackerDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _trackingModel = database.GetCollection<TrackingModel>(settings.VehicleTrackingCollectionName);
        }
    }
}
