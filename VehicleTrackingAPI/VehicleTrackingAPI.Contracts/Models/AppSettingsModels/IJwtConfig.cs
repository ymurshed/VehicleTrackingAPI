namespace VehicleTracker.Contracts.Models.AppSettingsModels
{
    public interface IJwtConfig
    {
        string SecretKey { get; set; }
        string Issuer { get; set; }
        int ExpireTimeInMins { get; set; }
    }
}
