using GraphQL.Types;
using MongoDB.Driver.GeoJsonObjectModel;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Contracts.GraphQL
{
    public class TrackingInfoType : ObjectGraphType<TrackingInfo>
    {
        public TrackingInfoType()
        {
            Name = "TrackingInfo";
            Field(o => o.Location, false, typeof(GeoJsonPoint<GeoJson2DGeographicCoordinates>)).Description("Geo location contains longitude & latitude");
            Field(o => o.TrackingTime).Description("Location tracking time");
        }
    }
}
