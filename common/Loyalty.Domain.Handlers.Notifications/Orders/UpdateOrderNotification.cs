using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Orders
{
    public class UpdateOrderNotification : IIntegrationEventsNotification
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
