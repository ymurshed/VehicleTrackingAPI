using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VehicleTrackingClient.Models;

namespace VehicleTrackingClient
{
    public class Client
    {
        public string Id { get; set; }

        public string Token { get; set; }
        
        public string RegistrationId { get; set; }
        public DateTime RandomStartTime { get; set; }
        public DateTime RandomEndTime { get; set; }

        public TrackingResponse TrackingResponse { get; set; }
        public List<TrackingResponse> TrackingResponseList { get; set; }
        
        public Client()
        {
            Id = Guid.NewGuid().ToString();
            Console.WriteLine($"--->>> Start invoking for task: {Id}");
        }

        public async Task<int> Invoke()
        {
            await InvokeRegistration();

            for (var i = 0; i < Constants.NoOfTrackingPerDevice; i++)
            {
                // Use this for get api call
                if (i == Constants.StartIndex) RandomStartTime = DateTime.Now;
                if (i == Constants.EndIndex) RandomEndTime = DateTime.Now;

                await InvokeTracking();
                await Task.Delay(Constants.Delay);
            }

            Console.WriteLine($"\nGetting token for task: {Id}. Token: {await GetToken()}");
            await GetLastTracking();
            await GetTrackingInCertainTime();
            return 1;
        }

        private async Task InvokeRegistration()
        {
            var id = new Random().Next(0, 999);
            var registrationCommand = new AddRegistrationCommand
            {
                VehicleDeviceId = $"vdi-{id}",
                VehicleModel = $"model-{id}"
            };

            var json = JsonConvert.SerializeObject(registrationCommand);
            var response = await HttpClientHelper.PostAsync(Constants.RegistrationApi, null, json);
            var result = response.Content.ReadAsStringAsync().Result;
            var registrationResponse = JsonConvert.DeserializeObject<RegistrationResponse>(result);
            
            // Set reg id
            RegistrationId = registrationResponse.RegistrationId;
            Console.WriteLine($"\nFor DeviceId {registrationResponse.VehicleDeviceId}, RegistrationId: {registrationResponse.RegistrationId}");
        }

        private async Task InvokeTracking()
        {
            var latitude = new Random().Next(-9999, 9999) * 1.1;
            var longitude = new Random().Next(-9999, 9999) * 1.1;
            
            var trackingCommand = new AddTrackingCommand
            {
                RegistrationId = RegistrationId,
                Latitude = latitude,
                Longitude = longitude
            };

            var json = JsonConvert.SerializeObject(trackingCommand);
            var response = await HttpClientHelper.PostAsync(Constants.TrackingApi, null, json);
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"For RegistrationId: {RegistrationId}, tracking is recorded.");
        }

        private async Task<string> GetToken()
        {
            var url = string.Format(Constants.GetTokenApi, Constants.AdminUser, Constants.AdminPassword);
            var response = await HttpClientHelper.GetAsync(url, null);
            Token = "Bearer " + JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            return Token;
        }

        private async Task GetLastTracking()
        {
            var url = string.Format(Constants.GetLastTrackingApi, RegistrationId);
            var authHeader = new Dictionary<string, string> { {"Authorization", Token} };

            var response = await HttpClientHelper.GetAsync(url, authHeader);
            var result = response.Content.ReadAsStringAsync().Result;
            TrackingResponse = JsonConvert.DeserializeObject<TrackingResponse>(result);

            Console.WriteLine($"\nFor RegistrationId: {RegistrationId}, Last tracking:");
            Print(TrackingResponse);
        }

        private async Task GetTrackingInCertainTime()
        {
            var url = string.Format(Constants.GetHistoryTrackingApi, RegistrationId, RandomStartTime, RandomEndTime);
            var authHeader = new Dictionary<string, string> { { "Authorization", Token } };

            var response = await HttpClientHelper.GetAsync(url, authHeader);
            var result = response.Content.ReadAsStringAsync().Result;
            TrackingResponseList = JsonConvert.DeserializeObject<List<TrackingResponse>>(result);

            Console.WriteLine($"\nFor RegistrationId: {RegistrationId}, tracking history within: {RandomStartTime.ToUniversalTime()} <---> {RandomEndTime.ToUniversalTime()}:");
            foreach (var trackingResponse in TrackingResponseList)
            {
                Print(trackingResponse);
            }
        }

        private static void Print(TrackingResponse trackingResponse)
        {
            Console.WriteLine($"At: {trackingResponse.TrackingTime}, Latitude: {trackingResponse.Latitude}, Longitude: {trackingResponse.Longitude}");
        }
    }
}
