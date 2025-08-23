using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProgramRule", Schema = SchemaName.Loyalty)]
    public class LoyaltyProgramRule : AuditableEntity, IArchivableEntity
    {
        public LoyaltyRuleType RuleType { get; set; }

        [ForeignKey(nameof(LoyaltyProgram))]
        public LoyaltyProgram LoaLoyaltyProgramId { get; set; }

        public string RuleValue { get; set; }

        public bool IsArchived { get; set; }
    }
}
