using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductGroupCommandHandler 
        : BaseHandler, IArchiveLoyaltyProductGroupCommandHandler 
    {
        public ArchiveLoyaltyProductGroupCommandHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.LoyaltyProductGroups
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

            //if (result.Success)
            //{
            //    await mediator.Publish(venue.ToVenueNotification(), cancellationToken);
            //}
            return result;
        }
    }
}
