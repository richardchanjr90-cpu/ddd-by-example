using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram
{
    public static class VenueHttpFunction
    {
        [FunctionName("VenueHttpFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation($"{nameof(VenueHttpFunction)} was triggered.");

            string id = req.Query["id"];

            switch (req.Method.ToLower())
            {
                case "get":

                    log.LogInformation($"Get method was used to invoke the function ({req.Method}) with {id}");
                    break;

                case "post":

                    string requestBody = new StreamReader(req.Body).ReadToEnd();
                    dynamic data = JsonConvert.DeserializeObject(requestBody);

                    log.LogInformation($"Post method was used to invoke the function ({req.Method}) with {data}");

                    break;

                case "put":

                    log.LogInformation($"Put method was used to invoke the function ({req.Method}) with {id}");
                    break;

                case "delete":

                    log.LogInformation($"Delete method was used to invoke the function ({req.Method}) with {id}");
                    break;

                default:
                    throw new NotSupportedException("Rest method is not supported.");
            }

            return new OkObjectResult("Ok");
        }
    }
}
