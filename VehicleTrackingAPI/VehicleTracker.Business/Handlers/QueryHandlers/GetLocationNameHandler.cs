using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTracker.Business.Utility;
using VehicleTracker.Contracts.Models.AppSettingsModels;
using VehicleTracker.Contracts.Models.ResponseModels;
using VehicleTracker.Contracts.Queries;

namespace VehicleTracker.Business.Handlers.QueryHandlers
{
    public class GetLocationNameHandler : IRequestHandler<GetLocationNameQuery, LocationResponse>
    {
        private readonly ILogger<GetLocationNameHandler> _logger;
        private readonly IGoogleMapApiConfig _googleMapApiConfig;
        
        public GetLocationNameHandler(ILogger<GetLocationNameHandler> logger, IGoogleMapApiConfig googleMapApiConfig)
        {
            _logger = logger;
            _googleMapApiConfig = googleMapApiConfig;
        }

        public Task<LocationResponse> Handle(GetLocationNameQuery request, CancellationToken cancellationToken)
        {
            var latlngValue = $"{request.Latitude},{request.Longitude}";
            var requestUrl = string.Format(_googleMapApiConfig.ApiUrl, latlngValue, _googleMapApiConfig.ApiKey);
            var googleApiResponse = HttpClientHelper.GetResult(requestUrl).Result;

            var locationResponse = new LocationResponse { Status = googleApiResponse.Status };

            if (locationResponse.Status.Equals(Constants.OkStatus))
            {
                locationResponse.Location = googleApiResponse.Results.FirstOrDefault()?.formatted_address;
                _logger.LogInformation($"For Latitude, Longitude: {latlngValue}, Location name: {locationResponse.Location}.");
            }
            else
            {
                locationResponse.Error = googleApiResponse.error_message;
                _logger.LogError($"Error getting location name. Error details: {locationResponse.Error}.");
            }
            return Task.FromResult(locationResponse);
        }
    }
}
