using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Rule;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.LoyaltyProductGroup
{
    public class LoyaltyProductGroupViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("loyaltyProgramId")]
        public long LoyaltyProgramId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rules")]
        public RuleViewModel Rules { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("productGroupId")]
        public long ProductGroupId { get; set; }
    }
}
