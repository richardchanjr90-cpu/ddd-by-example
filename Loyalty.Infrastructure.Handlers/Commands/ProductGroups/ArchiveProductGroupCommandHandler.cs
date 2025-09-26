using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class ArchiveProductGroupCommandHandler
        : BaseHandler, IArchiveProductGroupCommandHandler
    {
        public ArchiveProductGroupCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                group.IsArchived = true;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };
        }
    }
}
