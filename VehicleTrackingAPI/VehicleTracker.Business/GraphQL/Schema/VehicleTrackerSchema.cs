using GraphQL;
using VehicleTracker.Business.GraphQL.Query;

namespace VehicleTracker.Business.GraphQL.Schema
{
    public class VehicleTrackerSchema : global::GraphQL.Types.Schema
    {
        public VehicleTrackerSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<VehicleTrackerQuery>();
        }
    }
}
