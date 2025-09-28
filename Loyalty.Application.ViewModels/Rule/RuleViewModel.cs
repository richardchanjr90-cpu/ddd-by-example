using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Rule
{
    public class RuleViewModel
    {
        [JsonProperty("rules")]
        public List<SingleRuleViewModel> Rules { get; set;  }
    }
}
