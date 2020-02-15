using MediatR;
using VehicleTracker.Contracts.Models.ResponseModels;

namespace VehicleTracker.Contracts.Queries
{
    public class GetLocationNameQuery : IRequest<LocationResponse>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public GetLocationNameQuery(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
