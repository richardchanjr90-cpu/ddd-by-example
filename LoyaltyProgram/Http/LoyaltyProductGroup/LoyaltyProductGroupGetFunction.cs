using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public static class LoyaltyProductGroupGetFunction
    {
        [FunctionName("LoyaltyProductGroupGetFunction")]
        public static async Task<IActionResult> Run(
            long id,
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/loyaltygroups/{id}")]
            HttpRequest req,
            ILogger log,
            [Inject]LoyaltyProductGroupAppService service)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}