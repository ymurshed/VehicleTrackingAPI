using MediatR;
using VehicleTracker.Contracts.Models.HelperModels;

namespace VehicleTracker.Contracts.Queries
{
    public class GetUserTokenQuery : IRequest<string>
    {
        public User User { get; set; }

        public GetUserTokenQuery(string userName, string password)
        {
            User = new User
            {
                UserName = userName,
                Password = password
            };
        }
    }
}
