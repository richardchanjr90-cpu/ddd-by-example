using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenuePutImageFunction
    {
        private readonly IOptions<VenueGalleryImageSettings> imageSettings;
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenuePutImageFunction(
            IOptions<VenueGalleryImageSettings> imageSettings,
            LoyaltyVenueAppService service,
            LoyaltyVenueImageAppService imageService)
        {
            this.imageSettings = imageSettings;
            this.service = service;
            this.imageService = imageService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenuePutImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            string index,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-images-{id}/original-image-{index}.jpg", FileAccess.Write)] Stream originalBlob,
            [Blob("venue-images-{id}/md-image-{index}.jpg", FileAccess.Write)] Stream mediumBlob,
            [Blob("venue-images-{id}/sm-image-{index}.jpg", FileAccess.Write)] Stream smallBlob)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await Handler.WrapAsync(log, token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var image = await imageService.ConvertImage(req, id, Guid.Parse(index));

                if (image != null)
                {
                    imageService.SaveImageOfWidthToStream(
                        originalBlob, 
                        image.Image);

                    imageService.SaveImageOfWidthToStream(
                        mediumBlob, 
                        image.Image, 
                        imageSettings.Value.MdImageWidth);

                    imageService.SaveImageOfWidthToStream(
                        smallBlob, 
                        image.Image, 
                        imageSettings.Value.SmImageWidth);
                }

                log.LogDebug($"Venue Image created for: {id}", id);
                return new NoContentResult();
            });
        }
    }
}
