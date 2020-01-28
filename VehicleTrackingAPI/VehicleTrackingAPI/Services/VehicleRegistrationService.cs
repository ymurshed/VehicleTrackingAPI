using System.Threading.Tasks;
using MongoDB.Driver;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.DbModels;

namespace VehicleTrackingAPI.Services
{
    public class VehicleRegistrationService : IVehicleRegistrationService
    {
        private readonly IMongoCollection<VehicleRegistrationModel> _registrationModel;

        public VehicleRegistrationService(IVehicleTrackerDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _registrationModel = database.GetCollection<VehicleRegistrationModel>(settings.VehicleRegistrationCollectionName);
        }

        public async Task<VehicleRegistrationModel> Get(string deviceId)
        {
            return await _registrationModel.Find(registration => registration.VehicleDeviceId == deviceId).FirstOrDefaultAsync();
        }
            
        public void CreateRegistrationAsync(VehicleRegistrationModel registrationModel)
        {
            _registrationModel.InsertOneAsync(registrationModel);
        }
    }
}
