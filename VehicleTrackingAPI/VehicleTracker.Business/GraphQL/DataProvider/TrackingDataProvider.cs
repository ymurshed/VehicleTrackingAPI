using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Business.GraphQL.DataProvider
{
    public class TrackingDataProvider
    {
        private static ITrackingService _trackingService;

        public TrackingDataProvider(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        public Task<TrackingInfo> GetTrackingInfo(string registrationId)
        {
            return Task.Factory.StartNew(() => _trackingService.GetTrackingInfoAsync(registrationId).Result);
        }

        public Task<List<TrackingInfo>> GetTrackingInfoHistory(string registrationId, DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => _trackingService.GetTrackingInfoHistoryAsync(registrationId, startTime, endTime).Result);
        }

        public Task<TrackingHistory> GetTrackingHistoryDetails(string registrationId)
        {
            return Task.Factory.StartNew(() => _trackingService.GetTrackingHistoryDetailsAsync(registrationId).Result);
        }
    }
}
