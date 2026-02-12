using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.UserProfile
{
    public class UpdateUserProfileCommandHandler
        : BaseHandler, IRequestHandler<UpdateUserProfileCommand, ICommandResult>
    {
        public UpdateUserProfileCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Include(x => x.Venues)
                .Where(x => x.WorkerId == request.WorkerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (!request.Email.Equals(worker.Email))
            {
                var emailUser = await Context.Workers
                    .Include(x => x.Venues)
                    .Where(x => x.Email == request.Email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (emailUser != null)
                {
                    throw new LoyaltyValidationException("This email is already taken", null, ErrorCode.EMAIL_EXISTS);
                }
            }

            worker.Name = request.Name;
            worker.LastName = request.LastName;
            worker.Email = request.Email;
            worker.PositionName = request.PositionName;

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}