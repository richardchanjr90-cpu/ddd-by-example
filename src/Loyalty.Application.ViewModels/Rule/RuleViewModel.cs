using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Rule
{
    public class RuleViewModel
    {
        [JsonPropertyName("rules")]
        public List<SingleRuleViewModel> Rules { get; set;  }
    }
}
