using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.LoyaltyProgram
{
    public class LoyaltyProgramViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [JsonProperty("startedDate")]
        public DateTime StartedDate { get; set; }

        [JsonProperty("endedDate")]
        public DateTime EndedDate { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("venueId")]
        public long VenueId { get; set; }
    }
}
