using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Orders;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Orders
{
    public class PatchOrderCommandHandler
        : BaseHandler, IRequestHandler<PatchOrderCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchOrderCommandHandler(ILoyaltyDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Context.Orders
                .Where(x => x.Id == request.OrderId)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            order?.UpdateStatus(request.Status);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = order?.Id
            };

            if (order != null && result.Success)
            {
                await mediator.Publish(
                    new UpdateOrderNotification
                    {
                        Id = order.Id,
                        Status = (OrderStatus)order.Status.Id,
                        VenueId = order.VenueId
                    }, cancellationToken);
            }

            return result;
        }
    }
}
