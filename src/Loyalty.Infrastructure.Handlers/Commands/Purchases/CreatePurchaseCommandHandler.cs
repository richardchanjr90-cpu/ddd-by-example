using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Notifications.Visit;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using MediatR.Extensions.UnitOfWork.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class CreatePurchaseCommandHandler : BaseDapperHandler, IRequestHandler<CreatePurchaseCommand, INotificationResult>
    {
        private readonly SqlConnection connection;

        public CreatePurchaseCommandHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        private const string SelectProductsSql = @"SELECT COUNT(p.[Id])
                                                  FROM [loyalty].[Product] p
                                                  JOIN [loyalty].[ProductGroup] pg ON p.ProductGroupId = pg.Id
                                                  WHERE p.Id = @id AND pg.VenueId in @ids";

        private const string SelectLoyaltyProductGroupsSql = @"SELECT COUNT(lpg.[Id])
                                                          FROM [loyalty].[LoyaltyProductGroup] lpg
                                                          JOIN  [loyalty].LoyaltyProgram lp ON lp.Id = lpg.LoyaltyProgramId
                                                          WHERE lpg.Id = @lpgId AND lp.VenueId IN @ids";

        private const string InsertSQL = @"INSERT INTO [loyalty].[Purchase]
                                           ([CreatedBy],
                                            [ModifiedBy],
                                            [Modified],
                                            [Created],
                                            [LoyaltyProductGroupId],
                                            [ProductId],
                                            [UserId],                                 
                                            [InternalPurchaseMadeBySystem],
                                            [Value],
                                            [VenueId]) 
                                                        Values (
                                                        @CreatedBy,
                                                        @ModifiedBy,
                                                        @Modified,
                                                        @Created,
                                                        @LoyaltyProductGroupId,
                                                        @ProductId,
                                                        @UserId,
                                                        @InternalPurchaseMadeBySystem,
                                                        @Value,
                                                        @VenueId)";

        public async Task<INotificationResult> Handle(
            CreatePurchaseCommand request,
            CancellationToken cancellationToken)
        {
            connection.Open();
            var ids = Principal.GetVenueIds();
            if (request.ProductId != null)
            {
                var id = request.ProductId;
                var number = connection.ExecuteScalar<int>(SelectProductsSql, new
                {
                    id,
                    ids
                });

                if (number == 0)
                {
                    throw new LoyaltyValidationException("Product does not belong to this venue or does not exist.", ErrorCode.INCORRECT_PRODUCT);
                }
            }

            var lpgId = request.LoyaltyProductGroupId;
            var lpgNumber = connection.ExecuteScalar<int>(SelectLoyaltyProductGroupsSql, new
            {
                lpgId,
                ids
            });

            if (lpgNumber == 0)
            {
                throw new LoyaltyValidationException(
                    "LoyaltyProductGroup does not belong to this venue or does not exist.",
                    ErrorCode.INCORRECT_LOYALTY_GROUP);
            }

            var date = DateTime.Now;
            var affectedRows = connection.Execute(InsertSQL, new
            {
                CreatedBy = Principal.GetUserId(),
                ModifiedBy = Principal.GetUserId(),
                Modified = date,
                Created = date,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId,
                ProductId = request.ProductId,
                UserId = request.UserId,
                Value = request.Value,
                VenueId = request.VenueId
            });

            var result = new NotificationResult { Success = affectedRows > 0 };

            var notification = new CreatePurchaseNotification
            {
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

            return result;
        }
    }
}