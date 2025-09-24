using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductGroupCommandHandler 
        : BaseHandler, IArchiveLoyaltyProductGroupCommandHandler 
    {
        public ArchiveLoyaltyProductGroupCommandHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public Task<ICommandResult> Handle(ArchiveLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
