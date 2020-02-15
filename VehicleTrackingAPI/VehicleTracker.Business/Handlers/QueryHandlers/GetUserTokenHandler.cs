using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTracker.Business.Utility;
using VehicleTracker.Contracts.Models.AppSettingsModels;
using VehicleTracker.Contracts.Queries;

namespace VehicleTracker.Business.Handlers.QueryHandlers
{
    public class GetUserTokenHandler : IRequestHandler<GetUserTokenQuery, string>
    {
        private readonly IJwtConfig _config;
        private readonly IDummyAdminUser _adminUser;
        private readonly ILogger<GetRegistrationResponseByDeviceIdHandler> _logger;
        
        public GetUserTokenHandler(IJwtConfig config, IDummyAdminUser adminUser, 
                                   ILogger<GetRegistrationResponseByDeviceIdHandler> logger)
        {
            _config = config;
            _adminUser = adminUser;
            _logger = logger;
        }

        public Task<string> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
        {
            var tokenGenerator = new TokenGenerator(_config, DummyUserHandler.SetRole(request.User, _adminUser));
            _logger.LogInformation($"Token generated for user: {request.User.UserName}");
            return Task.FromResult(tokenGenerator.GetToken());
        }
    }
}
