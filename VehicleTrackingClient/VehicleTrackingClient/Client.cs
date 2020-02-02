using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VehicleTrackingClient.Models;

namespace VehicleTrackingClient
{
    public class Client
    {
        public int Id { get; set; }
        public string RegistrationId { get; set; }

        public Client(int i)
        {
            Id = i;
            Console.WriteLine($"--->>> Start invoking for task: {Id}");
        }

        ~Client()
        {
            Console.WriteLine($"<<<--- End invoking for task: {Id}");
        }

        public async void Invoke()
        {
            await InvokeRegistration();

            for (var i = 0; i < Constants.NoOfTrackingPerDevice; i++)
            {
                await InvokeTracking();
                Thread.Sleep(Constants.Delay);
            }
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
            Console.WriteLine($"For DeviceId {registrationResponse.VehicleDeviceId}, RegistrationId: {registrationResponse.RegistrationId}");
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
            Console.WriteLine($"For RegistrationId: {RegistrationId}, tracking response is: {result}");
        } 
    }
}
