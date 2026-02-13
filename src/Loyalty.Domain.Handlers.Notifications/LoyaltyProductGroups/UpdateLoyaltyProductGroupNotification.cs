using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups
{
    public class UpdateLoyaltyProductGroupNotification : INotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("rule")]
        public string Rule { get; set; }

        [JsonPropertyName("ruleType")]
        public int RuleType { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }
}