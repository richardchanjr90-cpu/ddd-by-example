using System;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Product
{
    public static class ProductGetAllFunction
    {
        [FunctionName("ProductGetAllFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{venueId}/products")]
            HttpRequest req,
            ILogger log,
            [Inject]ProductAppService service)
        {
            log.LogInformation($"{nameof(ProductGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get());
            });
        }
    }
}
