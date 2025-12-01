using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Http.VenueImages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueLogo
{
    public class VenueDeleteLogoFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueDeleteLogoFunction(LoyaltyVenueImageAppService imageService, LoyaltyVenueAppService service)
        {
            this.imageService = imageService;
            this.service = service;
        }

        [FunctionName("VenueDeleteLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-logo-{id}", FileAccess.ReadWrite)]
            CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var blockBlob = container.GetBlockBlobReference($"logo.jpg");
                Task originalBLobDelete = blockBlob.DeleteIfExistsAsync();

                await Task.WhenAll(originalBLobDelete);

                var url = (await imageService.GetImages(container, null)).FirstOrDefault();
                await service.Patch(id, url);

                return new NoContentResult();
            });
        }
    }
}