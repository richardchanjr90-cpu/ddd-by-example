using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using Loyalty.Core.Outbox.Entities.Enums;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using Loyalty.Infrastructure.Outbox.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loyalty.Infrastructure.Outbox
{
    public class PersistentIntegrationEventService : IIntegrationEventService
    {
        private readonly IntegrationEventsContext dbContext;

        public PersistentIntegrationEventService(
            IntegrationEventsContext dbContext, 
            IDbContextTransaction transaction)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<List<IntegrationEventLogEntry>> RetrieveNotProcessedEvents(Guid transactionId)
        {
            var result = await dbContext.IntegrationEvents
                .Where(e => e.TransactionId == transactionId.ToString() &&
                            e.State == EventStateEnum.NotPublished)
                .OrderBy(x => x.CreationTime)
                .ToListAsync();

            return result
                ?.Select(e => e.DeserializeJsonContent<IntegrationEvent>())
                .ToList() ?? new List<IntegrationEventLogEntry>();
        }

        public virtual async Task SaveEventAsync(INotification integrationEvent, IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var eventLogEntry = new IntegrationEventLogEntry(integrationEvent, transaction.TransactionId);

            await dbContext.Database.UseTransactionAsync(transaction.GetDbTransaction());
            await dbContext.IntegrationEvents.AddAsync(eventLogEntry);

            await dbContext.SaveChangesAsync();
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            var eventLogEntry = dbContext.IntegrationEvents.Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            if(status == EventStateEnum.InProgress)
                eventLogEntry.TimesSent++;

            dbContext.IntegrationEvents.Update(eventLogEntry);

            return dbContext.SaveChangesAsync();
        }
    }
}
