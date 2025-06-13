using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LoyaltyProgram
{
    public static class VenueHttpFunction
    {
        [FunctionName("VenueHttpFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation($"{nameof(VenueHttpFunction)} was triggered.");

            string id = req.Query["id"];

            switch (req.Method)
            {
                case "GET":

                    log.LogInformation($"Get method was used to invoke the function ({req.Method}) with {id}");
                    break;

                case "POST":

                    string requestBody = new StreamReader(req.Body).ReadToEnd();
                    dynamic data = JsonConvert.DeserializeObject(requestBody);

                    log.LogInformation($"Post method was used to invoke the function ({req.Method}) with {data}");

                    break;

                case "PUT":

                    log.LogInformation($"Put method was used to invoke the function ({req.Method}) with {id}");
                    break;

                case "DELETE":

                    log.LogInformation($"Delete method was used to invoke the function ({req.Method}) with {id}");
                    break;

                default:
                    throw new NotSupportedException("Rest method is not supported.");
            }

            return new OkObjectResult("Ok");
        }

        public static Task Get()
        {
            throw new NotImplementedException();
        }

        public static Task GetAll()
        {
            throw new NotImplementedException();
        }

        public static Task Create()
        {
            throw new NotImplementedException();
        }

        public static Task Update()
        {
            throw new NotImplementedException();
        }

        public static Task Delete()
        {
            throw new NotImplementedException();
        }
    }
}
