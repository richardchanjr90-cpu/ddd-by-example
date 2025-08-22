using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
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
        public LoyaltyProductType Type { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public virtual ICollection<LoyaltyProduct> LoyaltyProducts { get; set; }

        public bool IsPublished { get; set; }
    }
}
