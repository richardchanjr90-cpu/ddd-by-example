using System;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Events.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Outbox.Outbox;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Outbox
{
    public class EventBusPublishingService : IEventBusService
    {
        private readonly IIntegrationEventService persistentService;
        private readonly IIntegrationEventsContext integrationEventsContext;
        private readonly ILoyaltyTenantDbContext loyaltyTenantDbContext;
        private readonly ILogger logger;
        private readonly IMediator mediator;

        public EventBusPublishingService(
            IIntegrationEventService persistentService,
            IIntegrationEventsContext integrationEventsContext,
            ILoyaltyTenantDbContext loyaltyTenantDbContext,
            ILogger logger,
            IMediator mediator)
        {
            this.persistentService = persistentService;
            this.integrationEventsContext = integrationEventsContext;
            this.loyaltyTenantDbContext = loyaltyTenantDbContext;
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task PersistEventAsync(INotification evt)
        {
            var transaction = loyaltyTenantDbContext.GetCurrentTransaction();
            await persistentService.SaveEventAsync(evt);
        }

        public async Task PublishEventsAsync(Guid transactionId)
        {
            var pendingLogEvents = await persistentService.RetrieveNotProcessedEvents(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                logger.LogInformation(
                    "Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", 
                    logEvt.EventId, 
                    logEvt.IntegrationEvent);

                try
                {
                    await  mediator.Publish(logEvt.IntegrationEvent);
                    await persistentService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex, 
                        "ERROR publishing integration event: {IntegrationEventId}", 
                        logEvt.EventId);

                    await persistentService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }
    }
}
