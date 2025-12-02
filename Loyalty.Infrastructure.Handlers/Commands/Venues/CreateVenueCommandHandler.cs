using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class CreateVenueCommandHandler : BaseHandler, ICreateVenueCommandHandler
    {
        private readonly IMediator mediator;

        public CreateVenueCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = request.ToSingle(Principal.GetUserId());

            var worker = await Context.Workers
                .Include(x => x.Venues)
                .ThenInclude(x => x.Venue)
                .Where(x => x.WorkerId == Principal.GetUserId())
                .FirstOrDefaultAsync(cancellationToken);

            if (worker == null)
            {
                worker = new Worker
                {
                    PositionName = "Владелец",
                    WorkerId = Principal.GetUserId(),
                    Phone = Principal.GetPhone(),
                    Name = Principal.GetName(),
                    LastName = Principal.GetSurname(),
                };
            }

            var venueWorker = new VenueWorker
            {
                Venue = venue,
                Worker = worker,
                Role = VenueUserRole.Owner
            };

            Context.VenueWorkers.Add(venueWorker);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(venue.ToVenueNotification(), cancellationToken);
                await AddClaimsAboutNewVenue(worker);
            }

            return result;
        }

        private async Task AddClaimsAboutNewVenue(Worker worker)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(Principal.GetUserId());
            var claims = user.CustomClaims.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var ids = worker.Venues.Select(x => x.VenueId).Select(x => x.ToString());
            claims[ClaimConstants.VENUE_CLAIM] = ids.ToCommaSeparatedStringOrNull();
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(Principal.GetUserId(), claims);
        }
    }
}