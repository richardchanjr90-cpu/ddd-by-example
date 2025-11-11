using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class ApproveVenueCommandHandler : BaseHandler, IApproveVenueCommandHandler
    {
        private readonly IMediator mediator;

        public ApproveVenueCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ApproveVenuePatchCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Where(x => x.Id == request.Id)
                .SingleAsync(cancellationToken);

            if (!venue.IsPublished)
            {
                throw new ValidationException("Venue is not Published, so it can't be approved.");
            }

            var wasApproved = venue.IsApproved;
            venue.IsApproved = true;

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success || wasApproved)
            {
                await mediator.Publish(
                    new PatchVenueApproveNotification
                    {
                        Id = venue.Id
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}
