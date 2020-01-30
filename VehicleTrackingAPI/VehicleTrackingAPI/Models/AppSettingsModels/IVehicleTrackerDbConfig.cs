namespace VehicleTrackingAPI.Models.AppSettingsModels
{
    public interface IVehicleTrackerDbConfig
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string VehicleRegistrationCollectionName { get; set; }
        string VehicleTrackingCollectionName { get; set; }
    }
}
