using System;
using System.Threading.Tasks;
using Loyalty.Core.Shared;
using Loyalty.Domain.Handlers;
using Loyalty.Venue.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenueDeleteFunction
    {
        [FunctionName("VenueDeleteFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venue/{id}")]HttpRequest req,
            int id,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenueDeleteFunction)} was triggered.");

            var di = new HostConfigurator();
            var builder = di.BuildHost(context, log);

            var host = builder.ConfigureServices((hostContext, services) =>
            {
                services.AddMediatR(typeof(BaseHandler).Assembly);
                services.AddScoped<LoyaltyVenueAppService>();
            }).Build();

            host.Start();

            var app = (LoyaltyVenueAppService)host.Services.GetService(typeof(LoyaltyVenueAppService));
            return new OkObjectResult(app.Delete(id));
        }
    }
}
