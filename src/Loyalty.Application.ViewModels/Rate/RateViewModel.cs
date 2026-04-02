using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Rate
{
    public class RateViewModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }
}
