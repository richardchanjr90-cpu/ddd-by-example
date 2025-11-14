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
    public class ProductGroupGetFunction
    {
        private readonly ProductGroupAppService service;

        public ProductGroupGetFunction(ProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductGroupGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "productgroups/{id}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductGroupGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () => { return new OkObjectResult(await service.Get(id)); });
        }
    }
}