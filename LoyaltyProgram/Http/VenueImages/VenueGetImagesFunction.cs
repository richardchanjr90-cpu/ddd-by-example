using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenueGetImagesFunction
    {
        [FunctionName("VenueGetImagesFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/details/images")]
            HttpRequest req,
            ILogger log,
            [Blob("venue-images-{id}", FileAccess.Read)]
            CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenueGetImagesFunction)} was triggered.");
            var results = new List<string>();
            var exists = await container.ExistsAsync();

            if (exists)
            {
                var operation =
                    await container.ListBlobsSegmentedAsync(req.Query["prefix"], true, BlobListingDetails.None, 50,
                        null, null, null);
                results = operation.Results.Select(item => item.Uri.ToString()).ToList();
            }

            return await ExceptionWrapper.Handle(async () => { return new OkObjectResult(results); });
        }
    }
}