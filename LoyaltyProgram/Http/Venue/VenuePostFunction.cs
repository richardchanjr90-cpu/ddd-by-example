using System;
using System.Threading.Tasks;
using Loyalty.Core.Shared;
using Loyalty.Core.Shared.Exceptions;
using Loyalty.Core.ViewModels;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Venue.Service;
using LoyaltyProgram.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenuePostFunction
    {
        [FunctionName("VenuePostFunction")]

        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venue")]VenueViewModel model,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenuePostFunction)} was triggered.");

            var host = new HostConfigurator()
                .Setup<LoyaltyVenueAppService>(log, context);

            return await ExceptionWrapper.Handle(async () =>
            {
                var app = host.StartService<LoyaltyVenueAppService>();
                return new OkObjectResult(await app.Create(model));
            });
        }
    }
}
