using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Product
{
    public static class ProductPostFunction
    {
        [FunctionName("ProductPostFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{venueId}/products")]ProductViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]ProductAppService service)
        {
            log.LogInformation($"{nameof(ProductPostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}
