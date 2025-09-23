using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("ProductGroup", Schema = SchemaName.Loyalty)]
    public class ProductGroup : AuditableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
