using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using Loyalty.Infrastructure.Events.DataAccess.Context.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Outbox
{
    public class LoggingIntegrationEventService : PersistentIntegrationEventService
    {
        private readonly ILogger logger;

        public LoggingIntegrationEventService(
            IIntegrationEventsContext dbContext,
            ILoyaltyTenantDbContext tenantDbContext,
            ILogger logger)
            : base(dbContext, tenantDbContext)
        {
            this.logger = logger;
        }

        public override async Task SaveEventAsync(INotification integrationEvent)
        {
            logger.LogInformation("--- Start {@IntegrationEvent}", integrationEvent);

            await base.SaveEventAsync(integrationEvent);

            logger.LogInformation("--- Finished {@Id}", integrationEvent);
        }

        public override async Task<List<IntegrationEventLogEntry>> RetrieveNotProcessedEvents(Guid transactionId)
        {
            logger.LogInformation("Start {transactionId}", transactionId);

            var results = await base.RetrieveNotProcessedEvents(transactionId);

            logger.LogInformation("Ended with results: {@Results}", results);

            return results;
        }
    }
}
