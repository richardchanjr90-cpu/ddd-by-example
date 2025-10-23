using System.IO;
using Loyalty.Application.Storage.Dto;
using Loyalty.Common.Shared.Settings;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace LoyaltyProgram.Storage
{
    public class VenueSaveImageFunction
    {
        private readonly IOptions<VenueGalleryImageSettings> imageSettings;

        public VenueSaveImageFunction(IOptions<VenueGalleryImageSettings> imageSettings)
        {
            this.imageSettings = imageSettings;
        }

        [FunctionName("VenueSaveImageFunction")]
        public void Run(
            [QueueTrigger("venue-images", Connection = "QueueConnectionString")]VenueQueueImageDto data,
            [Blob("venue-images-{VenueId}/original-image-{Index}.jpg", FileAccess.Read)] byte[] originalBlob,
            [Blob("venue-images-{VenueId}/md-image-{Index}.jpg", FileAccess.Write)] Stream mediumBlob,
            [Blob("venue-images-{VenueId}/sm-image-{Index}.jpg", FileAccess.Write)] Stream smallBlob,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueSaveImageFunction)} was triggered.");

            var image = Image.Load(originalBlob);

            var mdWidthMultiplier = image.Width / (float)imageSettings.Value.MdImageWidth;
            var smWidthMultiplier = image.Width / (float)imageSettings.Value.SmImageWidth;

            image.Mutate(ctx => ctx.Resize(
                (int)(image.Width / mdWidthMultiplier),
                (int)(image.Height / mdWidthMultiplier)));
            image.SaveAsJpeg(mediumBlob);

            image.Mutate(ctx => ctx.Resize(
                (int)(image.Width / smWidthMultiplier),
                (int)(image.Height / smWidthMultiplier)));

            image.SaveAsJpeg(smallBlob);
        }
    }
}
