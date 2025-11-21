using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public class LoyaltyProductGroupPostFunction
    {
        private readonly LoyaltyProductGroupAppService service;

        public LoyaltyProductGroupPostFunction(LoyaltyProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProductGroupPostFunction")]
        public async Task<IActionResult> Run(
            long loyaltyProgramId,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "programs/{loyaltyProgramId}/loyaltygroups")]
            LoyaltyProductGroupViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupPostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}