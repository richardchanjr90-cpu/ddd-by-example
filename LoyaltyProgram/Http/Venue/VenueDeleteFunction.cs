using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public class VenueDeleteFunction
    {
        private readonly LoyaltyVenueAppService service;

        public VenueDeleteFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenueDeleteFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{id}")] HttpRequest req,
            long id,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueDeleteFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Archive(id));
            });
        }
    }
}
