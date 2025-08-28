using System.Threading.Tasks;
using Loyalty.Core.Shared.Exceptions;
using Loyalty.Venue.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.VenueDetails
{
    public static class VenueDetailsGetFunction
    {
        [FunctionName("VenueDetailsGetFunction")]
        public static async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/details")]HttpRequest req,
            [Inject]LoyaltyVenueDetailsAppService service,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueDetailsGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}
