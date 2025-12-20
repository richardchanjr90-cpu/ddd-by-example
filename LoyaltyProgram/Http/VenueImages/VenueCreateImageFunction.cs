using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenueCreateImageFunction
    {
        private readonly IOptions<VenueGalleryImageSettings> imageSettings;
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueCreateImageFunction(
            IOptions<VenueGalleryImageSettings> imageSettings,
            LoyaltyVenueAppService service,
            LoyaltyVenueImageAppService imageService)
        {
            this.imageSettings = imageSettings;
            this.service = service;
            this.imageService = imageService;
        }

        [FunctionName("VenueCreateImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/details/images")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-images-{id}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenueCreateImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var items = await imageService.GetCount(container);
                var image = await imageService.ConvertImage(req, id, Guid.NewGuid());

                if (items + 1 > 10)
                {
                    return new BadRequestErrorMessageResult("Cannot create more than 10 images");
                }

                using (var stream = new MemoryStream())
                using (var mdStream = new MemoryStream())
                using (var smStream = new MemoryStream())
                {
                    await container.CreateIfNotExistsAsync();
                    var blob = container.GetBlockBlobReference($"original-image-{image.Index}.jpg");
                    var mdBlob = container.GetBlockBlobReference($"md-image-{image.Index}.jpg");
                    var smBlob = container.GetBlockBlobReference($"sm-image-{image.Index}.jpg");

                    imageService.SaveImageOfWidthToStream(
                        stream,
                        image.Image);

                    imageService.SaveImageOfWidthToStream(
                        mdStream,
                        image.Image,
                        imageSettings.Value.MdImageWidth);

                    imageService.SaveImageOfWidthToStream(
                        smStream,
                        image.Image,
                        imageSettings.Value.SmImageWidth);

                    await blob.UploadFromStreamAsync(stream);
                    await mdBlob.UploadFromStreamAsync(mdStream);
                    await smBlob.UploadFromStreamAsync(smStream);

                    await service.Patch(
                        id,
                        await imageService.GetImages(container, "original"));
                }

                return new NoContentResult();
            });
        }
    }
}
