using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductGroupCommandHandler
        : BaseHandler, IArchiveLoyaltyProductGroupCommandHandler
    {
        private readonly IMediator mediator;

        public ArchiveLoyaltyProductGroupCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ArchiveLoyaltyProductGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = await Context.LoyaltyProductGroups
                .Include(x => x.LoyaltyProgram)
                .Include(x => x.Group)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                group.IsArchived = true;
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };

            if (result.Success && group != null)
            {
                await mediator.Publish(
                    new ArchiveLoyaltyProductGroupNotification
                    {
                        Id = group.Id
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}