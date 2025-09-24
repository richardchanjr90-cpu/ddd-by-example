using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Purchase
{
    public static class PurchaseGetActiveFunction
    {
        [FunctionName("PurchaseGetActiveFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "purchases")]string userCode,
            HttpRequest req,
            ILogger log,
            [Inject]PurchaseAppService service)
        {
            log.LogInformation($"{nameof(PurchaseGetActiveFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new NoContentResult();
            });
        }
    }
}
