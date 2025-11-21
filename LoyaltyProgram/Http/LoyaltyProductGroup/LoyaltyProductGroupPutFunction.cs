using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public class LoyaltyProductGroupPutFunction
    {
        private readonly LoyaltyProductGroupAppService service;

        public LoyaltyProductGroupPutFunction(LoyaltyProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProductGroupPutFunction")]
        public async Task<IActionResult> Run(
            long loyaltyProgramId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "programs/{loyaltyProgramId}/loyaltygroups")]
            LoyaltyProductGroupViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupPutFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}