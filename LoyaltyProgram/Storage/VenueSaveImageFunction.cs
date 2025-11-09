using System.IO;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Settings;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace LoyaltyProgram.Storage
{
    public class VenueSaveImageFunction
    {
        private readonly IOptions<VenueGalleryImageSettings> imageSettings;
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueSaveImageFunction(
            IOptions<VenueGalleryImageSettings> imageSettings, 
            LoyaltyVenueAppService service,
            LoyaltyVenueImageAppService imageService)
        {
            this.imageSettings = imageSettings;
            this.service = service;
            this.imageService = imageService;
        }

        [FunctionName("VenueSaveImageFunction")]
        public async Task Run(
            [QueueTrigger("venue-images", Connection = "QueueConnectionString")]
            VenueNewBlobImageDto data,
            [Blob("venue-images-{VenueId}/original-image-{Index}.jpg", FileAccess.Read)]
            byte[] originalBlob,
            [Blob("venue-images-{VenueId}/md-image-{Index}.jpg", FileAccess.Write)]
            Stream mediumBlob,
            [Blob("venue-images-{VenueId}/sm-image-{Index}.jpg", FileAccess.Write)]
            Stream smallBlob,
            [Blob("venue-images-{VenueId}", FileAccess.Read)]
            CloudBlobContainer container,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueSaveImageFunction)} was triggered.");

            var image = Image.Load(originalBlob);

            var mdWidthMultiplier = image.Width / (float) imageSettings.Value.MdImageWidth;
            var smWidthMultiplier = image.Width / (float) imageSettings.Value.SmImageWidth;

            image.Mutate(ctx => ctx.Resize(
                (int) (image.Width / mdWidthMultiplier),
                (int) (image.Height / mdWidthMultiplier)));
            image.SaveAsJpeg(mediumBlob);

            image.Mutate(ctx => ctx.Resize(
                (int) (image.Width / smWidthMultiplier),
                (int) (image.Height / smWidthMultiplier)));

            image.SaveAsJpeg(smallBlob);

            await service.Patch(
                data.VenueId, 
                await imageService.GetImages(container, "original"));
        }
    }
}