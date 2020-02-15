using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Business.GraphQL.DataProvider
{
    public class RegistrationDataProvider
    {
        private static IRegistrationService _registrationService;

        public RegistrationDataProvider(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public Task<RegistrationInfo> GetRegistration(string deviceId)
        {
            return Task.Factory.StartNew(() => _registrationService.Get(deviceId).Result);
        }

        public Task<List<RegistrationInfo>> GetAllRegistration()
        {
            return Task.Factory.StartNew(() => _registrationService.GetAll().Result);
        }
    }
}
