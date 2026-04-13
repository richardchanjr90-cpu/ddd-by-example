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
    public class UpdateUserProfileCommandHandler
        : BaseHandler, IRequestHandler<UpdateUserProfileCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public UpdateUserProfileCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor, IMediator mediator)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .IgnoreQueryFilters()
                .Include(x => x.Venues)
                .Where(x => x.WorkerId == request.WorkerId)
                .FirstOrDefaultAsync(cancellationToken);

            worker.Name = request.Name;
            worker.LastName = request.LastName;

            var commandResult = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };

            if (commandResult.Success)
            {
                await mediator.Publish(
                    new UpdateWorkerProfileNotification
                    {
                        WorkerId = worker.WorkerId,
                        LastName = worker.LastName,
                        Name = worker.Name
                    },
                    cancellationToken);
            }

            return commandResult;
        }
    }
}