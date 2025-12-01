using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Product
{
    public class ProductPostFunction
    {
        private readonly ProductAppService service;

        public ProductPostFunction(ProductAppService service)
        {
            this.service = service;
        }

        [FunctionName("ProductPostFunction")]
        public async Task<IActionResult> Run(
            long groupId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "productGroups/{groupId}/products")]
            ProductViewModel model,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductPostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Create(model, groupId));
            });
        }
    }
}