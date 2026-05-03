using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Read.Ping
{
    public class LoaderIoFunction
    {
        [FunctionName("LoaderIoFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "loaderio-05c951e4f59a15bcb4003500bfa99a94")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoaderIoFunction)} was triggered.");
            var bytes = Encoding.UTF8.GetBytes("loaderio-05c951e4f59a15bcb4003500bfa99a94");
            return new FileContentResult(bytes, "text/plain");
        }
    }
}