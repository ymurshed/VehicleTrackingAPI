using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Models.ResponseModels;
using VehicleTrackingAPI.Queries;
using VehicleTrackingAPI.Utility;

namespace VehicleTrackingAPI.Handlers.QueryHandlers
{
    public class GetLocationNameHandler : IRequestHandler<GetLocationNameQuery, MapApiResponse>
    {
        private readonly ILogger<GetLocationNameHandler> _logger;
        private readonly IGoogleMapApiConfig _googleMapApiConfig;
        
        public GetLocationNameHandler(ILogger<GetLocationNameHandler> logger, IGoogleMapApiConfig googleMapApiConfig)
        {
            _logger = logger;
            _googleMapApiConfig = googleMapApiConfig;
        }

        public Task<MapApiResponse> Handle(GetLocationNameQuery request, CancellationToken cancellationToken)
        {
            var latlngValue = $"{request.Latitude},{request.Longitude}";
            var requestUrl = string.Format(_googleMapApiConfig.ApiUrl, latlngValue, _googleMapApiConfig.ApiKey);
            var response = HttpClientHelper.GetResult(requestUrl).Result;

            var mapApiResponse = new MapApiResponse {Status = response[Constants.MapStatus].ToString().ToUpper()};

            if (mapApiResponse.Status.Equals(Constants.OkStatus))
            {
                mapApiResponse.Location = "";
                _logger.LogInformation($"For Latitude, Longitude: {latlngValue}, Location name: {mapApiResponse.Location}.");
            }
            else
            {
                mapApiResponse.Error = response[Constants.MapError].ToString();
                _logger.LogError($"Error getting location name. Error details: {mapApiResponse.Error}.");
            }
            return Task.FromResult(mapApiResponse);
        }
    }
}
