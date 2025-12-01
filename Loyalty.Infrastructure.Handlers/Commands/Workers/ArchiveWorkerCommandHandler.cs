using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class ArchiveWorkerCommandHandler
        : BaseHandler, IArchiveWorkerCommandHandler
    {
        public ArchiveWorkerCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (worker != null)
            {
                worker.IsArchived = true;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker?.Id
            };
        }
    }
}