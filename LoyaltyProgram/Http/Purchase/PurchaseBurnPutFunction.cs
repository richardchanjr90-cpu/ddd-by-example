using System;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Purchase
{
    public class PurchaseBurnPutFunction
    {
        private readonly PurchaseAppService service;

        public PurchaseBurnPutFunction(PurchaseAppService service)
        {
            this.service = service;
        }

        [FunctionName("PurchaseBurnPutFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{venueId}/purchases")]PurchaseViewModel model,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchaseBurnPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Burn(model, venueId));
            });
        }
    }
}
