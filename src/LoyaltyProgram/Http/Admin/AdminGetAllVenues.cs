using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Admin
{
    public class AdminGetAllVenues
    {
        private readonly LoyaltyVenueAppService service;

        public AdminGetAllVenues(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [FunctionName("AdminGetAllVenues")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "control/admins/venues")] HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(AdminGetAllVenues)} was triggered.");
            var isAdmin = token.Principal.IsAdmin();

            if (isAdmin)
            {
                req.ValidateSecret();
                var result = await service.GetAllVenuesForAdmin();
                return new OkObjectResult(result);
            }
            
            return new NoContentResult();
        }
    }
}