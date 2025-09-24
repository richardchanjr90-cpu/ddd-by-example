using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class UpdateLoyaltyProgramCommandHandler
        : BaseHandler, IUpdateLoyaltyProgramCommandHandler
    {
        public UpdateLoyaltyProgramCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(UpdateLoyaltyProgramCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
