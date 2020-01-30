using System;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.HelperModels;

namespace VehicleTrackingAPI.Utility
{
    public class DummyUserHandler
    {
        public static User SetRole(User user, IDummyAdminUser adminUser)
        {
            user.Role = adminUser.UserName.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase) && 
                        adminUser.Password.Equals(user.Password, StringComparison.CurrentCultureIgnoreCase) 
                        ? adminUser.Role
                        : Constants.DummyGeneralUserRole;
            return user;
        }
    }
}
