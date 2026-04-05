using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Orders
{
    [Table("Order", Schema = SchemaName.Loyalty)]
    public class Order : AuditableEntity
    {
        public Order(
            long id,
            long venueId,
            DateTime placedDate,
            OrderStatus status,
            IList<OrderItem> orderItems,
            DateTime? pickUpTime = null,
            string comment = null)
        {
            Id = id;
            VenueId = venueId;
            PlacedDate = placedDate;
            Status = status;
            PickUpTime = pickUpTime;
            Comment = comment;
            OrderItems = orderItems;
        }

        private Order()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new long Id { get; set; }

        public override long TenantId => VenueId;

        public virtual ICollection<OrderItem> OrderItems { get; private set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; private set; }

        public DateTime PlacedDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public DateTime? PickUpTime { get; private set; }

        public string Comment { get; private set; }

        public void Add(OrderItem item)
        {
            OrderItems ??= new List<OrderItem>();
            OrderItems.Add(item);
        }

        public void AddStatus(OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.DeclinedByCustomer || newStatus == OrderStatus.Placed)
            {
                throw new LoyaltyValidationException("Invalid status", ErrorCode.ORDER_INVALID_STATE);
            }

            Status = newStatus;
        }
    }
}
