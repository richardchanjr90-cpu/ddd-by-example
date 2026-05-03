using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class PatchProductImageNotification : IIntegrationEventsNotification
    {   
        [JsonPropertyName("imageUri")]
        public Uri ImageUri { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
