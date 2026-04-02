using System;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class ArchiveProductNotification : IIntegrationEventsNotification
    {
        public long Id { get; set; }

        public bool IsArchived { get; set; }
    }
}
