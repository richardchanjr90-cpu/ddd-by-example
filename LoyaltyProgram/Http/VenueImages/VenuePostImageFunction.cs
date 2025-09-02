using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.VenueImages
{
    public static class VenuePostImageFunction
    {
        [FunctionName("VenuePostImageFunction")]
        public static async Task<IActionResult> Run(
            long id,
            int index,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [Inject]LoyaltyVenueImageAppService service,
            [Queue("venue-images", Connection = "QueueConnectionString")] ICollector<VenueImage> queueItems)
        {
            log.LogInformation($"{nameof(VenuePostImageFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                var images = await service.GetImages(req, id, index);
                foreach (var image in images)
                {
                    queueItems.Add(image);
                }

                return new NoContentResult();
            });
        }
    }
}
