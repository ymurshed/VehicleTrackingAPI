namespace VehicleTrackingAPI.Models.AppSettingsModels
{
    public class GoogleMapApiConfig : IGoogleMapApiConfig
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }
}
