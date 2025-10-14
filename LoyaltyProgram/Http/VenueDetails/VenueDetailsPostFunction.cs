using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.VenueDetails
{
    public class VenueDetailsPostFunction
    {
        private readonly LoyaltyVenueDetailsAppService service;

        public VenueDetailsPostFunction(LoyaltyVenueDetailsAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenueDetailsPostFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/details")]
            VenueDetailsViewModel model,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueDetailsPostFunction)} was triggered.");
            
            return await ExceptionWrapper.Handle(async () =>
            {
                model = await req.Cast<VenueDetailsViewModel>();
                //await req.AuthorizeAsync(host);
                return new OkObjectResult(await service.Create(id, model));
            });
        }
    }
}
