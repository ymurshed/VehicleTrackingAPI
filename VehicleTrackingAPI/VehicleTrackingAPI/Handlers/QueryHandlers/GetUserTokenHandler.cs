using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Queries;
using VehicleTrackingAPI.Utility;

namespace VehicleTrackingAPI.Handlers.QueryHandlers
{
    public class GetUserTokenHandler : IRequestHandler<GetUserTokenQuery, string>
    {
        private readonly IJwtConfig _config;
        private readonly ILogger<GetRegistrationResponseByDeviceIdHandler> _logger;
        
        public GetUserTokenHandler(IJwtConfig config, ILogger<GetRegistrationResponseByDeviceIdHandler> logger)
        {
            _config = config;
            _logger = logger;
        }

        public Task<string> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
        {
            var tokenGenerator = new TokenGenerator(_config, DummyUserHandler.SetRole(request.User));
            _logger.LogInformation($"Token generted for user: {request.User.UserName}");
            return Task.FromResult(tokenGenerator.GetToken());
        }
    }
}
