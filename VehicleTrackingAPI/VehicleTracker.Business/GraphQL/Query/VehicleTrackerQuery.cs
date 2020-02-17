using System;
using GraphQL.Types;
using VehicleTracker.Business.GraphQL.DataProvider;
using VehicleTracker.Contracts.GraphQL;

namespace VehicleTracker.Business.GraphQL.Query
{
    public class VehicleTrackerQuery : ObjectGraphType
    {
        public VehicleTrackerQuery(RegistrationDataProvider registrationDataProvider, TrackingDataProvider trackingDataProvider)
        {
            Name = "Query";

            #region Registration
            Field<RegistrationInfoType>("Registration",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "deviceId",
                        Description = "Vehicle unique deviceId"
                    }),
                resolve: ctx => registrationDataProvider.GetRegistration(ctx.GetArgument<string>("deviceId")));


            Field<ListGraphType<RegistrationInfoType>>("AllRegistration", resolve: ctx => registrationDataProvider.GetAllRegistration());
            #endregion

            #region Tracking
            Field<TrackingInfoType>("CurrentTracking",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "registrationId",
                        Description = "Vehicle registrationId"
                    }),
                resolve: ctx => trackingDataProvider.GetTrackingInfo(ctx.GetArgument<string>("registrationId")));


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
                resolve: ctx => trackingDataProvider.GetTrackingInfoHistory(ctx.GetArgument<string>("registrationId"),
                    ctx.GetArgument<DateTime>("startTime"),
                    ctx.GetArgument<DateTime>("endTime")));


            Field<TrackingHistoryType>("TrackingHistoryDetails",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "registrationId",
                        Description = "Vehicle registrationId"
                    }),
                resolve: ctx => trackingDataProvider.GetTrackingHistoryDetails(ctx.GetArgument<string>("registrationId")));
            #endregion
        }
    }
}
