using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProduct", Schema = SchemaName.Loyalty)]
    public class LoyaltyProduct : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public bool IsArchived { get; set; }
    }
}
