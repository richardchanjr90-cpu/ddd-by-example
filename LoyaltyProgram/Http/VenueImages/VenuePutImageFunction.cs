using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.VenueImages
{
    public static class VenuePutImageFunction
    {
        [FunctionName("VenuePutImageFunction")]
        public static async Task<IActionResult> Run(
            long id,
            int index,
            [HttpTrigger(AuthorizationLevel.Function, "post, put", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [Inject]LoyaltyVenueImageAppService service,
            [Blob("venue-images-{id}/original-image-{index}.jpg", FileAccess.Write)] Stream blobStream,
            [Queue("venue-images", Connection = "QueueConnectionString")] ICollector<VenueQueueImageDto> queueItems)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //if fails, empty blob is still created;
                //todo: create an issue in github
                // Azure Functions bug
                var images = await service.GetImages(req, id, index);

                if (images != null && images.Count > 0)
                {
                    var imageStream = Image.Load(images.First().Image);
                    imageStream.SaveAsJpeg(blobStream);
                    queueItems.Add(new VenueQueueImageDto
                    {
                        Index = index,
                        VenueId = id
                    });
                }

                return new NoContentResult();
            });
        }
    }
}
