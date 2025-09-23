using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Purchase
{
    public static class PurchasePostFunction
    {
        [FunctionName("PurchasePostFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "purchase")]PurchaseViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]PurchaseAppService service)
        {
            log.LogInformation($"{nameof(PurchasePostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new NoContentResult();
            });
        }
    }
}
