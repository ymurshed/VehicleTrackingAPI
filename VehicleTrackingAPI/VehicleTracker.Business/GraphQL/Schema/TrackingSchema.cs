using GraphQL;
using VehicleTracker.Business.GraphQL.Query;

namespace VehicleTracker.Business.GraphQL.Schema
{
    public class TrackingSchema : global::GraphQL.Types.Schema
    {
        public TrackingSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<TrackingQuery>();
        }
    }
}
