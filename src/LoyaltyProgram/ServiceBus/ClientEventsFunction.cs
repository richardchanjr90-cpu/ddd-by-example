using System;
using System.Data.SqlClient;
using System.Transactions;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using LoyaltyClient.Domain.Handlers.Notifications;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
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
            [ServiceBusTrigger("clientevents", Connection = "ServiceBusConnectionString")]
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
                    throw new NotSupportedException($"Not supported {message}");
            }
        }

        private void ProcessClientCode(CodeGeneratedNotification deserialize)
        {
            var mergeSql = @"MERGE [user].UserCode
                                USING ( 
                                    VALUES (@UserId, @CodeValue, @ExpirationDate)
                                ) AS foo (userId, codeValue, expirationDate) 
                                ON [user].UserCode.userId = foo.userId 
                                WHEN MATCHED THEN
                                   UPDATE SET codeValue = foo.codeValue, expirationDate = foo.expirationDate
                                WHEN NOT MATCHED THEN
                                   INSERT (userId, codeValue, expirationDate)
                                   VALUES (foo.userId, foo.codeValue, foo.expirationDate)
                                ; --A MERGE statement must be terminated by a semi-colon (;).";

            connection.Open();
            using (var scope = new TransactionScope())
            {
                var isSuccess = connection.Execute(mergeSql, deserialize);
                scope.Complete();
            }
        }
    }
}