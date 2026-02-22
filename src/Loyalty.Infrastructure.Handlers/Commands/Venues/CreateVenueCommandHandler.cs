using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class CreateVenueCommandHandler : BaseHandler, ICreateVenueCommandHandler
    {
        private const int MaxVenueNumberPerPersonLimit = 10;

        private readonly IMediator mediator;
        private readonly IHttpContextAccessor accessor;

        public CreateVenueCommandHandler(
            ILoyaltyTenantDbContext context,
            IMediator mediator,
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            Venue venue;
            var strategy = Context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                var result = new CommandResult();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    venue = CreateVenue(request);
                    var saved = await Context.SaveChangesAsync(cancellationToken) > 0;
                    Principal.AddVenues(venue.Id);

                    var worker = await Context.Workers
                        .IgnoreQueryFilters()
                        .Include(x => x.Venues)
                        .ThenInclude(x => x.Venue)
                        .Where(x => x.WorkerId == Principal.GetUserId())
                        .FirstOrDefaultAsync(cancellationToken);

                    worker = CreateWorker(worker, venue);

                    saved = saved && await Context.SaveChangesAsync(cancellationToken) > 0;

                    result = new CommandResult
                    {
                        Success = saved,
                        Result = venue.Id
                    };
                    await AddClaimsAboutNewVenue(worker);
                    scope.Complete();
                }

                if (result.Success)
                {
                    await mediator.Publish(venue.ToVenueNotification(), cancellationToken);
                }

                return result;
            });
        }

        private Venue CreateVenue(CreateVenueCommand request)
        {
            var venue = request.ToSingle(Principal.GetUserId());
            Context.Venues.Add(venue);
            return venue;
        }

        private Worker CreateWorker(Worker worker, Venue venue)
        {
            if (worker == null)
            {
                //todo: rework
                worker = new Worker
                {
                    WorkerId = Principal.GetUserId(),
                    Phone = Principal.GetPhone(),
                    Name = Principal.GetName(),
                    Email = Principal.GetEmailOrNull(),
                    LastName = Principal.GetSurname(),
                };
            }

            var venueWorker = new VenueWorker
            {
                Venue = venue,
                Worker = worker,
                PositionName = "Владелец",
                Role = VenueUserRole.Owner
            };

            Context.VenueWorkers.Add(venueWorker);
            return worker;
        }

        private async Task AddClaimsAboutNewVenue(Worker worker)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(Principal.GetUserId());
            var claims = user.CustomClaims.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var ids = worker.Venues.Select(x => x.VenueId).Select(x => x.ToString());

            if (worker.Venues.Select(x => x.VenueId).Count() >= MaxVenueNumberPerPersonLimit)
            {
                throw new LoyaltyValidationException(
                    $"Limit of {MaxVenueNumberPerPersonLimit} venues reached.", null, ErrorCode.LIMIT_REACHED);
            }

            claims[ClaimConstants.VENUE_CLAIM] = ids.ToCommaSeparatedStringOrNull();
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(Principal.GetUserId(), claims);
        }
    }
}