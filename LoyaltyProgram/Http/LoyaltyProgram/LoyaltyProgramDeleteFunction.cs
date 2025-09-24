using System;
using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.LoyaltyProgram
{
    public static class LoyaltyProgramDeleteFunction
    {
        [FunctionName("LoyaltyProgramDeleteFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{venueId}/programs/{id}")]LoyaltyProgramViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]LoyaltyProgramAppService service)
        {
            log.LogInformation($"{nameof(LoyaltyProgramDeleteFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Archive(id));
            });
        }
    }
}
