using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Core.Entities.Rules
{
    public abstract class BaseRule
    {
        public abstract List<LoyaltyRuleType> CompatibleRules { get; set; }
    }
}
