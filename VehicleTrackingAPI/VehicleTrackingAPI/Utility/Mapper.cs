using System.Collections.Generic;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Utility
{
    public static class Mapper
    {
        public static TrackingResponse MapToTrackingResponse(this TrackingModel trackingModel)
        {
            var trackingResponse = new TrackingResponse
            {
                TrackingTime = trackingModel.TrackingTime,
                Latitude = trackingModel.Location.Coordinates.Latitude,
                Longitude = trackingModel.Location.Coordinates.Longitude
            };
            return trackingResponse;
        }

        public static List<TrackingResponse> MapToTrackingResponseList(this List<TrackingModel> trackingModelList)
        {
            var trackingResponseList = new List<TrackingResponse>();
            foreach (var trackingModel in trackingModelList)
            {
                trackingResponseList.Add(trackingModel.MapToTrackingResponse());
            }
            return trackingResponseList;
        }
    }
}
