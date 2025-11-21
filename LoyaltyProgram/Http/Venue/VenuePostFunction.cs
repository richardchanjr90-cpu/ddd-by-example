using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public class VenuePostFunction
    {
        private readonly LoyaltyVenueAppService service;

        public VenuePostFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenuePostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues")]
            VenueViewModel model,
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenuePostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                model = await req.Cast<VenueViewModel>();
                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}