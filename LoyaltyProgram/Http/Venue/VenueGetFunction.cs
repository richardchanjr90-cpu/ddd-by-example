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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueGetFunction
    {
        [FunctionName("VenueGetFunction")]
        public static async Task<IActionResult> Run(
            Guid id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venue/{id}")]HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenueGetFunction)} was triggered.");

            var di = new HostConfigurator();
            var builder = di.BuildHost(context, log);

            var host = builder.ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<LoyaltyVenueAppService>();
            })
                .ConfigureData()
                .Build();

            host.Start();
            var app = host.StartService<LoyaltyVenueAppService>();

            return new OkObjectResult(await app.Get(id));
        }
    }
}
