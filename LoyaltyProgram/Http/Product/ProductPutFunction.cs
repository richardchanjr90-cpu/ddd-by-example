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
    public static class ProductPutFunction
    {
        [FunctionName("ProductPutFunction")]
        public static async Task<IActionResult> Run(
            long groupId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "productGroups/{groupId}/products")]ProductViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]ProductAppService service)
        {
            log.LogInformation($"{nameof(ProductPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}
