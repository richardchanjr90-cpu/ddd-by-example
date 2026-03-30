using System.Transactions;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using LoyaltyClient.Domain.Handlers.Notifications;
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
    }
}