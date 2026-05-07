using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Handlers.Commands.Pipelines
{
    public class TransactionBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<ICommandResult>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> logger;
        private readonly ILoyaltyTenantDbContext dbContext;
        private readonly IEventBusService eventService;

        public TransactionBehaviour(
            ILoyaltyTenantDbContext dbContext,
            IEventBusService eventService,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(ILoyaltyTenantDbContext));
            this.eventService = eventService ?? throw new ArgumentException(nameof(eventService));
            this.logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType().Name;

            try
            {
                if (dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await dbContext.BeginTransactionAsync())
                    {
                        logger.LogInformation(
                            "----- Begin transaction {TransactionId} for {CommandName} ({@Request})",
                            transaction.TransactionId,
                            typeName,
                            request);

                        response = await next();

                        logger.LogInformation(
                            "----- Commit transaction {TransactionId} for {CommandName}",
                            transaction.TransactionId,
                            typeName);

                        await dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    await eventService.PublishEventsAsync(transactionId);
                });

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "ERROR Handling transaction for {CommandName} ({@Command})",
                    typeName,
                    request);

                throw;
            }
        }
    }
}
