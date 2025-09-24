using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class ArchiveProductGroupCommandHandler
        : BaseHandler, IArchiveProductGroupCommandHandler
    {
        public ArchiveProductGroupCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(ArchiveProductGroupCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
