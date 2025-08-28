using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class VenueFullViewModel
    {
        [JsonProperty("venue")]
        public VenueViewModel Venue { get; set; }

        [JsonProperty("details")]
        public VenueDetailsViewModel Details { get; set; }
    }
}
