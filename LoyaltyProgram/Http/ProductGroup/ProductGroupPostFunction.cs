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
    public class ProductGroupPostFunction
    {
        private readonly ProductGroupAppService service;

        public ProductGroupPostFunction(ProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductGroupPostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "productgroups")]ProductGroupViewModel model,
            HttpRequest req,
            ILogger log)
        {
            model = await req.Cast<ProductGroupViewModel>();
            log.LogInformation($"{nameof(ProductGroupPostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}
