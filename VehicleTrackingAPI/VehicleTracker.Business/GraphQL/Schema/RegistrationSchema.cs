using GraphQL;
using VehicleTracker.Business.GraphQL.Query;

namespace VehicleTracker.Business.GraphQL.Schema
{
    public class RegistrationSchema : global::GraphQL.Types.Schema
    {
        public RegistrationSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RegistrationQuery>();
        }
    }
}
