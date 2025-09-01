using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class VenueFullViewModel
    {
        [JsonProperty("venue")]
        public VenueViewModel Venue { get; set; }

        [JsonProperty("details")]
        public VenueDetailsViewModel Details { get; set; }
    }
}
