using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Purchases;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class BurnPurchaseCommandHandler : BaseHandler, IBurnPurchaseCommandHandler
    {
        private readonly IMediator mediator;

        public BurnPurchaseCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(BurnPurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchases = await Context.Purchases
                .IgnoreQueryFilters()
                .Where(x => x.LoyaltyProductGroupId == request.LoyaltyProductGroupId
                            && x.UserId == request.UserId
                            && !x.BurnDate.HasValue)
                .ToListAsync(cancellationToken);

            var amount = purchases.Sum(x => x.Value);

            if (amount < request.Amount)
            {
                throw new LoyaltyValidationException("Amount of points is lower than requested");
            }

            var amountToBurn = request.Amount;
            foreach (var purchase in purchases)
            {
                var purchaseValue = purchase.Value;
                purchase.BurnDate = DateTime.Now;

                if (purchaseValue > amountToBurn)
                {
                    purchase.BurnDate = DateTime.Now;
                    var amountToReSave = purchaseValue - amountToBurn;
                    var leftOverPurchase = new Purchase
                    {
                        UserId = request.UserId,
                        Value = amountToReSave,
                        InternalPurchaseMadeBySystem = true,
                        LoyaltyProductGroupId = purchase.LoyaltyProductGroupId,
                        ProductId = purchase.ProductId
                    };
                    Context.Purchases.Add(leftOverPurchase);
                    break;
                }

                amountToBurn -= purchaseValue ?? 0;
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = purchases.Select(x => x.Id).ToList()
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new BurnPurchaseNotification
                    {
                        VenueId = request.VenueId,
                        UserId = request.UserId,
                        LoyaltyProductGroupId = request.LoyaltyProductGroupId,
                        Total = request.Amount
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}