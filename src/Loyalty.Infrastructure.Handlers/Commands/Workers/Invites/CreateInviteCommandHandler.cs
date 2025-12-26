using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers.Invites
{
    public class CreateInviteCommandHandler
        : BaseHandler, ICreateInviteCommandHandler
    {
        public CreateInviteCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(CreateInviteCommand request, CancellationToken cancellationToken)
        {
            if (request.Role >= Principal.GetRole())
            {
                throw new LoyaltyValidationException("Impossible to invite a user with the role that is >= current user's.", null, ErrorCode.IMPOSSIBLE_TO_CREATE_WITH_ROLE);
            }

            var worker = new Worker
            {
                Name = request.Name,
                Phone = request.Phone,
                PositionName = request.PositionName,
            };

            var venueWorker = new VenueWorker();
            venueWorker.VenueId = request.VenueId;
            venueWorker.Worker = worker;
            venueWorker.Role = request.Role;
            Context.VenueWorkers.Add(venueWorker);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}