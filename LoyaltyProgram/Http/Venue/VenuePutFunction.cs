using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenuePutFunction
    {
        [FunctionName("VenuePutFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues")]VenueViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]LoyaltyVenueAppService service)
        {
            log.LogInformation($"{nameof(VenuePutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}
