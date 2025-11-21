using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public class LoyaltyProductGroupGetAllFunction
    {
        private readonly LoyaltyProductGroupAppService service;

        public LoyaltyProductGroupGetAllFunction(LoyaltyProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProductGroupGetAllFunction")]
        public async Task<IActionResult> Run(
            long loyaltyProgramId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "programs/{loyaltyProgramId}/loyaltygroups")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupGetAllFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.GetAll(loyaltyProgramId));
            });
        }
    }
}