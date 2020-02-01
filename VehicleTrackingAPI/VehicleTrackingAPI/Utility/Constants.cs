namespace VehicleTrackingAPI.Utility
{
    public class Constants
    {
        public const string SwaggerTitle = "VehicleTrackingAPI";

        public const string SecretKey = "JwtConfig:SecretKey";
        public const string Issuer = "JwtConfig:Issuer";
        public const string ExpireTimeInMins = "JwtConfig:ExpireTimeInMins";

        public const string AdminUserPolicy = "RequireAdminAccess";
        public const string OtherUserPolicy = "RequireOtherAccess";

        public const string AdminUserRole = "admin-user";
        public const string OtherUserRole = "other-user";

        public const string Unauthorized = "Unauthorized. ";
        public const string UnauthorizedError = "Unauthorized user. ";

    }
}
