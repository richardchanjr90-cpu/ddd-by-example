using System;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Purchase
{
    public class PurchaseGetActiveFunction
    {
        private readonly PurchaseAppService service;

        public PurchaseGetActiveFunction(PurchaseAppService service)
        {
            this.service = service;
        }

        [FunctionName("PurchaseGetActiveFunction")]
        public async Task<IActionResult> Run(
            long id,
            string guid,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/purchases/users/{guid}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchaseGetActiveFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.GetActivePurchases(Guid.Parse(guid), id));
            });
        }
    }
}
