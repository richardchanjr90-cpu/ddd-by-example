using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Client
{
    public class ClientInfoGetFunction
    {
        private readonly ClientInfoAppService service;

        public ClientInfoGetFunction(ClientInfoAppService service)
        {
            this.service = service;
        }

        [RequestHttpHeader("Authorization", true)]
        [FunctionName("ClientInfoGetFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "clients/{userId}")]
            HttpRequest req,
            string userId,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(ClientInfoGetFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(
                log,
                token,
                async () => new OkObjectResult(await service.Get(userId)));
        }
    }
}