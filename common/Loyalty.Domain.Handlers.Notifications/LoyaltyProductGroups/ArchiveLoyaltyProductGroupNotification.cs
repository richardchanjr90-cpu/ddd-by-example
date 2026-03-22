using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductGroupNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}