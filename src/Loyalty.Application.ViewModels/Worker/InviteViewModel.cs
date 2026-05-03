using System;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Worker
{
    public class InviteViewModel
    {
        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("role")]
        public int Role { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }
    }
}
