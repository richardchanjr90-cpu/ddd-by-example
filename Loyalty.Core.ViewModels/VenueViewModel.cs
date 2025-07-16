using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class VenueViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public GeoPositionViewModel Location { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("category")]
        public int Category { get; set; }
    }
}
