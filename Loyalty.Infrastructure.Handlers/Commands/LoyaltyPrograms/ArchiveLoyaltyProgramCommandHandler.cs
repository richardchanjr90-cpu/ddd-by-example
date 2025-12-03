using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramCommandHandler
        : BaseHandler, IArchiveLoyaltyProgramCommandHandler
    {
        private readonly IMediator mediator;

        public ArchiveLoyaltyProgramCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(
            ArchiveLoyaltyProgramCommand request,
            CancellationToken cancellationToken)
        {
            var program = await Context.LoyaltyPrograms
                .Include(x => x.LoyaltyProductGroups)
                .ThenInclude(x => x.Group)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (program != null)
            {
                program.IsArchived = true;

                if (program.LoyaltyProductGroups != null)
                {
                    foreach (var group in program.LoyaltyProductGroups)
                    {
                        group.IsArchived = true;
                    }
                }
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = program?.Id
            };

            if (result.Success && program != null)
            {
                await mediator.Publish(
                    new ArchiveLoyaltyProgramNotification
                    {
                        Id = program.Id
                    },
                    cancellationToken);

                if (program.LoyaltyProductGroups != null)
                {
                    foreach (var lpg in program.LoyaltyProductGroups)
                    {
                        await mediator.Publish(
                            new ArchiveLoyaltyProductGroupNotification
                            {
                                Id = lpg.Id
                            },
                            cancellationToken);
                    }
                }
            }
     
            return result;
        }
    }
}