using System;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class CreateProductNotification : IIntegrationEventsNotification
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public ProductGroupIconType GroupIcon { get; set; }

        public string GroupName { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
