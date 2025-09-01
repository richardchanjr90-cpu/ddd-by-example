using System.IO;
using System.Threading.Tasks;
using Loyalty.Storage.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace LoyaltyProgram.Storage
{
    public static class VenueSaveImageFunction
    {
        [FunctionName("VenueSaveImageFunction")]
        public static async Task Run(
            [QueueTrigger("venue-images", Connection = "QueueConnectionString")]VenueImage data,
            [Blob("venue-images-{VenueId}/image", FileAccess.ReadWrite)] Stream blob,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueSaveImageFunction)} was triggered.");
            Image.Load(data.Image).SaveAsJpeg(blob);
        }
    }
}
