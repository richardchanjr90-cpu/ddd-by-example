using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Read.Admin
{
    public class VersionSupportGetFunction
    {
        [FunctionName("VersionSupportGetFunction")]
        public IActionResult Run(
            int major,
            int minor,
            int build,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "control/admins/version/{major}/{minor}/{build}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(VersionSupportGetFunction)} was triggered.");

            return new OkObjectResult(true);
        }
    }
}