using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("LoyaltyProductGroup", Schema = SchemaName.Loyalty)]
    public class LoyaltyProductGroup : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProgram))]
        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public LoyaltyGroupRule Rule { get; set; }

        public ProductGroup ProductGroup { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public bool IsArchived { get; set; }
    }
}
