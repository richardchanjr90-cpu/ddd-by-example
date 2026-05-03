using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Orders
{
    public class PatchOrderStatusViewModel
    {
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("venueComment")]
        public string VenueComment { get; set; }
    }
}
