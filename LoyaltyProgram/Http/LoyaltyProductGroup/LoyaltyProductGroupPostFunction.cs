using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public static class LoyaltyProductGroupPostFunction
    {
        [FunctionName("LoyaltyProductGroupPostFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{venueId}/loyaltygroups")]LoyaltyProductGroupViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]LoyaltyProductGroupAppService service)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupPostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}
