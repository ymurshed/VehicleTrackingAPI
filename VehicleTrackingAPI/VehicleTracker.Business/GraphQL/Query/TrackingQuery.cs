using System;
using GraphQL.Types;
using VehicleTracker.Business.GraphQL.DataProvider;
using VehicleTracker.Contracts.GraphQL;

namespace VehicleTracker.Business.GraphQL.Query
{
    public class TrackingQuery : ObjectGraphType
    {
        public TrackingQuery(TrackingDataProvider provider)
        {
            Name = "Query";


            Field<TrackingInfoType>("CurrentTracking",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "registrationId",
                        Description = "Vehicle registrationId"
                    }),
                resolve: ctx => provider.GetTrackingInfo(ctx.GetArgument<string>("registrationId")));


            Field<ListGraphType<TrackingInfoType>>("TrackingHistory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "registrationId",
                        Description = "Vehicle registrationId"
                    },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>>
                    {
                        Name = "startTime",
                        Description = "Start time"
                    },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>>
                    {
                        Name = "endTime",
                        Description = "End time"
                    }),
                resolve: ctx => provider.GetTrackingInfoHistory(ctx.GetArgument<string>("registrationId"), 
                                ctx.GetArgument<DateTime>("startTime"), 
                                ctx.GetArgument<DateTime>("endTime")));
        }
    }
}
