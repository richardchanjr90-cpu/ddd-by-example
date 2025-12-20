using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.Purchase;
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

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActivePurchasesViewModel[]))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [FunctionName("PurchaseGetActiveFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            string guid,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/purchases/users/{guid}")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchaseGetActiveFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.GetActivePurchases(guid, venueId));
            });
        }
    }
}