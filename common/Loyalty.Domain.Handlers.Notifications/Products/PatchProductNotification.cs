using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class PatchProductNotification: IIntegrationEventsNotification
    {
        public bool IsAvailableForOrder { get; set; }

        public long Id { get; set; }
    }
}
