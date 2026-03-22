using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups
{
    public class CreateLoyaltyProductGroupNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("loyaltyProgramId")]
        public long LoyaltyProgramId { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("rule")]
        public string Rule { get; set; }
    }
}