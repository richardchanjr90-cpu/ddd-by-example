using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Rule;
using Loyalty.Common.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.LoyaltyProductGroup
{
    public class LoyaltyProductGroupViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("loyaltyProgramId")]
        public string LoyaltyProgramId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rule")]
        public RuleViewModel Rule { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("productGroup")]
        public ProductGroupViewModel ProductGroup { get; set; }
    }
}
