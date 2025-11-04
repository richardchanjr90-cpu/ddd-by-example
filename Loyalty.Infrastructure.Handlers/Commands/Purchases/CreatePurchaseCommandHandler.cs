using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Purchases;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class CreatePurchaseCommandHandler : BaseHandler, ICreatePurchaseCommandHandler
    {
        private readonly IMediator mediator;

        public CreatePurchaseCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = new Purchase
            {
                Value = request.Value,
                UserId = request.UserId,
                ProductId = request.ProductId,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId
            };

            Context.Purchases.Add(purchase);

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = purchase.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new CreatePurchaseNotification
                    {
                        VenueId = request.VenueId,
                        UserId = purchase.UserId,
                        LoyaltyProductGroupId = purchase.LoyaltyProductGroupId,
                        Total = purchase.Value
                    },
                    cancellationToken);
            }
            return result;
        }
    }
}
