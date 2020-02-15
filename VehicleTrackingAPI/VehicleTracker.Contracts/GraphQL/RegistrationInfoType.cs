using GraphQL.Types;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Contracts.GraphQL
{
    public class RegistrationInfoType : ObjectGraphType<RegistrationInfo>
    {
        public RegistrationInfoType()
        {
            Name = "RegistrationInfo";
            Field(o => o.Id).Description("Primary key");
            Field(o => o.VehicleModel).Description("Vehicle model or type");
            Field(o => o.VehicleDeviceId).Description("Unique id of a vehicle");
            Field(o => o.RegistrationId).Description("Vehicle registration id");
            Field(o => o.RegistrationDate).Description("Vehicle registration date");
        }
    }
}
