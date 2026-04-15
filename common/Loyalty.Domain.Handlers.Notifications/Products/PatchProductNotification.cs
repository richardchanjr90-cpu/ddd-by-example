using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class PatchProductNotification: IIntegrationEventsNotification
    {
        [JsonPropertyName("isAvailableForOrder")]
        public bool IsAvailableForOrder { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
