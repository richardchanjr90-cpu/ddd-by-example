using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using Loyalty.Core.Outbox.Entities.Enums;
using MediatR;

namespace Loyalty.Core.Outbox.Entities
{
    public class IntegrationEventLogEntry
    {
        public IntegrationEventLogEntry(INotification integrationEvent, Guid transactionId)
        {
            EventId = Guid.NewGuid();            
            CreationTime = DateTime.UtcNow;
            EventTypeName = integrationEvent.GetType().FullName;
            Content = JsonSerializer.Serialize(integrationEvent);
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
            TransactionId = transactionId.ToString();
        }

        private IntegrationEventLogEntry()
        {
        }

        public Guid EventId { get; private set; }

        public string EventTypeName { get; private set; }

        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();

        [NotMapped]
        public INotification IntegrationEvent { get; private set; }

        public EventStateEnum State { get; set; }

        public int TimesSent { get; set; }

        public DateTime CreationTime { get; private set; }

        public string Content { get; private set; }

        public string TransactionId { get; private set; }

        public IntegrationEventLogEntry DeserializeJsonContent<T>()
            where T : IntegrationEvent
        {
            IntegrationEvent = JsonSerializer.Deserialize<T>(Content);
            return this;
        }
    }
}
