using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class LoyaltyProgramViewModel
    {
        [JsonProperty("id")]
        public Guid ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("startedDate")]
        public DateTime StartedDate { get; set; }

        [JsonProperty("endedDate")]
        public DateTime EndedDate { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("cardBecomesInactiveAfterEnd")]
        public bool CardBecomesInactiveAfterEnd { get; set; }

        [JsonProperty("venueId")]
        public int VenueId { get; set; }
    }
}
