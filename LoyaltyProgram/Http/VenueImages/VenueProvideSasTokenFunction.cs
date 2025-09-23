using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueImages
{
    public static class VenueProvideSasTokenFunction
    {
        [FunctionName("VenueProvideSasTokenFunction")]
        public static async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "security/sas/venues/{id}")]
            HttpRequest req,
            [Blob("venue-images-{id}", FileAccess.Read)] CloudBlobContainer container,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueProvideSasTokenFunction)} was triggered.");

            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                SharedAccessStartTime = DateTime.Now
            };

            var sas = container.GetSharedAccessSignature(policy);

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(sas);
            });
        }
    }
}
