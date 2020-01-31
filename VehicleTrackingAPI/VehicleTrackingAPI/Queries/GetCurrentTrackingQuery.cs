using MediatR;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Queries
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
