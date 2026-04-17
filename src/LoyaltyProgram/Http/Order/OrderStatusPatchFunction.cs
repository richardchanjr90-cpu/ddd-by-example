using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto.Orders;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Orders;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Order
{
    public class OrderStatusPatchFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly OrderAppService service;

        public OrderStatusPatchFunction(OrderAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("OrderStatusPatchFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "orders")]
            [RequestBodyType(typeof(PatchOrderStatusViewModel), "PatchOrderStatusViewModel")] PatchOrderStatusViewModel model,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager), nameof(VenueUserRole.Worker))] FunctionTokenResult token,
            [Queue("changedorder-notification", Connection = "QueueConnectionString")] ICollector<OrderChangedDto> queueItems,
            ILogger log)
        {
            log.LogInformation($"{nameof(OrderStatusPatchFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var order = await service.Get(model.OrderId);
                var result = await service.PatchStatus(model);

                if (result.Success)
                {
                    queueItems.Add(new OrderChangedDto
                    {
                        UserId = order.CustomerId,
                        Id = model.OrderId,
                        VenueComment = model.VenueComment,
                        ChangedStatus = (OrderStatus)model.Status
                    });
                }

                return new OkObjectResult(result);
            });
        }
    }
}