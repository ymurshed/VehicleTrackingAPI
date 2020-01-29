using System.Threading.Tasks;
using VehicleTrackingAPI.Models.DbModels;
using VehicleTrackingAPI.Models.DbModels.SubModels;

namespace VehicleTrackingAPI.Services
{
    public interface ITrackingService
    {
        Task AddTrackingAsync(TrackingModel trackingModel);
        Task AddGeoPointModelAsync(string registrationId, GeoPointModel geoPointModel);
    }
}
