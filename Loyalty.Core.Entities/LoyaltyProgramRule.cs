using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("LoyaltyProgramRule", Schema = SchemaName.Loyalty)]
    public class LoyaltyProgramRule : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string RuleValue { get; set; }

        public bool IsArchived { get; set; }
    }
}
