namespace VehicleTrackingClient
{
    public class Constants
    {
        public const string RegistrationApi = "http://localhost:5000/api/Registration";
        public const string TrackingApi = "http://localhost:5000/api/Tracking";

        public const string GetTokenApi = "http://localhost:5000/api/Token?userName={0}&password={1}";

        public const string GetLastTrackingApi = "http://localhost:5000/api/Tracking/CurrentTracking?registrationId={0}";
        public const string GetHistoryTrackingApi = "http://localhost:5000/api/Tracking/TrackingsInCertainTime?registrationId={0}&startTime={1}&endTime={2}";

        public const int NoOfDevice = 2;
        public const int NoOfTrackingPerDevice = 3;
        public const int Delay = 1000 * 5;

        // To get random time
        public const int StartIndex = 1;
        public const int EndIndex = 2;

        public const string AdminUser = "admin";
        public const string AdminPassword = "admin@123";
    }
}
