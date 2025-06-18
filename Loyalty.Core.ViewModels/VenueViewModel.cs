using System;
using Loyalty.Core.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class VenueViewModel
    {
        [JsonProperty("id")]
        public Guid ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public GeoPositionViewModel Location { get; set; }

        [JsonProperty("parentId")]
        public Guid? ParentId { get; set; }

        [JsonProperty("type")]
        public VenueType Type { get; set; }

        [JsonProperty("category")]
        public VenueCategory Category { get; set; }
    }
}
