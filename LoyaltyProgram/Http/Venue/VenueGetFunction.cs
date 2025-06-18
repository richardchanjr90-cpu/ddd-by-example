using System;
using System.Threading.Tasks;
using Loyalty.Core.Shared;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Domain.Handlers;
using Loyalty.Venue.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueGetFunction
    {
        [FunctionName("VenueGetFunction")]
        public static async Task<IActionResult> Run(
            int id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venue/{id:int}")]HttpRequest req,
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

            var app = (LoyaltyVenueAppService)host.Services.GetService(typeof(LoyaltyVenueAppService));

            return new OkObjectResult(await app.Get(id));
        }
    }
}
