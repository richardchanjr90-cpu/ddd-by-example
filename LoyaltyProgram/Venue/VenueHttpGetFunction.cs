using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Venue
{
    public static class VenueHttpGetFunction
    {
        [FunctionName("VenueHttpGetFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "venue/{id}")]HttpRequest req, int id, ILogger log)
        {
            log.LogInformation($"{nameof(VenueHttpGetFunction)} was triggered.");

            throw new NotImplementedException();
        }
    }
}
