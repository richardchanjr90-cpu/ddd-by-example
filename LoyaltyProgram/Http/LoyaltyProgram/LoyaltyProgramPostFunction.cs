using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProgram
{
    public class LoyaltyProgramPostFunction
    {
        private readonly LoyaltyProgramAppService service;

        public LoyaltyProgramPostFunction(LoyaltyProgramAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProgramPostFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{venueId}/programs")]
            LoyaltyProgramViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProgramPostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Create(model, venueId));
            });
        }
    }
}