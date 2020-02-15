using MediatR;
using VehicleTracker.Contracts.Models.ResponseModels;

namespace VehicleTracker.Contracts.Queries
{
    public class GetCurrentTrackingQuery : IRequest<TrackingResponse>
    {
        public string RegistrationId { get; set; }
        
        public GetCurrentTrackingQuery(string registrationId)
        {
            RegistrationId = registrationId;
        }
    }
}
