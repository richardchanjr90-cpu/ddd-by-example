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
    public class LoyaltyProgramPutFunction
    {
        private readonly LoyaltyProgramAppService service;

        public LoyaltyProgramPutFunction(LoyaltyProgramAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProgramPutFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{venueId}/programs")]
            LoyaltyProgramViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProgramPutFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Update(model, venueId));
            });
        }
    }
}