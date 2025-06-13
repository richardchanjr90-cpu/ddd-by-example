using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LoyaltyProgram.Venue
{
    public static class VenueHttpPostFunction
    {
        [FunctionName("VenueHttpPostFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "venue")]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            throw new NotImplementedException();
        }
    }
}
