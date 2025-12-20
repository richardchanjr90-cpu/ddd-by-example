using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
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

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("PurchasePostFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{venueId}/purchases")]
            [RequestBodyType(typeof(PurchaseViewModel), "PurchaseViewModel")] PurchaseViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchasePostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Purchase(model, venueId, token.Principal.GetUserId()));
            });
        }
    }
}