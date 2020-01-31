using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IMongoCollection<TrackingHistory> _trackingHistory;

        public TrackingService(IVehicleTrackerDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);

            _trackingHistory = database.GetCollection<TrackingHistory>(config.VehicleTrackingCollectionName);
        }

        public async Task CreateTrackingHistoryAsync(TrackingHistory trackingHistory)
        {
            await _trackingHistory.InsertOneAsync(trackingHistory);
        }

        public async Task AddItemToTrackingHistoryAsync(string registrationId, TrackingModel trackingModel)
        {
            var filter = Builders<TrackingHistory>.Filter.Eq("_id", registrationId);
            var definition = Builders<TrackingHistory>.Update.Push(e => e.TrackingModels, trackingModel);
            var definition2 = definition.Set(e => e.LatestTrackingModel, trackingModel);
            await _trackingHistory.UpdateOneAsync(filter, definition2);
        }

        public async Task<TrackingModel> GetTrackingModelAsync(string registrationId)
        {
            var latestTrackingModel = await _trackingHistory.Find(th => th.Id == registrationId)
                                        .Project(th => th.LatestTrackingModel).FirstOrDefaultAsync();
            return latestTrackingModel;
        }

        public async Task<List<TrackingModel>> GetTrackingModelsInCertainTimeAsync(string registrationId, DateTime startTime, DateTime endTime)
        {
            var trackingHistory = await _trackingHistory.Find(history => history.Id == registrationId).FirstOrDefaultAsync();
            var trackingModels = trackingHistory.TrackingModels.FindAll(o =>
                                    o.TrackingTime >= startTime.ToUniversalTime() && o.TrackingTime <= endTime.ToUniversalTime());
            return trackingModels;
        }
    }
}
