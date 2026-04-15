using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Outbox
{
    public abstract class LoggingIntegrationEventService : PersistentIntegrationEventService
    {
        private readonly ILogger logger;

        protected LoggingIntegrationEventService(
            IntegrationEventsContext dbContext,
            IDbContextTransaction transaction,
            ILogger logger)
            : base(dbContext, transaction)
        {
            this.logger = logger;
        }

        public override async Task SaveEventAsync(INotification integrationEvent, IDbContextTransaction transaction)
        {
            logger.LogInformation("--- Start {@IntegrationEvent}", integrationEvent);

            await base.SaveEventAsync(integrationEvent, transaction);

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
