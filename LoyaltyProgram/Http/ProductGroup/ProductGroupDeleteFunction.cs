using System;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.ProductGroup
{
    public static class ProductGroupDeleteFunction
    {
        [FunctionName("ProductGroupDeleteFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{venueId}/productgroups/{id}")]ProductGroupViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]ProductGroupAppService service)
        {
            log.LogInformation($"{nameof(ProductGroupDeleteFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Archive(id));
            });
        }
    }
}
