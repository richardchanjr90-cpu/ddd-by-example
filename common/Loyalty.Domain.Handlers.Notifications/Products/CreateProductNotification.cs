using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class CreateProductNotification : IIntegrationEventsNotification
    {
        public long Id { get; set; }

        public ProductGroupIconType GroupIcon { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
