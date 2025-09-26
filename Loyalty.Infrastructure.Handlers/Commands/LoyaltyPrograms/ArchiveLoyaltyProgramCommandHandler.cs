using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramCommandHandler
        : BaseHandler, IArchiveLoyaltyProgramCommandHandler
    {
        public ArchiveLoyaltyProgramCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveLoyaltyProgramCommand request, CancellationToken cancellationToken)
        {
            var program = await Context.LoyaltyPrograms
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (program != null)
            {
                program.IsArchived = true;
            }
         
            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = program?.Id
            };
        }
    }
}
