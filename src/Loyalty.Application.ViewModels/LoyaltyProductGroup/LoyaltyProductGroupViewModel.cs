using System.Text.Json.Serialization;
using Loyalty.Application.ViewModels.Rule;

namespace Loyalty.Application.ViewModels.LoyaltyProductGroup
{
    public class LoyaltyProductGroupViewModel
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

        [JsonPropertyName("productGroupId")]
        public long ProductGroupId { get; set; }
    }
}
