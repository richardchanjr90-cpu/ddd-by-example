using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Product
{
    public class ProductDeleteFunction
    {
        private readonly ProductAppService service;

        public ProductDeleteFunction(ProductAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductDeleteFunction")]
        public async Task<IActionResult> Run(
            long groupId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "productGroups/{groupId}/products/{id}")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductDeleteFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Archive(id));
            });
        }
    }
}