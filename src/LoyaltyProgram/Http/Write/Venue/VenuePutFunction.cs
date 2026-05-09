using System;
using System.Net;
using System.Net.Http;
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

namespace LoyaltyProgram.Http.Write.Venue
{
    public class VenuePutFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly LoyaltyVenueAppService service;

        public VenuePutFunction(LoyaltyVenueAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenuePutFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues")]
            //[RequestBodyType(typeof(UpdateVenueViewModel), "VenueViewModel")] UpdateVenueViewModel model,
            HttpRequestMessage req,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenuePutFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var model = await req.Cast<UpdateVenueViewModel>();
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}