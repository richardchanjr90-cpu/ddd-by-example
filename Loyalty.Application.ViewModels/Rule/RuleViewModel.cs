using System.Collections.Generic;
using Loyalty.Common.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Rule
{
    public class RuleViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("combinedRuleType")]
        public LoyaltyRuleType CombinedRuleType { get; set; }

        [JsonProperty("rules")]
        public List<SingleRuleViewModel> Rules { get; set;  }
    }
}
