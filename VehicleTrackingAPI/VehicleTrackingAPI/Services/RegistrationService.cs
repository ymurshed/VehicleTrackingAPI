using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMongoCollection<RegistrationModel> _registrationModel;

        public RegistrationService(IVehicleTrackerDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);

            _registrationModel = database.GetCollection<RegistrationModel>(config.VehicleRegistrationCollectionName);
        }

        public async Task<RegistrationModel> Get(string deviceId)
        {
            return await _registrationModel.Find(registration => registration.VehicleDeviceId == deviceId).FirstOrDefaultAsync();
        }
            
        public async Task AddRegistrationAsync(RegistrationModel registrationModel)
        {
            await _registrationModel.InsertOneAsync(registrationModel);
        }
    }
}
