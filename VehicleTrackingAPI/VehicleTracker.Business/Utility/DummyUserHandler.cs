using System;
using VehicleTracker.Contracts.Models.AppSettingsModels;
using VehicleTracker.Contracts.Models.HelperModels;

namespace VehicleTracker.Business.Utility
{
    public class DummyUserHandler
    {
        public static User SetRole(User user, IDummyAdminUser adminUser)
        {
            user.Role = adminUser.UserName.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase) && 
                        adminUser.Password.Equals(user.Password, StringComparison.CurrentCultureIgnoreCase) 
                        ? adminUser.Role
                        : Constants.OtherUserRole;
            return user;
        }
    }
}
