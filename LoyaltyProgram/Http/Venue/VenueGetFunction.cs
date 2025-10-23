using System;
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
    public static class VenueGetFunction
    {
        [FunctionName("VenueGetFunction")]
        public static async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venue/{id}")]HttpRequest req,
            [Inject]LoyaltyVenueAppService service,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}
