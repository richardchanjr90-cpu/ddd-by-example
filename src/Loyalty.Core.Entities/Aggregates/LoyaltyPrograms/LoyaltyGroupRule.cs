using System.Collections.Generic;
using System.Text.Json;
using Loyalty.Core.Entities.SeedWork;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.LoyaltyPrograms
{
    public class LoyaltyGroupRule : ValueObject
    {
        public LoyaltyGroupRule(
              LoyaltyRuleType ruleType,
              object rule,
              LoyaltyRuleVersion ruleVersion)
        {
            RuleType = ruleType;
            Rule = JsonSerializer.Serialize(rule);
            RuleVersion = ruleVersion;
        }

        private LoyaltyGroupRule()
        {
        }

        public long Id { get; set; }

        public long LoyaltyProductGroupId { get; private set; }

        public LoyaltyProductGroup LoyaltyProductGroup { get; private set; }

        public LoyaltyRuleType RuleType { get; private set; }

        public string Rule { get; private set; }

        public LoyaltyRuleVersion RuleVersion { get; private set; }

        public long TenantId => LoyaltyProductGroup.TenantId;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return LoyaltyProductGroupId;
            yield return RuleType;
            yield return Rule;
            yield return RuleVersion;
        }
    }
}
