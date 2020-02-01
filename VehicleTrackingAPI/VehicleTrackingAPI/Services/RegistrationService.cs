using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMongoCollection<RegistrationInfo> _registrationInfo;

        public RegistrationService(IVehicleTrackerDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);

            _registrationInfo = database.GetCollection<RegistrationInfo>(config.VehicleRegistrationCollectionName);
        }

        public async Task<RegistrationInfo> Get(string deviceId)
        {
            return await _registrationInfo.Find(registration => registration.VehicleDeviceId == deviceId).FirstOrDefaultAsync();
        }
            
        public async Task AddRegistrationAsync(RegistrationInfo registrationInfo)
        {
            await _registrationInfo.InsertOneAsync(registrationInfo);
        }
    }
}
