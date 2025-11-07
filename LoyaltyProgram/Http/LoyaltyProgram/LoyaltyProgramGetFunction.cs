using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProgram
{
    public class LoyaltyProgramGetFunction
    {
        private readonly LoyaltyProgramAppService service;

        public LoyaltyProgramGetFunction(LoyaltyProgramAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProgramGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/programs/{id}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProgramAppService)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get(id, venueId));
            });
        }
    }
}