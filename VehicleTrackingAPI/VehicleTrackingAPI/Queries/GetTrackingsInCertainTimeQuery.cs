﻿using System;
using System.Collections.Generic;
using MediatR;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Queries
{
    public class GetTrackingsInCertainTimeQuery : IRequest<List<TrackingResponse>>
    {
        public string RegistrationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public GetTrackingsInCertainTimeQuery(string registrationId, DateTime startTime, DateTime endTime)
        {
            RegistrationId = registrationId;
            StartTime  = startTime;
            EndTime = endTime;
        }
    }
}
