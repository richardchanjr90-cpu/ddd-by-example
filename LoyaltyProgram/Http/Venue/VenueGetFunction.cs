using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Auth;
using Loyalty.Core.Auth.Validation;
using Loyalty.Core.Shared;
using Loyalty.Core.Shared.Exceptions;
using Loyalty.Core.Shared.Settings;
using Loyalty.Venue.Service;
using LoyaltyProgram.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                .Setup<LoyaltyVenueAppService>(log, context);

            var config = host.Services.GetService<IConfiguration>();
            var settings = new AuthSettings();
            config.GetSection(nameof(AuthSettings)).Bind(settings);

            var jwt = req.GetJwtTokenOrNull();
            var tokenValidator = new CachedTokenValidator(settings);
            var token = tokenValidator.GetToken(jwt);

            var p = Thread.CurrentPrincipal;

            return await ExceptionWrapper.Handle(async () =>
            {
                var app = host.StartService<LoyaltyVenueAppService>();
                return new OkObjectResult(await app.Get(Guid.Parse(id)));
            });
        }
    }
}
