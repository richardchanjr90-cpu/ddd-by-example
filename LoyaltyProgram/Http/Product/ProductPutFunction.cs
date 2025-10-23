using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Product
{
    public class ProductPutFunction
    {
        private readonly ProductAppService service;

        public ProductPutFunction(ProductAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductPutFunction")]
        public async Task<IActionResult> Run(
            long groupId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "productGroups/{groupId}/products")]ProductViewModel model,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}
