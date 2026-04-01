using System.Transactions;
using Dapper;
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
        public void Run(
            [ServiceBusTrigger("%ServiceBusClientsTopicName%", "venues", Connection = "ServiceBusConnectionString")]
            Message message,
            ILogger log)
        {
            log.LogInformation($"{nameof(ClientEventsFunction)} was triggered.");

            switch (message.ContentType)
            {
                case nameof(CodeGeneratedNotification):
                    ProcessClientCode(message.Deserialize<CodeGeneratedNotification>());
                    break;

                case nameof(CreateOrderNotification):
                    CreateOrder(message.Deserialize<CreateOrderNotification>());
                    break;

                default:
                    log.LogInformation($"No handle for: {message}");
                    break;
            }
        }

        private void ProcessClientCode(CodeGeneratedNotification deserialize)
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

            using (var scope = new TransactionScope())
            {
                connection.Open();
                var isSuccess = connection.Execute(mergeSql, deserialize);
                scope.Complete();
            }
        }

        private void CreateOrder(CreateOrderNotification deserialize)
        {
            var inserOrderSql = @"INSERT INTO loyalty.[Order] 
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

            var insertOrderItemsSql = @"INSERT INTO loyalty.[OrderItem] (Id, ProductId, OrderId, Amount)
                                        VALUES (@Id, @ProductId, @OrderId, @Amount)";

            var orderItems = deserialize.OrderItems;

            using (var scope = new TransactionScope())
            {
                connection.Open();

                var isSuccess = connection.Execute(inserOrderSql, deserialize);
                var isSuccess2 = connection.Execute(insertOrderItemsSql, orderItems);

                scope.Complete();
            }
        }
    }
}