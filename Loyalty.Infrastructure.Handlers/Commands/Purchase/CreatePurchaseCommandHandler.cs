using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchase
{
    public class CreatePurchaseCommandHandler : BaseHandler, ICreatePurchaseCommandHandler
    {
        public CreatePurchaseCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<ICommandResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
