using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.LoyaltyProductGroup
{
    public class LoyaltyProductGroupViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ruleType")]
        public int RuleType { get; set; }

        [JsonProperty("ruleValue")]
        public string RuleValue { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }
    }
}
