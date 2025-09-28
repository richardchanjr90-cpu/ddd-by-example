using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("Product", Schema = SchemaName.Loyalty)]
    public class Product : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public Venue OwnerVenue { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public bool IsArchived { get; set; }

        public long? ProductGroupId { get; set; }

        public ProductGroup ProductGroup { get; set; }
    }
}
