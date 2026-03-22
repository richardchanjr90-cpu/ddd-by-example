using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Purchases;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Notifications.Visit;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class CreatePurchaseCommandHandler : BaseHandler, ICreatePurchaseCommandHandler
    {
        private readonly IMediator mediator;

        public CreatePurchaseCommandHandler(
            ILoyaltyTenantDbContext context,
            IMediator mediator,
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(
            CreatePurchaseCommand request,
            CancellationToken cancellationToken)
        {
            if (request.ProductId != null)
            {
                var product = await Context.Products.Where(x => x.Id == request.ProductId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (product == null)
                {
                    throw new LoyaltyValidationException("Product does not belong to this venue or does not exist.", null, ErrorCode.INCORRECT_PRODUCT);
                }
            }

            var loyaltyGroup = await Context.LoyaltyProductGroups
                .Where(x => x.Id == request.LoyaltyProductGroupId)
                .SingleOrDefaultAsync(cancellationToken);

            if (loyaltyGroup == null)
            {
                throw new LoyaltyValidationException("LoyaltyProductGroup does not belong to this venue or does not exist.", null, ErrorCode.INCORRECT_LOYALTY_GROUP);
            }

            var purchase = new Purchase
            {
                Value = request.Value,
<<<<<<< Updated upstream
                UserId = request.UserId,
                ProductId = request.ProductId,
                VenueId = request.VenueId,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId
            };

            Context.Purchases.Add(purchase);
=======
                VenueId = request.VenueId
            });

            var result = new NotificationResult { Success = affectedRows > 0 };
>>>>>>> Stashed changes

            var result = new CommandResult();
            result.Success = await Context.SaveChangesAsync(cancellationToken) > 0;
            result.Result = purchase.Id;

            if (result.Success)
            {
<<<<<<< Updated upstream
                var notification = new CreatePurchaseNotification
                {
                    VenueId = request.VenueId,
                    UserId = purchase.UserId,
                    LoyaltyProductGroupId = purchase.LoyaltyProductGroupId,
                    Total = purchase.Value
                };

                await mediator.Publish(notification, cancellationToken);
            }
=======
                VenueId = request.VenueId,
                UserId = request.UserId,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId,
                Total = request.Value,
                When = date,
                WorkerId = request.WorkerId
            };

            var visit = new CreateVisitNotification
            {
                VenueId = request.VenueId,
                UserId = request.UserId,
                When = date
            };

            result.OnSucceededNotifications.Add(notification);
            result.OnSucceededNotifications.Add(visit);
>>>>>>> Stashed changes

            return result;
        }
    }
}