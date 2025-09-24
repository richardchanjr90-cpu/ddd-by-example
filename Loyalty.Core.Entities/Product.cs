using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("Product", Schema = SchemaName.Loyalty)]
    public class Product : AuditableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }
    }
}
