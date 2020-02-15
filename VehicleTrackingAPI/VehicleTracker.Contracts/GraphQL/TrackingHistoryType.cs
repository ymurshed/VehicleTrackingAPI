using GraphQL.Types;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Contracts.GraphQL
{
    public class TrackingHistoryType : ObjectGraphType<TrackingHistory>
    {
        public TrackingHistoryType()
        {
            Name = "TrackingHistory";
            Field(o => o.Id).Description("Primary key");
            Field(o => o.VehicleDeviceId).Description("Unique id of a vehicle");
            Field(o => o.LatestTrackingInfo, false, typeof(TrackingInfoType)).Description("Current tracking info of a vehicle");
            Field(o => o.TrackingInfoHistory, false, typeof(ListGraphType<TrackingInfoType>)).Description("List of all tracking info of a vehicle");
        }
    }
}
