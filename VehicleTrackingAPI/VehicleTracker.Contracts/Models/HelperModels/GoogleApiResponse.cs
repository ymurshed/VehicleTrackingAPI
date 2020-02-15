using System.Collections.Generic;

namespace VehicleTracker.Contracts.Models.HelperModels
{
    public class GoogleApiResponse
    {
        public string error_message { get; set; }
        public List<Results> Results { get; set; }
        public string Status { get; set; }
    }

    public class Results
    {
        public string formatted_address { get; set; }
    }
}
