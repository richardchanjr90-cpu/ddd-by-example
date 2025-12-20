using System;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;

namespace LoyaltyProgram.Http.VenueSas
{
    public class VenueProvideSasTokenFunction
    {
        [FunctionName("VenueProvideSasTokenFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "security/sas/")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueProvideSasTokenFunction)} was triggered.");

            var connectionString = Environment.GetEnvironmentVariable("QueueConnectionString");

            var policy = new SharedAccessAccountPolicy
            {
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Container |
                                SharedAccessAccountResourceTypes.Object |
                                SharedAccessAccountResourceTypes.Service,
                Permissions = SharedAccessAccountPermissions.Read |
                              SharedAccessAccountPermissions.List |
                              SharedAccessAccountPermissions.ProcessMessages,
                SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                SharedAccessStartTime = DateTime.Now,
                Protocols = SharedAccessProtocol.HttpsOnly
            };

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var sas = storageAccount.GetSharedAccessSignature(policy);

            return await Handler.WrapAsync(log, token, async () =>
            {
                return new OkObjectResult(sas);
            });
        }
    }
}