using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public class VenuePostFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly LoyaltyVenueAppService service;

        public VenuePostFunction(LoyaltyVenueAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [SupportedRequestFormat("application/json")]
        [FunctionName("VenuePostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues")]
            [RequestBodyType(typeof(CreateVenueViewModel), "VenueViewModel")] CreateVenueViewModel model,
            HttpRequest req,
            [FunctionToken(nameof(VenueUserRole.Owner))] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenuePostFunction)} was triggered.");
            
            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                model = await req.Cast<CreateVenueViewModel>();
                log.LogInformation("Venue created: {@Model}", model);

                return new OkObjectResult(await service.Create(model));
            });
        }
    }
}