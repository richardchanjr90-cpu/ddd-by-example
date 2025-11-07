using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Purchase
{
    public class PurchasePostFunction
    {
        private readonly PurchaseAppService service;

        public PurchasePostFunction(PurchaseAppService service)
        {
            this.service = service;
        }

        [FunctionName("PurchasePostFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{venueId}/purchases")]
            PurchaseViewModel model,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchasePostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Purchase(model, venueId));
            });
        }
    }
}