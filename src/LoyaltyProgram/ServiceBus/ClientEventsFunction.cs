using System;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Storage.Dto.Orders;
using Loyalty.Common.Shared.Extensions;
using LoyaltyClient.Domain.Handlers.Notifications.Code;
using LoyaltyClient.Domain.Handlers.Notifications.Orders;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.ServiceBus
{
    public class ClientEventsFunction
    {
        private readonly SqlConnection connection;

        public ClientEventsFunction(SqlConnection connection)
        {
            this.connection = connection;
        }

        [FunctionName("ClientEventsFunction")]
        public async Task Run(
            [ServiceBusTrigger("%ServiceBusClientsTopicName%", "venues", Connection = "ServiceBusConnectionString")] Message message,
            [Queue("neworder-notification", Connection = "QueueConnectionString")] ICollector<NewOrderDto> newOrders,
            [Queue("neworder-notification", Connection = "QueueConnectionString")] ICollector<OrderDeclinedDto> orders,
            ILogger log)
        {
            log.LogInformation($"{nameof(ClientEventsFunction)} was triggered.");

            switch (message.ContentType)
            {
                case nameof(CodeGeneratedNotification):
                    await ProcessClientCode(message.Deserialize<CodeGeneratedNotification>());
                    break;

                case nameof(CreateOrderNotification):
                    await CreateOrder(message.Deserialize<CreateOrderNotification>(), newOrders);
                    break;

                case nameof(PatchOrderNotification):
                    await PatchOrder(message.Deserialize<PatchOrderNotification>(), orders);
                    break;

                default:
                    log.LogInformation($"No handle for: {message}");
                    break;
            }
        }

        private async Task ProcessClientCode(CodeGeneratedNotification deserialize)
        {
            var mergeSql = @"MERGE [loyalty].UserCode
                                USING ( 
                                    VALUES (@UserId, @CodeValue, @ExpirationDate)
                                ) AS foo (userId, codeValue, expirationDate) 
                                ON [loyalty].UserCode.userId = foo.userId 
                                WHEN MATCHED THEN
                                   UPDATE SET codeValue = foo.codeValue, expirationDate = foo.expirationDate
                                WHEN NOT MATCHED THEN
                                   INSERT (userId, codeValue, expirationDate)
                                   VALUES (foo.userId, foo.codeValue, foo.expirationDate)
                                ; --A MERGE statement must be terminated by a semi-colon (;).";

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                connection.Open();
                var isSuccess = await connection.ExecuteAsync(mergeSql, deserialize);
                scope.Complete();
            }
        }

        private async Task CreateOrder(CreateOrderNotification deserialize, ICollector<NewOrderDto> newOrders)
        {
            const string inserOrderSql = @"INSERT INTO loyalty.[Order] 
                                   (Id, 
                                    CreatedBy, 
                                    ModifiedBy,
                                    Created,
                                    Modified, 
                                    VenueId, 
                                    PlacedDate, 
                                    [Status], 
                                    PickUpTime, 
                                    Comment) 
                            VALUES (@Id, 
                                    @UserId, 
                                    @UserId, 
                                    GETDATE(), 
                                    GETDATE(), 
                                    @VenueId, 
                                    @PlacedDate,
                                    @Status, 
                                    @PickUpTime, 
                                    @Comment) ";
            const string insertOrderItemsSql = @"INSERT INTO loyalty.[OrderItem] (Id, ProductId, OrderId, Amount)
                                        VALUES (@Id, @ProductId, @OrderId, @Amount)";

            var orderItems = deserialize.OrderItems;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                connection.Open();

                var isSuccess = await connection.ExecuteAsync(inserOrderSql, deserialize);
                var isSuccess2 = await connection.ExecuteAsync(insertOrderItemsSql, orderItems);

                if (isSuccess > 0 && isSuccess2 > 0)
                {
                    newOrders.Add(new NewOrderDto
                    {
                        Date = DateTime.Now,
                        VenueId = deserialize.VenueId
                    });
                }

                scope.Complete();
            }
        }

        private async Task PatchOrder(PatchOrderNotification deserialize, ICollector<OrderDeclinedDto> orders)
        {
            var updateOrderSql = "UPDATE loyalty.[Order] SET [Status] = @UpdatedStatus WHERE Id = @Id";

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                connection.Open();

                var isSuccess = await connection.ExecuteAsync(updateOrderSql, deserialize);

                if (isSuccess > 0)
                {
                    orders.Add(new OrderDeclinedDto
                    {
                        Id = deserialize.Id,
                        VenueId = deserialize.VenueId,
                        UserId = deserialize.UserId,
                        UpdatedStatus = deserialize.UpdatedStatus
                    });
                }

                scope.Complete();
            }
        }
    }
}