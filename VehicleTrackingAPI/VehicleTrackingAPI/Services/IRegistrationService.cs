using System.Threading.Tasks;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationInfo> Get(string deviceId);
        Task AddRegistrationAsync(RegistrationInfo registrationInfo);
    }
}
