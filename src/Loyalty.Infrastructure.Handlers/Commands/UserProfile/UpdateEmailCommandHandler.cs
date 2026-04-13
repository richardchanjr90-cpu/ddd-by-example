using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.UserProfile
{
    public class UpdateEmailCommandHandler
        : BaseHandler, IRequestHandler<UpdateEmailCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public UpdateEmailCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor, IMediator mediator)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .IgnoreQueryFilters()
                .Include(x => x.Venues)
                .Where(x => x.WorkerId == request.WorkerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (!request.Email.Equals(worker.Email))
            {
                var emailUser = await Context.Workers
                    .IgnoreQueryFilters()
                    .Include(x => x.Venues)
                    .Where(x => x.Email == request.Email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (emailUser != null)
                {
                    throw new LoyaltyValidationException("This email is already taken.", ErrorCode.EMAIL_EXISTS);
                }
            }

            worker.Email = request.Email;

            var commandResult = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };

            return commandResult;
        }
    }
}