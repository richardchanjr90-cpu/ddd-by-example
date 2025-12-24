using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Worker
{
    public class InviteViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("venueId")]
        public long VenueId { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("positionName")]
        public string PositionName { get; set; }
    }
}
