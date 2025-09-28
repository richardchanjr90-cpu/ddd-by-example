using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.ProductGroup
{
    public static class ProductGroupPostFunction
    {
        [FunctionName("ProductGroupPostFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{venueId}/productgroups")]ProductGroupViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]ProductGroupAppService service)
        {
            model = await req.Cast<ProductGroupViewModel>();
            log.LogInformation($"{nameof(ProductGroupPostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Create(model, venueId));
            });
        }
    }
}
