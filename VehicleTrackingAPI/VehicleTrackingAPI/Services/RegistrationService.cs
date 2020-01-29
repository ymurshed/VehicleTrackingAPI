using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMongoCollection<RegistrationModel> _registrationModel;

        public RegistrationService(IVehicleTrackerDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _registrationModel = database.GetCollection<RegistrationModel>(settings.VehicleRegistrationCollectionName);
        }

        public async Task<RegistrationModel> Get(string deviceId)
        {
            return await _registrationModel.Find(registration => registration.VehicleDeviceId == deviceId).FirstOrDefaultAsync();
        }
            
        public bool CreateRegistrationAsync(RegistrationModel registrationModel)
        {
            var result = _registrationModel.InsertOneAsync(registrationModel);
            return result.IsCompletedSuccessfully;
        }
    }
}
