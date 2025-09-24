using System;
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
    public static class PurchaseBurnPutFunction
    {
        [FunctionName("PurchaseBurnPutFunction")]
        public static async Task<IActionResult> Run(
            long id,
            string guid,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/purchases/users/{guid}")]PurchaseViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]PurchaseAppService service)
        {
            log.LogInformation($"{nameof(PurchaseBurnPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Burn(model, Guid.Parse(guid), id));
            });
        }
    }
}
