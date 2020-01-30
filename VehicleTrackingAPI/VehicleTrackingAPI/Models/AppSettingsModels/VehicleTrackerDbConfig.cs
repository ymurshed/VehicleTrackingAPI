namespace VehicleTrackingAPI.Models.AppSettingsModels
{
    public class VehicleTrackerDbConfig : IVehicleTrackerDbConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string VehicleRegistrationCollectionName { get; set; }
        public string VehicleTrackingCollectionName { get; set; }
    }
}
