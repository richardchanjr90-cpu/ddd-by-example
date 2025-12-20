using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
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

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
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
            log.LogInformation($"{nameof(VenueDeleteLogoFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var uris = await imageService.GetImages(container, "logo");

                foreach (var uri in uris)
                {
                    var blockblob = container.GetBlockBlobReference(new CloudBlockBlob(new Uri(uri)).Name);
                    var p = await blockblob.DeleteIfExistsAsync();
                }

                await service.PatchLogo(id, null);

                return new NoContentResult();
            });
        }
    }
}