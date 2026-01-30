using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Rule;

namespace Loyalty.Application.ViewModels.LoyaltyProductGroup
{
    public class LoyaltyProductGroupGetViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("loyaltyProgramId")]
        public long LoyaltyProgramId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rules")]
        public RuleViewModel Rules { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("productGroup")]
        public ProductGroupViewModel ProductGroup { get; set; }
    }
}
