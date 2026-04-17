using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Code
{
    public class PurchaseGetActiveByCodeFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly CodeAppService service;

        public PurchaseGetActiveByCodeFunction(CodeAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [FunctionName("PurchaseGetActiveByCodeFunction")]
        public async Task<IActionResult> Run(
            string code,
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/purchases/{code}")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(PurchaseGetActiveByCodeFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                return new OkObjectResult(await service.GetByCode(code, venueId));
            });
        }
    }
}