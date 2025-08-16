using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class LocationViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }
    }
}