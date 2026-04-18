using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Orders.Status;
using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders
{
    public class Order : TenantEntity, IAggregateRoot
    {
        private Order()
        {
            //for ef core
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new long Id { get; set; }

        public override long TenantId => VenueId;

        public virtual ICollection<OrderItem> OrderItems { get; private set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; private set; }

        public DateTime PlacedDate { get; private set; }

        public OrderStatusEnumeration Status { get; private set; }

        public DateTime? PickUpTime { get; private set; }

        public string Comment { get; private set; }

        public string VenueComment { get; private set; }

        public OrderVenueRate? Rate { get; private set; }

        public void Add(OrderItem item)
        {
            OrderItems ??= new List<OrderItem>();
            OrderItems.Add(item);
        }

        public void GiveRateToUser(OrderVenueRate rate, string venueComment)
        {
            if (rate == OrderVenueRate.Star && String.IsNullOrEmpty(venueComment))
            {
                throw new LoyaltyValidationException("When rate is = 1, comment is required.", ErrorCode.ORDER_INVALID_STATE);
            }

            if (Status.Id < FinishedOrder.Finished.Id)
            {
                throw new LoyaltyValidationException("Can't rate order, until Finished.", ErrorCode.ORDER_INVALID_STATE);
            }
            
            VenueComment = venueComment;
            Rate = rate;
        }

        public void UpdateStatus(OrderStatus status, string comment)
        {
            var newStatus = OrderStatusEnumeration.From((int)status);
            VenueComment = comment;
            newStatus.Set(this);

            if (String.IsNullOrEmpty(comment) && status == OrderStatus.DeclinedByVenue)
            {
                throw new LoyaltyValidationException("Comment required if declined by Venue", ErrorCode.ORDER_INVALID_STATE);
            }
        }

        internal void UpdateStatus(OrderStatusEnumeration newStatus)
        {
            Status = newStatus;
        }
    }
}
