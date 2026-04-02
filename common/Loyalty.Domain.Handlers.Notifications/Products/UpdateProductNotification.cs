using System;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class UpdateProductNotification : IIntegrationEventsNotification
    {
        public long Id { get; set; }

        public ProductGroupIconType GroupIcon { get; set; }
        
        public string GroupName { get; set; }

        public string ImageUri { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public string Name { get; set; }
    }
}
