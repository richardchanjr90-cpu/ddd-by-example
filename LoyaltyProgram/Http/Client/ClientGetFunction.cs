using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Client
{
    public static class ClientGetFunction
    {
        [FunctionName("ClientGetFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "client")]string userCode,
            HttpRequest req,
            ILogger log,
            [Inject]ClientAppService service)
        {
            log.LogInformation($"{nameof(ClientGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new NoContentResult();
            });
        }
    }
}
