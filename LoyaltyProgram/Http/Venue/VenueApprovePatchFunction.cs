using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureExtensions.FunctionToken.FunctionBinding.Enums;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public class VenueApprovePatchFunction
    {
        private readonly LoyaltyVenueAppService service;

        public VenueApprovePatchFunction(LoyaltyVenueAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [FunctionName("VenueApprovePatchFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "venues/{id}/approve/")]
            HttpRequest req,
            long id,
            [FunctionToken(AuthLevel.AllowAnonymous)] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueDeleteFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                req.ValidateSecret();
                return new OkObjectResult(await service.Approve(id));
            });
        }
    }
}