using GraphQL.Types;
using VehicleTracker.Business.GraphQL.DataProvider;
using VehicleTracker.Contracts.GraphQL;

namespace VehicleTracker.Business.GraphQL.Query
{
    public class RegistrationQuery : ObjectGraphType
    {
        public RegistrationQuery(RegistrationDataProvider provider)
        {
            Name = "Query";


            Field<RegistrationInfoType>("Registration",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "deviceId",
                        Description = "Vehicle unique deviceId"
                    }),
                resolve: ctx => provider.GetRegistration(ctx.GetArgument<string>("deviceId")));


            Field<ListGraphType<RegistrationInfoType>>("AllRegistration", resolve: ctx => provider.GetAllRegistration());
        }
    }
}
