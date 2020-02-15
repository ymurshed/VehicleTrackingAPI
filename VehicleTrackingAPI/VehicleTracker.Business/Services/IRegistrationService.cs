using System.Threading.Tasks;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Business.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationInfo> Get(string deviceId);
        Task AddRegistrationAsync(RegistrationInfo registrationInfo);
    }
}
