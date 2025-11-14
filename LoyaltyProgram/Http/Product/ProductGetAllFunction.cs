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
    public class ProductGetAllFunction
    {
        private readonly ProductAppService service;

        public ProductGetAllFunction(ProductAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductGetAllFunction")]
        public async Task<IActionResult> Run(
            long groupId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "productGroups/{groupId}/products")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.GetAll(groupId));
            });
        }
    }
}