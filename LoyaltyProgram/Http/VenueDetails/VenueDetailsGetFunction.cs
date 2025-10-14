using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.VenueDetails
{
    public class VenueDetailsGetFunction
    {
        private readonly LoyaltyVenueDetailsAppService service;

        public VenueDetailsGetFunction(LoyaltyVenueDetailsAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenueDetailsGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/details")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueDetailsGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}
