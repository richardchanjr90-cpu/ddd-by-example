using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Order
{
    public class OrderGetFunction
    {
        private readonly OrderAppService service;

        public OrderGetFunction(OrderAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetOrderByVenueIdQueryResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("OrderGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{id}")]
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(OrderGetFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}