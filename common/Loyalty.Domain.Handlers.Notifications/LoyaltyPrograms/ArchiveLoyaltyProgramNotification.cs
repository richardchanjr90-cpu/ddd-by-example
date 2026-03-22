using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramNotification : IIntegrationEventsNotification
    {       
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}