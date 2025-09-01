using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("LoyaltyProductGroup", Schema = SchemaName.Loyalty)]
    public class LoyaltyProductGroup : AuditableEntity, IArchivableEntity, IRequireTwoStepSaveEntity
    {
        [ForeignKey(nameof(LoyaltyProgram))]
        public long LoyaltyProgramId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public virtual ICollection<LoyaltyProduct> LoyaltyProducts { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }
    }
}
