using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.VenueDetails
{
    public static class VenueDetailsPutFunction
    {
        [FunctionName("VenueDetailsPutFunction")]
        public static async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/details")]VenueDetailsViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]LoyaltyVenueDetailsAppService service)
        {
            log.LogInformation($"{nameof(VenueDetailsPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                model = await req.Cast<VenueDetailsViewModel>();
                return new OkObjectResult(await service.Update(id, model));
            });
        }
    }
}
