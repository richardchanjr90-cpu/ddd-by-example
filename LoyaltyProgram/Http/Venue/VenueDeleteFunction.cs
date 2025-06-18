using System;
using System.Threading.Tasks;
using Loyalty.Core.Shared;
using Loyalty.Venue.Service;
using LoyaltyProgram.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueDeleteFunction
    {
        [FunctionName("VenueDeleteFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venue/{id:guid}")]HttpRequest req,
            Guid id,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenueDeleteFunction)} was triggered.");

            var di = new HostConfigurator();
            var builder = di.BuildHost(context, log);

            var host = builder.ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<LoyaltyVenueAppService>();
            })
                .ConfigureData()
                .Build();

            var app = host.StartService<LoyaltyVenueAppService>();
            return new OkObjectResult(await app.Delete(id));
        }
    }
}
