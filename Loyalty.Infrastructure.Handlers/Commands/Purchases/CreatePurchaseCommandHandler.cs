using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Purchases;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class CreatePurchaseCommandHandler : BaseHandler, ICreatePurchaseCommandHandler
    {
        public CreatePurchaseCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = new Core.Entities.Purchase
            {
                Value = request.Value,
                UserId = request.UserId,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId
            };

            Context.Purchases.Add(purchase);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = purchase.Id
            };
        }
    }
}
