using System.Threading.Tasks;
using Loyalty.Core.Shared;
using Loyalty.Core.Shared.Exceptions;
using Loyalty.Venue.Service;
using LoyaltyProgram.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueGetAllFunction
    {
        [FunctionName("VenueGetAllFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "venue")]HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenueGetAllFunction)} was triggered.");

            var host = new HostConfigurator()
                .Setup<LoyaltyVenueAppService>(log, context);

            return await ExceptionWrapper.Handle(async () =>
            {
                await req.AuthorizeAsync(host);
                var app = host.StartService<LoyaltyVenueAppService>();
                return new OkObjectResult(await app.Get());
            });
        }
    }
}
