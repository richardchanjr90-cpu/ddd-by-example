using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using MediatR.Extensions.UnitOfWork.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class BurnPurchaseCommandHandler : BaseHandler, IRequestHandler<BurnPurchaseCommand, INotificationResult>
    {
        private readonly IMediator mediator;

        public BurnPurchaseCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<INotificationResult> Handle(BurnPurchaseCommand request, CancellationToken cancellationToken)
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
                throw new LoyaltyValidationException("Amount of points is lower than requested", null, ErrorCode.INCORRECT_AMOUNT_OF_POINTS);
            }

            var purchase = new Purchase
            {
                UserId = request.UserId,
                VenueId = request.VenueId,
                Value = -request.Amount,
                InternalPurchaseMadeBySystem = true,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId,
            };

            Context.Purchases.Add(purchase);

            var notification = new NotificationResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
            };

            notification.OnSucceededNotifications.Add(new BurnPurchaseNotification
            {
                VenueId = request.VenueId,
                UserId = request.UserId,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId,
                Total = request.Amount
            });

            return notification;
        }
    }
}
