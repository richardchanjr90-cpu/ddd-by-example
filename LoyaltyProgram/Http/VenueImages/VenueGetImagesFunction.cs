using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.VenueImages
{
    public static class VenueGetImagesFunction
    {
        [FunctionName("VenueGetImagesFunction")]
        public static async Task<IActionResult> Run(
            long id,
            int index,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [Inject]LoyaltyVenueImageAppService service)
        {
            log.LogInformation($"{nameof(VenueGetImagesFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new NoContentResult();
            });
        }
    }
}
