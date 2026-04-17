using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Purchase
{
    public class PurchasePostFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly PurchaseAppService service;

        public PurchasePostFunction(PurchaseAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
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
            [Queue("purchase-notification", Connection = "QueueConnectionString")] ICollector<PurchaseNotificationDto> queueItems,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchasePostFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                token.Principal.IsInRoleAndThrow(venueId);
                var result = await service.Purchase(model, venueId, token.Principal.GetUserId());
                
                if (result.Success)
                {
                    var message = $"Успешно начислено {model.Value} баллов!";

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