using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Orders;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Notifications.Rate;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Orders
{
    public class PatchRateOrderCommandHandler
        : BaseHandler, IRequestHandler<PatchRateOrderCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchRateOrderCommandHandler(ILoyaltyDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchRateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Context.Orders
                .Where(x => x.Id == request.OrderId)
                .SingleOrDefaultAsync(cancellationToken);

            order?.GiveRateToUser(request.Rate, request.Comment);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = order?.Id
            };

            //todo: move to domain events.
            if (order != null && result.Success)
            {
                await mediator.Publish(
                    new UpsertUserRateNotification
                    {
                        VenueId = order.VenueId,
                        OrderId = order.Id,
                        Rate = (int)request.Rate,
                        UserId = order.CreatedBy
                    }, cancellationToken);
            }

            return result;
        }
    }
}
