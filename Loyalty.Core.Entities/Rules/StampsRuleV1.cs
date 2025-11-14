using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Rules
{
    public class StampsRuleV1 : BaseRule
    {
        public int StampsToCollect { get; set; }

        public int StampsToAssign { get; set; } = 0;

        public override List<LoyaltyRuleType> CompatibleRules { get; set; }
    }
}
