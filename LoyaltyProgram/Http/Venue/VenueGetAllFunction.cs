using System.Threading.Tasks;
using Loyalty.Core.Shared.Exceptions;
using Loyalty.Venue.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueGetAllFunction
    {
        [FunctionName("VenueGetAllFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venue")]HttpRequest req,
            ILogger log,
            [Inject]LoyaltyVenueAppService service)
        {
            log.LogInformation($"{nameof(VenueGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Get());
            });
        }
    }
}
