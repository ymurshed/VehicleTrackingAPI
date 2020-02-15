using System.Threading.Tasks;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.Models.AppSettingsModels;
using VehicleTracker.Contracts.Models.DbModels;

namespace VehicleTracker.Business.GraphQL.DataProvider
{
    public class RegistrationDataProvider
    {
        private static readonly object LockObject = new object();
        private static RegistrationDataProvider _instance;

        private static IRegistrationService _registrationService;

        public static RegistrationDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null) _instance = new RegistrationDataProvider();
                    }
                }
                return _instance;
            }
            set => _instance = value;
        }

        public RegistrationDataProvider()
        {
            // Todo: handle DI
            _registrationService = new RegistrationService(new VehicleTrackerDbConfig());
        }

        public Task<RegistrationInfo> GetRegistration(string deviceId)
        {
            return Task.Factory.StartNew(() => _registrationService.Get(deviceId).Result);
        }
    }
}
