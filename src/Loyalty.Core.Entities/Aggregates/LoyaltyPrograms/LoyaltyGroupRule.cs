using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.LoyaltyPrograms
{
    public class LoyaltyGroupRule : TenantEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        public LoyaltyProductGroup LoyaltyProductGroup { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string Rule { get; set; }

        public LoyaltyRuleVersion RuleVersion { get; set; }

        public bool IsArchived { get; set; }

        public override long TenantId => LoyaltyProductGroup.TenantId;
    }
}
