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

namespace LoyaltyProgram.Http.Venue
{
    public class VenuePutFunction
    {
        private readonly LoyaltyVenueAppService service;

        public VenuePutFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenuePutFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues")]
            VenueViewModel model,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenuePutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                model = await req.Cast<VenueViewModel>();
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}