using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Pipelines
{
    public class CommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILoyaltyTenantDbContext context;
        private readonly IMediator mediator;

        public CommandBehavior(ILoyaltyTenantDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (response is ICommandNotificationResult responseResult)
            {
                var entities = context.GetModifiedOrAddedEntitiesBeforeSaveChanges();

                try
                {
                    responseResult.CommandResult = new CommandResult();
                    responseResult.CommandResult.Success = await context.SaveChangesAsync(cancellationToken) > 0;
                    responseResult.CommandResult.Result = entities.Select(x => x.Id);
                }
                catch (Exception ex)
                {
                    responseResult.CommandResult.Success = false;
                    responseResult.CommandResult.Message = ex.Message;
                    responseResult.CommandResult.Result = ex;
                }

                if (responseResult.CommandResult.Success)
                {
                    foreach (var notification in responseResult.OnSuccessNotifications)
                    {
                        if (notification != null)
                        {
                            await mediator.Publish(notification(), cancellationToken);
                        }
                    }
                }
                else
                {
                    foreach (var notification in responseResult.OnFailNotifications)
                    {
                        if (notification != null)
                        {
                            await mediator.Publish(notification(), cancellationToken);
                        }
                    }
                }
            }

            return response;
        }
    }
}