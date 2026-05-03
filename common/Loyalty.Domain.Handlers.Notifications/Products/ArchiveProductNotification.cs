using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class ArchiveProductNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("isArchived")]
        public bool IsArchived { get; set; }
    }
}
