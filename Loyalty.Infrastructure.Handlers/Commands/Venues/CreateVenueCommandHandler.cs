using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class CreateVenueCommandHandler : BaseHandler, ICreateVenueCommandHandler
    {
        private readonly IMediator mediator;

        public CreateVenueCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = request.ToSingle();

            var worker = new Worker
            {
                Role = VenueUserRole.Owner,
                PositionName = "Владелец",
                WorkerId = request.OwnerId,
                Phone = request.OwnerPhone,
                Name = request.OwnerName,
                LastName = request.OwnerSurname
            };

            venue.Workers = new List<Worker>
                 {
                     worker
                 };

            Context.Venues.Add(venue);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(venue.ToVenueNotification(), cancellationToken);
            }

            return result;
        }
    }
}