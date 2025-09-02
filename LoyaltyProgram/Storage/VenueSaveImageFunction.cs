using System;
using System.IO;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace LoyaltyProgram.Storage
{
    public static class VenueSaveImageFunction
    {
        [FunctionName("VenueSaveImageFunction")]
        public static async Task Run(
            [QueueTrigger("venue-images", Connection = "QueueConnectionString")]VenueImage data,
            [Blob("venue-images-{VenueId}/image-{Index}.Jpeg", FileAccess.Write)] Stream originalBlob,
            [Blob("venue-images-{VenueId}/image-md-{Index}.Jpeg", FileAccess.Write)] Stream mediumBlob,
            [Blob("venue-images-{VenueId}/image-sm-{Index}.Jpeg", FileAccess.Write)] Stream smallBlob,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueSaveImageFunction)} was triggered.");

            var image = Image.Load(data.Image);
            image.SaveAsJpeg(originalBlob);

            image.Mutate(ctx => ctx.Resize(image.Width / 2, image.Height / 2));
            image.SaveAsJpeg(mediumBlob);

            image.Mutate(ctx => ctx.Resize(image.Width / 2, image.Height / 2));
            image.SaveAsJpeg(smallBlob);
        }
    }
}
