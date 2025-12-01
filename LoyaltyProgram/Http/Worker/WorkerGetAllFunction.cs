using System.Security.Claims;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Worker
{
    public class WorkerGetAllFunction
    {
        private readonly WorkerAppService service;

        public WorkerGetAllFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [FunctionName("WorkerGetAllFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "workers")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ClaimsPrincipal principal,
            ILogger log)
        {
            log.LogInformation($"{nameof(WorkerGetAllFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Get(token.Principal.GetUserId()));
            });
        }
    }
}