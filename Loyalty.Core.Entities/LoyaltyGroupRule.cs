using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("LoyaltyGroupRule", Schema = SchemaName.Loyalty)]
    public class LoyaltyGroupRule : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string Rule { get; set; }

        public string RuleVersion { get; set; }

        public bool IsArchived { get; set; }

        public LoyaltyGroupRule ParseRule(string rule, LoyaltyRuleType type)
        {
            throw new NotImplementedException();
        }
    }
}
