using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class CreateLoyaltyProductGroupCommandHandler
        : BaseHandler, ICreateLoyaltyProductGroupCommandHandler
    {
        public CreateLoyaltyProductGroupCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(CreateLoyaltyProductGroupCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
