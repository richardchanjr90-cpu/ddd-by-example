using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public class LoyaltyProductGroupGetFunction
    {
        private readonly LoyaltyProductGroupAppService service;

        public LoyaltyProductGroupGetFunction(LoyaltyProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProductGroupGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            long loyaltyProgramId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "programs/{loyaltyProgramId}/loyaltygroups/{id}")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupGetFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Get(id, loyaltyProgramId));
            });
        }
    }
}