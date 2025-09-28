using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("ProductGroup", Schema = SchemaName.Loyalty)]
    public class ProductGroup : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long? LoyaltyProductGroupId { get; set; }

        [Required]
        public string Icon { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public bool IsArchived { get; set; }
    }
}
