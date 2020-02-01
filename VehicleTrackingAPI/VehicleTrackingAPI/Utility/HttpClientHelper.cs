using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VehicleTrackingAPI.Models.HelperModels;

namespace VehicleTrackingAPI.Utility
{
    public class HttpClientHelper
    {
        public static async Task<GoogleApiResponse> GetResult(string requestUrl, Dictionary<string, string> httpHeader = null)
        {
            var result = GetAsync(requestUrl, httpHeader).Result;
            var jsonString = await result.Content.ReadAsStringAsync();
            var googleApiResponse = JsonConvert.DeserializeObject<GoogleApiResponse>(jsonString);
            return googleApiResponse;
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

        private static string GetDummyResponse()
        {
            const string json = "{\r\n   \"plus_code\" : {\r\n      \"compound_code\" : \"P27Q+MC New York, NY, USA\",\r\n      \"global_code\" : \"87G8P27Q+MC\"\r\n   },\r\n   \"results\" : [\r\n      {\r\n         \"address_components\" : [\r\n            {\r\n               \"long_name\" : \"279\",\r\n               \"short_name\" : \"279\",\r\n               \"types\" : [ \"street_number\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"Bedford Avenue\",\r\n               \"short_name\" : \"Bedford Ave\",\r\n               \"types\" : [ \"route\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"Williamsburg\",\r\n               \"short_name\" : \"Williamsburg\",\r\n               \"types\" : [ \"neighborhood\", \"political\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"Brooklyn\",\r\n               \"short_name\" : \"Brooklyn\",\r\n               \"types\" : [ \"political\", \"sublocality\", \"sublocality_level_1\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"Kings County\",\r\n               \"short_name\" : \"Kings County\",\r\n               \"types\" : [ \"administrative_area_level_2\", \"political\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"New York\",\r\n               \"short_name\" : \"NY\",\r\n               \"types\" : [ \"administrative_area_level_1\", \"political\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"United States\",\r\n               \"short_name\" : \"US\",\r\n               \"types\" : [ \"country\", \"political\" ]\r\n            },\r\n            {\r\n               \"long_name\" : \"11211\",\r\n               \"short_name\" : \"11211\",\r\n               \"types\" : [ \"postal_code\" ]\r\n            }\r\n         ],\r\n         \"formatted_address\" : \"279 Bedford Ave, Brooklyn, NY 11211, USA\",\r\n         \"geometry\" : {\r\n            \"location\" : {\r\n               \"lat\" : 40.7142484,\r\n               \"lng\" : -73.9614103\r\n            },\r\n            \"location_type\" : \"ROOFTOP\",\r\n            \"viewport\" : {\r\n               \"northeast\" : {\r\n                  \"lat\" : 40.71559738029149,\r\n                  \"lng\" : -73.9600613197085\r\n               },\r\n               \"southwest\" : {\r\n                  \"lat\" : 40.71289941970849,\r\n                  \"lng\" : -73.96275928029151\r\n               }\r\n            }\r\n         },\r\n         \"place_id\" : \"ChIJT2x8Q2BZwokRpBu2jUzX3dE\",\r\n         \"plus_code\" : {\r\n            \"compound_code\" : \"P27Q+MC Brooklyn, New York, United States\",\r\n            \"global_code\" : \"87G8P27Q+MC\"\r\n         },\r\n         \"types\" : [\r\n            \"bakery\",\r\n            \"cafe\",\r\n            \"establishment\",\r\n            \"food\",\r\n            \"point_of_interest\",\r\n            \"store\"\r\n         ]\r\n      },\r\n\t],\r\n   \"status\" : \"OK\"\r\n}\r\n";
            return json;
        }
    }
}
