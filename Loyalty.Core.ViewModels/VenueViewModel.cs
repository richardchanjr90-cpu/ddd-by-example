using System;
using System.Collections.Generic;
using Loyalty.Core.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class VenueViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public LocationViewModel Location { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("category")]
        public List<VenueCategoryViewModel> Categories { get; set; }

        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonProperty("details")]
        public VenueDetailsViewModel Details { get; set; }
    }
}


