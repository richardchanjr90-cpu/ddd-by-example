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
    public static class ProductGetFunction
    {
        [FunctionName("ProductGetFunction")]
        public static async Task<IActionResult> Run(
            long id,
            long groupId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "productGroups/{groupId}/products/{id}")]
            HttpRequest req,
            ILogger log,
            [Inject]ProductAppService service)
        {
            log.LogInformation($"{nameof(ProductGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}