using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VehicleTrackingAPI.Utility
{
    public class HttpClientHelper
    {
        public static async Task<JObject> GetResult(string requestUrl, Dictionary<string, string> httpHeader = null)
        {
            var result = GetAsync(requestUrl, httpHeader).Result;
            var jsonString = await result.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }

        private static async Task<HttpResponseMessage> GetAsync(string requestUrl, IDictionary<string, string> headers)
        {
            using (var client = PrepareHttpClient(headers))
            {
                try
                {
                    return await client.GetAsync(requestUrl);
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(e.Message),
                        ReasonPhrase = e.Message
                    };
                }
            }
        }

        private static HttpClient PrepareHttpClient(IDictionary<string, string> headers)
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = false
            };

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            client.DefaultRequestHeaders.ExpectContinue = false;

            if (headers == null || !headers.Any()) return client;

            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            return client;
        }
    }
}
