using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Write.LoyaltyProductGroup
{
    public class LoyaltyProductGroupPutFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly LoyaltyProductGroupAppService service;

        public LoyaltyProductGroupPutFunction(LoyaltyProductGroupAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("LoyaltyProductGroupPutFunction")]
        public async Task<IActionResult> Run(
            long loyaltyProgramId,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "programs/{loyaltyProgramId}/loyaltygroups")]
            [RequestBodyType(typeof(LoyaltyProductGroupViewModel), "LoyaltyProductGroupViewModel")] LoyaltyProductGroupViewModel model,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupPutFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}