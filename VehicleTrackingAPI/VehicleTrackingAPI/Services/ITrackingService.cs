﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public interface ITrackingService
    {
        Task CreateTrackingHistoryAsync(TrackingHistory trackingHistory);
        Task AddItemToTrackingHistoryAsync(string registrationId, TrackingInfo trackingInfo);
        Task<TrackingInfo> GetTrackingModelAsync(string registrationId);
        Task<List<TrackingInfo>> GetTrackingModelsInCertainTimeAsync(string registrationId, DateTime startTime, DateTime endTime);
    }
}
