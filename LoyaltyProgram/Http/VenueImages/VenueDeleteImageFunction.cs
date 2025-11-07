using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenueDeleteImageFunction
    {
        private readonly LoyaltyVenueImageAppService service;

        public VenueDeleteImageFunction(LoyaltyVenueImageAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenueDeleteImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            int index,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [Blob("venue-images-{id}", FileAccess.ReadWrite)]
            CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                var blockBlob = container.GetBlockBlobReference($"original-image-{index}.jpg");
                var mdBlob = container.GetBlockBlobReference($"md-image-{index}.jpg");
                var smBlob = container.GetBlockBlobReference($"sm-image-{index}.jpg");

                Task originalBLobDelete = blockBlob.DeleteIfExistsAsync();
                Task mdBLobDelete = mdBlob.DeleteIfExistsAsync();
                Task smBlobDelete = smBlob.DeleteIfExistsAsync();

                await Task.WhenAll(originalBLobDelete, mdBLobDelete, smBlobDelete);

                return new NoContentResult();
            });
        }
    }
}