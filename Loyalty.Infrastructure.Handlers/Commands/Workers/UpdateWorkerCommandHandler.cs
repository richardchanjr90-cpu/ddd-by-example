using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : BaseHandler, IUpdateWorkerCommandHandler
    {
        public UpdateWorkerCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
