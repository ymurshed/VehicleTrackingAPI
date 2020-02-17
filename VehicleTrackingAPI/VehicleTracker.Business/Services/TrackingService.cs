using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTracker.Contracts.Models.AppSettingsModels;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Business.Services
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

        public async Task AddItemToTrackingHistoryAsync(string registrationId, TrackingInfo trackingInfo)
        {
            var filter = Builders<TrackingHistory>.Filter.Eq("_id", registrationId);
            var definition = Builders<TrackingHistory>.Update.Push(e => e.TrackingInfoHistory, trackingInfo);
            var definition2 = definition.Set(e => e.LatestTrackingInfo, trackingInfo);
            await _trackingHistory.UpdateOneAsync(filter, definition2);
        }

        public async Task<TrackingInfo> GetTrackingInfoAsync(string registrationId)
        {
            var trackingInfo = await _trackingHistory.Find(th => th.Id == registrationId)
                                .Project(th => th.LatestTrackingInfo).FirstOrDefaultAsync();
            return trackingInfo;
        }

        public async Task<List<TrackingInfo>> GetTrackingInfoHistoryAsync(string registrationId, DateTime startTime, DateTime endTime)
        {
            var trackingHistory = await _trackingHistory.Find(th => th.Id == registrationId).FirstOrDefaultAsync();
            var trackingInfoList = trackingHistory.TrackingInfoHistory.FindAll(ti =>
                                    ti.TrackingTime >= startTime.ToUniversalTime() && ti.TrackingTime <= endTime.ToUniversalTime());
            return trackingInfoList;
        }

        public async Task<TrackingHistory> GetTrackingHistoryDetailsAsync(string registrationId)
        {
            return await _trackingHistory.Find(th => th.Id == registrationId).FirstOrDefaultAsync();
        }
    }
}
