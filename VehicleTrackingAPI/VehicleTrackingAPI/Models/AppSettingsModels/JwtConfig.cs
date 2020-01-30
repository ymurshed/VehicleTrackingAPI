namespace VehicleTrackingAPI.Models.AppSettingsModels
{
    public class JwtConfig : IJwtConfig
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpireTimeInMins { get; set; }
    }
}
