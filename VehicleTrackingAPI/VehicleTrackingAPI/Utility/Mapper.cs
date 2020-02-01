using System.Collections.Generic;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Utility
{
    public static class Mapper
    {
        public static TrackingResponse MapToTrackingResponse(this TrackingInfo trackingInfo)
        {
            var trackingResponse = new TrackingResponse
            {
                TrackingTime = trackingInfo.TrackingTime,
                Latitude = trackingInfo.Location.Coordinates.Latitude,
                Longitude = trackingInfo.Location.Coordinates.Longitude
            };
            return trackingResponse;
        }

        public static List<TrackingResponse> MapToTrackingResponseList(this List<TrackingInfo> trackingInfoList)
        {
            var trackingResponseList = new List<TrackingResponse>();
            foreach (var trackingInfo in trackingInfoList)
            {
                trackingResponseList.Add(trackingInfo.MapToTrackingResponse());
            }
            return trackingResponseList;
        }
    }
}
