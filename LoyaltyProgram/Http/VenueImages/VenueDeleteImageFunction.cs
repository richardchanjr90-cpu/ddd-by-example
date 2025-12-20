using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenueDeleteImageFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueDeleteImageFunction(LoyaltyVenueImageAppService imageService, LoyaltyVenueAppService service)
        {
            this.imageService = imageService;
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [FunctionName("VenueDeleteImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            string index,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-images-{id}", FileAccess.ReadWrite)]
            CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var blockBlob = container.GetBlockBlobReference($"original-image-{index}.jpg");
                var mdBlob = container.GetBlockBlobReference($"md-image-{index}.jpg");
                var smBlob = container.GetBlockBlobReference($"sm-image-{index}.jpg");

                Task originalBLobDelete = blockBlob.DeleteIfExistsAsync();
                Task mdBLobDelete = mdBlob.DeleteIfExistsAsync();
                Task smBlobDelete = smBlob.DeleteIfExistsAsync();

                await Task.WhenAll(originalBLobDelete, mdBLobDelete, smBlobDelete);

                await service.Patch(
                    id,
                    await imageService.GetImages(container, "original"));

                return new NoContentResult();
            });
        }
    }
}