using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class CreateVenueCommandHandler : BaseHandler, IRequestHandler<CreateVenueCommand, ICommandResult>
    {
        private readonly IMediator mediator;
        private readonly IOptions<VenueSettings> venueOptions;

        public CreateVenueCommandHandler(
            ILoyaltyTenantDbContext context,
            IMediator mediator,
            IOptions<VenueSettings> venueOptions,
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
            this.venueOptions = venueOptions;
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            Venue venue;
            Worker worker;
            var strategy = Context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                CommandResult result;
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    venue = CreateVenue(request);
                    var saved = await Context.SaveChangesAsync(cancellationToken) > 0;
                    Principal.AddVenues(venue.Id);

                    worker = await Context.Workers
                    .IgnoreQueryFilters()
                    .Include(x => x.Venues)
                    .ThenInclude(x => x.Venue)
                    .Where(x => x.WorkerId == Principal.GetUserId())
                    .FirstOrDefaultAsync(cancellationToken);

                    worker = UpdateWorker(worker, venue);
                    saved = saved && await Context.SaveChangesAsync(cancellationToken) > 0;

                    result = new CommandResult
                    {
                        Success = saved,
                        Result = venue.Id
                    };

                    await AddClaimsAboutNewVenue(worker);
                    scope.Complete();
                }

                if (result.Success && worker != null)
                {
                    await mediator.Publish(
                        new UpdatedWorkerNotification
                        {
                            WorkerId = worker.WorkerId,
                            LastName = worker.LastName,
                            Name = worker.Name,
                            PhotoUri = worker.PhotoUri,
                            Role = VenueUserRole.Owner,
                            VenueId = venue.Id
                        },
                        cancellationToken);
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

        private Worker UpdateWorker(Worker worker, Venue venue)
        {
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
            //todo: move to a firebase handler.
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(Principal.GetUserId());
            var claims = user.CustomClaims.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var ids = worker.Venues.Select(x => x.VenueId).Select(x => x.ToString());

            if (worker.Venues.Select(x => x.VenueId).Count() > venueOptions.Value.MaxVenueNumber)
            {
                throw new LoyaltyValidationException(
                    $"Limit of {venueOptions.Value.MaxVenueNumber} venues reached.", ErrorCode.LIMIT_REACHED);
            }

            claims[ClaimConstants.VENUE_CLAIM] = ids.ToCommaSeparatedStringOrNull();
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(Principal.GetUserId(), claims);
        }
    }
}