using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.DbModels.SubModels;

namespace VehicleTrackingAPI.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IMongoCollection<TrackingModel> _trackingModel;

        public TrackingService(IVehicleTrackerDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);

            _trackingModel = database.GetCollection<TrackingModel>(config.VehicleTrackingCollectionName);
        }

        public async Task AddTrackingAsync(TrackingModel trackingModel)
        {
            await _trackingModel.InsertOneAsync(trackingModel);
        }

        public async Task AddGeoPointAsync(string registrationId, GeoPointModel geoPointModel)
        {
            var arrayFilter = Builders<TrackingModel>.Filter.Eq("_id", registrationId);
            var arrayUpdate = Builders<TrackingModel>.Update.Push(e => e.GeoPointModels, geoPointModel);
            await _trackingModel.UpdateOneAsync(arrayFilter, arrayUpdate);
        }
    }
}
