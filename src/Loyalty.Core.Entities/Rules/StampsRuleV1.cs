using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Rules
{
    public class StampsRuleV1 : BaseRule
    {
        [JsonPropertyName("stampsToCollectles")]
        public int StampsToCollect { get; set; }

        [JsonPropertyName("stampsToAssign")]
        public int StampsToAssign { get; set; } = 0;

        public override List<LoyaltyRuleType> CompatibleRules { get; set; }
    }
}
