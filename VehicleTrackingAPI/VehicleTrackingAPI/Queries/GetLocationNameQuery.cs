using MediatR;
using VehicleTrackingAPI.Models.ResponseModels;

namespace VehicleTrackingAPI.Queries
{
    public class GetLocationNameQuery : IRequest<MapApiResponse>
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
