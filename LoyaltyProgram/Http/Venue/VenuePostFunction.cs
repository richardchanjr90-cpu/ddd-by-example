using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Exceptions;
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
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues")]VenueViewModel model,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenuePostFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}
