using GraphQL.Types;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Contracts.GraphQL
{
    public class TrackingInfoType : ObjectGraphType<TrackingInfo>
    {
        public TrackingInfoType()
        {
            Name = "TrackingInfo";
            Field(o => o.Location, false, typeof(LocationType)).Description("Geo location contains type, longitude & latitude");
            Field(o => o.TrackingTime).Description("Location tracking time");
        }
    }
}
