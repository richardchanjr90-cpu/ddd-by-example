using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.ProductGroup
{
    public static class ProductGroupGetAllFunction
    {
        [FunctionName("ProductGroupGetAllFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/productgroups")]
            HttpRequest req,
            ILogger log,
            [Inject]ProductGroupAppService service)
        {
            log.LogInformation($"{nameof(ProductGroupGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.GetAll(venueId));
            });
        }
    }
}
