using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities
{
    [Table("ProductGroup", Schema = SchemaName.Loyalty)]
    public class ProductGroup : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public Venue OwnerVenue { get; set; }

        public virtual ICollection<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public ProductGroupIconType Icon { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public bool IsArchived { get; set; }

        public override long TenantId => VenueId;
    }
}
