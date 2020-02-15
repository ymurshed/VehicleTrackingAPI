namespace VehicleTracker.Contracts.Models.AppSettingsModels
{
    public class DummyAdminUser : IDummyAdminUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
