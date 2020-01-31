using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public interface ITrackingService
    {
        Task CreateTrackingHistoryAsync(TrackingHistory trackingHistory);
        Task AddItemToTrackingHistoryAsync(string registrationId, TrackingModel trackingModel);
        Task<TrackingModel> GetTrackingModelAsync(string registrationId);
        Task<List<TrackingModel>> GetTrackingModelsInCertainTimeAsync(string registrationId, DateTime startTime, DateTime endTime);
    }
}
