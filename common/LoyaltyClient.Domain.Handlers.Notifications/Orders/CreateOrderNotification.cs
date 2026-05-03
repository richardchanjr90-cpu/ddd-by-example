using System;
using System.Collections.Generic;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyClient.Domain.Handlers.Notifications.Base;

namespace LoyaltyClient.Domain.Handlers.Notifications.Orders
{
    public class CreateOrderNotification : IClientVenueNotification
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public DateTime PlacedDate { get; set; }

        public string UserId { get; set; }

        public float UserRating { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime? PickUpTime { get; set; }

        public decimal? TotalPrice { get; set; }

        public string Comment { get; set; }

        public List<CreateOrderItemNotification> OrderItems { get; set; } = new List<CreateOrderItemNotification>();
    }
}
