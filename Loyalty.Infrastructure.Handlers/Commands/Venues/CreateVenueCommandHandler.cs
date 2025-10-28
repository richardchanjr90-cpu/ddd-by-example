using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
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

            //todo: fill owner with real details;
            var worker = new Worker
            {
                WorkerId = request.OwnerId,
                Role = VenueUserRole.Owner,
                PositionName = "Owner",
                Phone = "+37529" + new Random().Next(1000000, 9999999).ToString(),
                Name = "NameStub",
                LastName = "LastNameStub"
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
                await mediator.Publish(venue.ToNotification(), cancellationToken);
            }

            return result;
        }
    }
}