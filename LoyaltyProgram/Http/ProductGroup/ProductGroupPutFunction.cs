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

namespace LoyaltyProgram.Http.ProductGroup
{
    public class ProductGroupPutFunction
    {
        private readonly ProductGroupAppService service;

        public ProductGroupPutFunction(ProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductGroupPutFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{venueId}/productgroups")]ProductGroupViewModel model,
            HttpRequest req,
            ILogger log)
        {
            model = await req.Cast<ProductGroupViewModel>();
            log.LogInformation($"{nameof(ProductGroupPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Update(model, venueId));
            });
        }
    }
}
