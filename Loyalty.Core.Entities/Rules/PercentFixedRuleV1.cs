using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Core.Entities.Rules
{
    public class PercentFixedRuleV1 : BaseRule
    {
        public double FixedPercent { get; set; }

        public override List<LoyaltyRuleType> CompatibleRules { get; set; }
    }
}
