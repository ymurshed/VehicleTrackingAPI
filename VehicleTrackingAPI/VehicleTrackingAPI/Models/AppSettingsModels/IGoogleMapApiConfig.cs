namespace VehicleTrackingAPI.Models.AppSettingsModels
{
    public interface IGoogleMapApiConfig
    {
        string ApiKey { get; set; }
        string ApiUrl { get; set; }
    }
}
