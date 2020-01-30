namespace VehicleTrackingAPI.Models.AppSettingsModels
{
    public interface IDummyAdminUser
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Role { get; set; }
    }
}
