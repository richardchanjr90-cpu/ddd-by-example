using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using Loyalty.Core.Outbox.Entities.Enums;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using Loyalty.Infrastructure.Events.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Outbox.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loyalty.Infrastructure.Outbox
{
    public class PersistentIntegrationEventService : IIntegrationEventService
    {
        private static readonly Lazy<List<Type>> EventTypes = new Lazy<List<Type>>(LoadEventTypes);
        private readonly IIntegrationEventsContext dbContext;
        private Guid transactionId;

        public PersistentIntegrationEventService(
            ILoyaltyTenantDbContext tenantDbContext)
        {
            this.dbContext = new IntegrationEventsContext(
                new DbContextOptionsBuilder<IntegrationEventsContext>()
                    .UseSqlServer(tenantDbContext.Database.GetDbConnection())
                    .Options);

            var transaction = tenantDbContext.GetCurrentTransaction();

            if (transaction != null)
            {
                dbContext.Database.UseTransaction(transaction.GetDbTransaction());
                transactionId = transaction.TransactionId;
            }
        }

        public virtual async Task<List<IntegrationEventLogEntry>> RetrieveNotProcessedEvents(Guid transactionId)
        {
            var result = await dbContext.IntegrationEvents
                .Where(e => e.TransactionId == transactionId.ToString() &&
                            e.State == EventStateEnum.NotPublished)
                .OrderBy(x => x.CreationTime)
                .ToListAsync();

            return result
                ?.Select(e => e.DeserializeJsonContent(EventTypes.Value.Find(t=> t.Name == e.EventTypeShortName)))
                .ToList() ?? new List<IntegrationEventLogEntry>();
        }

        public virtual async Task SaveEventAsync(INotification integrationEvent)
        {
            var eventLogEntry = new IntegrationEventLogEntry(integrationEvent, transactionId);
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

        private static List<Type> LoadEventTypes()
        {
            var interfaceType = typeof(INotification);

            return typeof(IVenueIntegrationEventsNotification).Assembly
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass)
                .ToList();
        }

        private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            var eventLogEntry = dbContext.IntegrationEvents.Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            if (status == EventStateEnum.InProgress)
                eventLogEntry.TimesSent++;

            dbContext.IntegrationEvents.Update(eventLogEntry);

            return dbContext.SaveChangesAsync(default);
        }
    }
}
