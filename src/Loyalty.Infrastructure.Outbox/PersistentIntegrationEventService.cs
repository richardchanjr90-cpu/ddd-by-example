using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using Loyalty.Core.Outbox.Entities.Enums;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Events.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Outbox.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loyalty.Infrastructure.Outbox
{
    public class PersistentIntegrationEventService : IIntegrationEventService
    {
        private readonly IIntegrationEventsContext dbContext;
        private readonly ILoyaltyTenantDbContext tenantDbContext;

        public PersistentIntegrationEventService(
            IIntegrationEventsContext dbContext, 
            ILoyaltyTenantDbContext tenantDbContext)
        {
            this.dbContext = dbContext;
            this.tenantDbContext = tenantDbContext;
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

        public virtual async Task SaveEventAsync(INotification integrationEvent)
        {
            var transaction = tenantDbContext.GetCurrentTransaction();

            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var eventLogEntry = new IntegrationEventLogEntry(integrationEvent, transaction.TransactionId);

            await dbContext.Database.UseTransactionAsync(transaction.GetDbTransaction());
            await dbContext.IntegrationEvents.AddAsync(eventLogEntry);

            await dbContext.SaveChangesAsync(default);
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

            return dbContext.SaveChangesAsync(default);
        }
    }
}
