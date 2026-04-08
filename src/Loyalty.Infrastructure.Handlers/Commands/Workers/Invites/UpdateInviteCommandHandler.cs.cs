using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers.Invites
{
    public class UpdateInviteCommandHandler
        : BaseHandler, IRequestHandler<UpdateInviteCommand, ICommandResult>
    {
        private readonly IHttpContextAccessor accessor;

        public UpdateInviteCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(UpdateInviteCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Include(x => x.Venues)
                .Where(x => x.Id == request.Id)
                .SingleAsync(cancellationToken);

            var changerRole = accessor.HttpContext.User.GetRole();
            var changerId = accessor.HttpContext.User.GetUserId();

            if (worker.WorkerId != null && worker.WorkerId.Equals(changerId))
            {
                throw new LoyaltyValidationException(
                    "Impossible to change yourself.", 
                    ErrorCode.INVALID_ROLE);
            }

            if (request.Role != VenueUserRole.Owner && request.Role >= changerRole)
            {
                throw new LoyaltyValidationException(
                    "Impossible to set a role that is higher or equals to personal.", 
                    ErrorCode.INVALID_ROLE);
            }

            if (request.Role == VenueUserRole.Owner)
            {
                throw new LoyaltyValidationException(
                    "Impossible to create a second owner.", 
                    ErrorCode.SECOND_OWNER_NOT_ALLOWED);
            }

            worker.Name = request.Name;

            var venueWorkerNew = worker.Venues
                .FirstOrDefault(x => x.VenueId == request.VenueId);

            if (venueWorkerNew != null)
            {
                if (venueWorkerNew.Role == VenueUserRole.Owner)
                {
                    throw new LoyaltyValidationException("Impossible to change owner's role.", ErrorCode.OWNER_CHANGE_DENIED);
                }

                if (venueWorkerNew.Role > changerRole)
                {
                    throw new LoyaltyValidationException(
                        "Impossible to change user with a higher role.", 
                        ErrorCode.INVALID_ROLE);
                }

                venueWorkerNew.Role = request.Role;
                venueWorkerNew.PositionName = request.PositionName;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}
