using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities.Orders
{
    [Table("VenueMenu", Schema = SchemaName.Loyalty)]
    public class VenueMenu : AuditableEntity
    {
        public override long TenantId => VenueId;

        public virtual ICollection<VenueMenuProductGroup> ProductGroups { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public Venue Venue { get; set; }

        public DateTime? Date { get; set; }
    }
}
