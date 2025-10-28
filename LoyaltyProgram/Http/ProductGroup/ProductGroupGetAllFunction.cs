using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.ProductGroup
{
    public class ProductGroupGetAllFunction
    {
        private readonly ProductGroupAppService service;

        public ProductGroupGetAllFunction(ProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductGroupGetAllFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "productgroups")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductGroupGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get());
            });
        }
    }
}
