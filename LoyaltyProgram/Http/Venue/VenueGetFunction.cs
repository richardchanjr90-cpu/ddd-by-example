using System;
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
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueGetFunction
    {
        [FunctionName("VenueGetFunction")]
        public static async Task<IActionResult> Run(
            string id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venue/{id}")]HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenueGetFunction)} was triggered.");
            var host = new HostConfigurator()
                .Setup<LoyaltyVenueAppService>(context);

            return await ExceptionWrapper.Handle(async () =>
            {
                await req.AuthorizeAsync(host);
                var app = host.StartService<LoyaltyVenueAppService>();
                return new OkObjectResult(await app.Get(Guid.Parse(id)));
            });
        }
    }
}
