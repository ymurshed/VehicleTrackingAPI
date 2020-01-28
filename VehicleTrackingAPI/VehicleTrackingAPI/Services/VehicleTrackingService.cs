using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class VehicleTrackingService : IVehicleTrackingService
    {
        private readonly IMongoCollection<VehicleTrackingModel> _trackingModel;

        public VehicleTrackingService(IVehicleTrackerDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _trackingModel = database.GetCollection<VehicleTrackingModel>(settings.VehicleTrackingCollectionName);
        }
    }
}
