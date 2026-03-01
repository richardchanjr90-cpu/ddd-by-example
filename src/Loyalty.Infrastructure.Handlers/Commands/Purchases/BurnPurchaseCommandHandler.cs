using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using MediatR.Extensions.UnitOfWork.Results;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class BurnPurchaseCommandHandler : BaseDapperHandler, IRequestHandler<BurnPurchaseCommand, INotificationResult>
    {
        private readonly SqlConnection connection;

        public BurnPurchaseCommandHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        private const string SelectProductsSql = @"SELECT SUM([Value]) FROM loyalty.Purchase
                                                           WHERE LoyaltyProductGroupId = @lpgId AND UserId = @userId";

        private const string InsertSQL = @"INSERT INTO [loyalty].[Purchase]
                                           ([CreatedBy],
                                            [ModifiedBy],
                                            [Modified],
                                            [Created],
                                            [LoyaltyProductGroupId],
                                            [UserId],                                 
                                            [InternalPurchaseMadeBySystem],
                                            [Value],
                                            [BurnDate],
                                            [VenueId]) 
                                                        Values (
                                                        @CreatedBy,
                                                        @ModifiedBy,
                                                        @Modified,
                                                        @Created,
                                                        @LoyaltyProductGroupId,
                                                        @UserId,
                                                        @InternalPurchaseMadeBySystem,
                                                        @Value,
                                                        @BurnDate,
                                                        @VenueId)";

        public async Task<INotificationResult> Handle(BurnPurchaseCommand request, CancellationToken cancellationToken)
        {
            connection.Open();
            var amount = connection.ExecuteScalar<int>(SelectProductsSql, new
            {
                lpgId = request.LoyaltyProductGroupId,
                userId = request.UserId
            });

            if (amount < request.Amount)
            {
                throw new LoyaltyValidationException("Amount of points is lower than requested", null, ErrorCode.INCORRECT_AMOUNT_OF_POINTS);
            }

            var date = DateTime.Now;
            var affectedRows = connection.Execute(InsertSQL, new
            {
                CreatedBy = Principal.GetUserId(),
                ModifiedBy = Principal.GetUserId(),
                Modified = date,
                Created = date,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId,
                InternalPurchaseMadeBySystem = 0,
                BurnDate = date,
                UserId = request.UserId,
                Value = -request.Amount,
                VenueId = request.VenueId,
            });

            var result = new NotificationResult() { Success = affectedRows > 0 };

            var notification = new BurnPurchaseNotification
            {
                VenueId = request.VenueId,
                UserId = request.UserId,
                LoyaltyProductGroupId = request.LoyaltyProductGroupId,
                Total = request.Amount
            };

            result.OnSucceededNotifications.Add(notification);

            return result;
        }
    }
}
