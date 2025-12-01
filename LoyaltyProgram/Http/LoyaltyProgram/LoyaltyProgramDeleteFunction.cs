using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProgram
{
    public class LoyaltyProgramDeleteFunction
    {
        private readonly LoyaltyProgramAppService service;

        public LoyaltyProgramDeleteFunction(LoyaltyProgramAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProgramDeleteFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{venueId}/programs/{id}")]
            HttpRequest req,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProgramDeleteFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.Archive(id, token.Principal.GetUserId()));
            });
        }
    }
}