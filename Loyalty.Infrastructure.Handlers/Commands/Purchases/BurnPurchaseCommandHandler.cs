using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class BurnPurchaseCommandHandler : BaseHandler, IBurnPurchaseCommandHandler
    {
        public BurnPurchaseCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(BurnPurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchases = await Context.Purchases
                .Where(x => x.LoyaltyProductGroupId == request.LoyaltyProductGroupId
                            && x.UserId == request.UserId
                            && !x.BurnDate.HasValue)
                .Take(request.Amount)
                .ToListAsync(cancellationToken);

            if (purchases.Count < request.Amount)
            {
                throw new LoyaltyValidationException("Amount of points is lower than requested");
            }

            var date = DateTime.Now;
            purchases.ForEach(x => x.BurnDate = date);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = purchases.Select(x => x.Id).ToList()
            };
        }
    }
}
