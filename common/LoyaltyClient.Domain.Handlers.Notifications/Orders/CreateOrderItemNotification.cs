using System;
using LoyaltyClient.Domain.Handlers.Notifications.Base;

namespace LoyaltyClient.Domain.Handlers.Notifications.Orders
{
    public class CreateOrderItemNotification : IClientVenueNotification
    {
        public int Amount { get; set; }

        public long ProductId { get; set; }

        public long OrderId { get; set; }

        public long Id { get; set; }
    }
}
