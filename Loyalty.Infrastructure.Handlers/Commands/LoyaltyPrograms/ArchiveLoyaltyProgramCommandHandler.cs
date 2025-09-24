using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramCommandHandler
        : BaseHandler, IArchiveLoyaltyProgramCommandHandler
    {
        public ArchiveLoyaltyProgramCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(ArchiveLoyaltyProgramCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
