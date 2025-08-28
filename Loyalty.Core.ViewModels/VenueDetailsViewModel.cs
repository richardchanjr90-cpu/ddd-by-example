using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class VenueDetailsViewModel
    {
        [JsonProperty("venueId")]
        public long VenueId { get; set; }

        [JsonProperty("fullDescription")]
        public string FullDescription { get; set; }

        [JsonProperty("phones")]
        public List<string> Phones { get; set; }

        [JsonProperty("webSites")]
        public List<string> WebSites { get; set; }

        [JsonProperty("workingHours")]
        public string WorkingHours { get; set; }

        [JsonProperty("photosUrl")]
        public List<string> PhotosUrl { get; set; }
    }
}
