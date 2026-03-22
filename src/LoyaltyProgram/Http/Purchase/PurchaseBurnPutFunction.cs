using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Infrastructure.IoC;
using MediatR.Extensions.UnitOfWork.Interface;
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

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("PurchaseBurnPutFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{venueId}/purchases")]
            [RequestBodyType(typeof(PurchaseViewModel), "PurchaseViewModel")] PurchaseViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log,
            [Queue("purchase-notification", Connection = "QueueConnectionString")] ICollector<PurchaseNotificationDto> queueItems)
        {
            log.LogInformation($"{nameof(PurchaseBurnPutFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var result = await service.Burn(model, venueId, token.Principal.GetUserId());
                
                if (result.Success)
                {
                    var message = $"Успешно списано {model.Value} баллов!";
                    queueItems.Add(new PurchaseNotificationDto()
                    {
                        Message = message,
                        UserId = model.UserId
                    });
                }

                return new OkObjectResult(result);
            });
        }
    }
}