using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Venue
{
    public class VenueDetailsViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fullDescription")]
        public string FullDescription { get; set; }

        [JsonProperty("phones")]
        public List<string> Phones { get; set; }

        [JsonProperty("webSites")]
        public List<string> WebSites { get; set; }

        [JsonProperty("workingHours")]
        public List<string> WorkingHours { get; set; }
    }
}
