namespace VehicleTracker.Business.Utility
{
    public class Constants
    {
        // Swagger title
        public const string SwaggerTitle = "VehicleTracker";

        // Jwt & User configs
        public const string SecretKey = "JwtConfig:SecretKey";
        public const string Issuer = "JwtConfig:Issuer";
        public const string ExpireTimeInMins = "JwtConfig:ExpireTimeInMins";

        public const string AdminUserPolicy = "RequireAdminAccess";
        public const string OtherUserPolicy = "RequireOtherAccess";

        public const string AdminUserRole = "admin-user";
        public const string OtherUserRole = "other-user";

        public const string Unauthorized = "Unauthorized. ";
        public const string UnauthorizedError = "Unauthorized user. ";
        
        // Others
        public const string StopwatchKey = "StopWatch";
        public const string OkStatus = "OK";
    }
}
