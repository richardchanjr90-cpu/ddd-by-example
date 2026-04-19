using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class BurnPurchaseCommandHandler : IRequestHandler<BurnPurchaseCommand, ICommandResult>
    {
        private readonly IPurchaseRepository purchaseRepository;

        public BurnPurchaseCommandHandler(IPurchaseRepository purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        public async Task<ICommandResult> Handle(BurnPurchaseCommand request, CancellationToken cancellationToken)
        {
            var amount = (await purchaseRepository.GetPurchasesForUserAsync(request.UserId, cancellationToken))
                .Sum(x => x.Value);

            if (amount < request.Amount)
            {
                throw new LoyaltyValidationException("Amount of points is lower than requested", ErrorCode.INCORRECT_AMOUNT_OF_POINTS);
            }

            var purchase = Purchase.Burn(
                request.WorkerId,
                request.LoyaltyProductGroupId, 
                request.VenueId, 
                null, 
                request.UserId, 
                request.Amount);

            await purchaseRepository.AddAsync(purchase);

            var result = new CommandResult
            {
                Success = await purchaseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = purchase.Id
            };

            return result;
        }
    }
}
