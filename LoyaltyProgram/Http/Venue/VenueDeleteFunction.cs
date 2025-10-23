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
    public static class VenueDeleteFunction
    {
        [FunctionName("VenueDeleteFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venue/{id}")]HttpRequest req,
            long id,
            ILogger log,
            [Inject]LoyaltyVenueAppService service)
        {
            log.LogInformation($"{nameof(VenueDeleteFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Archive(id));
            });
        }
    }
}
