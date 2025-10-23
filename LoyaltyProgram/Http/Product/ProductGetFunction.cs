using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Product
{
    public class ProductGetFunction
    {
        private readonly ProductAppService service;

        public ProductGetFunction(ProductAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            long groupId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "productGroups/{groupId}/products/{id}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}