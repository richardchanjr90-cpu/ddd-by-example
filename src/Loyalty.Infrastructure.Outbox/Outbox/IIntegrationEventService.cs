using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using MediatR;

namespace Loyalty.Infrastructure.Outbox.Outbox
{
    public interface IIntegrationEventService
    {
        Task<List<IntegrationEventLogEntry>> RetrieveNotProcessedEvents(Guid transactionId);

        Task SaveEventAsync(INotification integrationEvent);

        Task MarkEventAsPublishedAsync(Guid eventId);

        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
