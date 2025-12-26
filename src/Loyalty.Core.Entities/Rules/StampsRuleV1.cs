using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Shared.Contracts.Enums;
using Newtonsoft.Json;

namespace Loyalty.Core.Entities.Rules
{
    public class StampsRuleV1 : BaseRule
    {
        [JsonProperty("stampsToCollectles")]
        public int StampsToCollect { get; set; }

        [JsonProperty("stampsToAssign")]
        public int StampsToAssign { get; set; } = 0;

        public override List<LoyaltyRuleType> CompatibleRules { get; set; }
    }
}
