using System;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyClient.Domain.Handlers.Notifications.Base;

namespace LoyaltyClient.Domain.Handlers.Notifications.Orders
{
    public class PatchOrderNotification : IClientVenueNotification
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }

        public OrderStatus Status { get; set; }

        public OrderStatus UpdatedStatus { get; set; }
    }
}
