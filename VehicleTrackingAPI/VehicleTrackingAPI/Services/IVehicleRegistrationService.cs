using System.Threading.Tasks;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public interface IVehicleRegistrationService
    {
        Task<VehicleRegistrationModel> Get(string deviceId);
        void CreateRegistrationAsync(VehicleRegistrationModel registrationModel);
    }
}
