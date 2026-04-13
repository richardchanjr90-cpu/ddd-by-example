using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class PatchOrderAcceptanceCommandHandler
        : BaseHandler, IRequestHandler<PatchOrderAcceptanceCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchOrderAcceptanceCommandHandler(
            ILoyaltyTenantDbContext context,
            IMediator mediator,
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchOrderAcceptanceCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Where(x => x.Id == request.VenueId)
                .SingleAsync(cancellationToken);

            if (request.Accept)
            {
                venue.AcceptNewOrders();
            }
            else
            {
                venue.RejectNewOrders();
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new PatchOrderAcceptanceNotification
                    {
                        VenueId = venue.Id,
                        Accept = venue.AcceptsOrders
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}
