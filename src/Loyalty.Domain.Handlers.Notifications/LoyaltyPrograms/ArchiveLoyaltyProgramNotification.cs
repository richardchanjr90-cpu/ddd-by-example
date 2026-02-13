using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramNotification : INotification
    {       
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}