using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Orders
{
    [Table("Order", Schema = SchemaName.Loyalty)]
    public class Order : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id { get; set; }

        public override long TenantId => VenueId;

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public DateTime PlacedDate { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime? PickUpTime { get; set; }

        public string Comment { get; set; }
    }
}
