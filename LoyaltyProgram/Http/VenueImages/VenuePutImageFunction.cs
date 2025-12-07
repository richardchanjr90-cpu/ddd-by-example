using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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

        [FunctionName("VenuePutImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            string index,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-images-{id}/original-image-{index}.jpg", FileAccess.Write)] Stream blobStream,
            [Blob("venue-images-{id}/md-image-{index}.jpg", FileAccess.Write)] Stream mediumBlob,
            [Blob("venue-images-{id}/sm-image-{index}.jpg", FileAccess.Write)] Stream smallBlob)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var images = await imageService.ConvertImages(req, id, Guid.Parse(index));

                if (images != null && images.Count > 0)
                {
                    var imageStream = Image.Load(images.First().Image);
                    imageStream.SaveAsJpeg(blobStream);

                    var mdWidthMultiplier = imageStream.Width / (float)imageSettings.Value.MdImageWidth;
                    var smWidthMultiplier = imageStream.Width / (float)imageSettings.Value.SmImageWidth;

                    imageStream.Mutate(ctx => ctx.Resize(
                        (int)(imageStream.Width / mdWidthMultiplier),
                        (int)(imageStream.Height / mdWidthMultiplier)));
                    imageStream.SaveAsJpeg(mediumBlob);

                    imageStream.Mutate(ctx => ctx.Resize(
                        (int)(imageStream.Width / smWidthMultiplier),
                        (int)(imageStream.Height / smWidthMultiplier)));
                    imageStream.SaveAsJpeg(smallBlob);
                }

                return new NoContentResult();
            });
        }
    }
}
