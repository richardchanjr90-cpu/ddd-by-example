using System;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyClient.Domain.Handlers.Notifications.Orders
{
    public class PatchOrderNotification
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string UserId { get; set; }

        public OrderStatus Status { get; set; }

        public OrderStatus UpdatedStatus { get; set; }
    }
}
