using System;
using System.Net;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using LoyaltyProgram.Http.Venue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Ping
{
    public class PingFunction
    {
        private readonly LoyaltyVenueAppService service;

        public PingFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [FunctionName("PingFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ping")]
            ILogger log)
        {
            log.LogInformation($"{nameof(PingFunction)} was triggered.");

            var result = await service.GetByUser();

            return new OkObjectResult(result);
        }
    }
}