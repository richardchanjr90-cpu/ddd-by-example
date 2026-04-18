using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Core.Outbox.Entities
{
    public class IntegrationEvent : INotification
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }
    }
}
