using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public class VenueGetAllFunction
    {
        private readonly LoyaltyVenueAppService service;

        public VenueGetAllFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenueGetAllFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "venues")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueGetAllFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                 return new OkObjectResult(await service.Get(token.Principal.GetUserId()));
             });
        }
    }
}