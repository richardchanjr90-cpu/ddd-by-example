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
    public static class LoyaltyProductGroupGetAllFunction
    {
        [FunctionName("LoyaltyProductGroupGetAllFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/programs")]
            HttpRequest req,
            ILogger log,
            [Inject]LoyaltyProductGroupAppService service)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get());
            });
        }
    }
}
