using System.Threading.Tasks;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationModel> Get(string deviceId);
        Task AddRegistrationAsync(RegistrationModel registrationModel);
    }
}
