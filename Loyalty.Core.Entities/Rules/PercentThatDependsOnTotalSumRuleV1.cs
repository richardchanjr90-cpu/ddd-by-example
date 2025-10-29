using System.Collections.Generic;
using Loyalty.Common.Shared.Enums.Contracts;

namespace Loyalty.Core.Entities.Rules
{
    public class PercentThatDependsOnTotalSumRuleV1 : BaseRule
    {
        public List<PercentRange> Ranges { get; set; } = new List<PercentRange>();

        public override List<LoyaltyRuleType> CompatibleRules { get; set; } = new List<LoyaltyRuleType>();
    }

    public class PercentRange
    {
        public double Start { get; set; }

        public double End { get; set; }

        public double AmountThatShouldBeCollected { get; set; }
    }
}
