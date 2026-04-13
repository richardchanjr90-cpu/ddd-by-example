using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers.Invites
{
    public class CreateInviteCommandHandler
        : BaseHandler, IRequestHandler<CreateInviteCommand, ICommandResult>
    {
        public CreateInviteCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(CreateInviteCommand request, CancellationToken cancellationToken)
        {
            if (request.Role >= Principal.GetRole())
            {
                throw new LoyaltyValidationException("Impossible to invite a user with the role that is >= current user's.",
                    ErrorCode.IMPOSSIBLE_TO_CREATE_WITH_ROLE);
            }

            var dbWorker = await Context.Workers.
                IgnoreQueryFilters()
                .Include(x => x.Venues)
                .Where(x => x.Phone == request.Phone)
                .FirstOrDefaultAsync(cancellationToken);

            var worker = new Worker
            {
                Name = request.Name,
                Phone = request.Phone,
            };

            if (dbWorker != null)
            {
                if (dbWorker.Venues.Any(x => x.VenueId == request.VenueId))
                {
                    throw new LoyaltyValidationException("Already invited to this venue", ErrorCode.DUPLICATED_ENTITY);
                }

                worker = dbWorker;
            }

            var venueWorker = new VenueWorker
            {
                VenueId = request.VenueId, 
                Worker = worker, 
                Role = request.Role,
                PositionName = request.PositionName,
            };

            await Context.VenueWorkers.AddAsync(venueWorker, cancellationToken);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}