using System;
using VehicleTrackingAPI.Models.HelperModels;

namespace VehicleTrackingAPI.Utility
{
    public class DummyUserHandler
    {
        public static User SetRole(User user)
        {
            user.Role = Constants.DummyAdminUserName.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase) && 
                        Constants.DummyAdminUserPass.Equals(user.Password, StringComparison.CurrentCultureIgnoreCase) 
                        ? Constants.DummyAdminRole
                        : Constants.DummyGeneralUserRole;
            return user;
        }
    }
}
