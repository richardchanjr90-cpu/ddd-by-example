using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public class VenueGetFunction
    {
        private readonly LoyaltyVenueAppService service;

        public VenueGetFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UpdateVenueViewModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenueGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueGetFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}