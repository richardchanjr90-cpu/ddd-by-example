using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers.Invites
{
    public class UpdateInviteCommandHandler
        : BaseHandler, IUpdateInviteCommandHandler
    {
        public UpdateInviteCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(UpdateInviteCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Include(x => x.Venues)
                .Where(x => x.Id == request.Id)
                .SingleAsync(cancellationToken);

            if (request.Role == VenueUserRole.Owner)
            {
                throw new LoyaltyValidationException("Impossible to create a second owner.", null, ErrorCode.SECOND_OWNER_NOT_ALLOWED);
            }

            worker.Name = request.Name;
            worker.Phone = request.Phone;
            worker.PositionName = request.PositionName;

            var venueWorker = worker.Venues.Single(x => x.VenueId == request.VenueId);
            venueWorker.Role = request.Role;

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}
