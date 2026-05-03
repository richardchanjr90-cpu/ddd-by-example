using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
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

namespace LoyaltyProgram.Http.Write.Invite
{
    public class InvitePostFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly WorkerAppService service;

        public InvitePostFunction(WorkerAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("InvitePostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "workers/invited")]
            [RequestBodyType(typeof(InviteViewModel), "InviteViewModel")] InviteViewModel model,
            HttpRequest req,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            ILogger log,
            [Queue("worker-invite", Connection = "QueueConnectionString")] ICollector<WorkerInviteDto> queueItems)
        {
            log.LogInformation($"{nameof(InvitePostFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var result = await service.Invite(model);

                if (result.Success)
                {
                    queueItems.Add(new WorkerInviteDto
                    {
                        WorkerPhone = model.Phone,
                        Inviter = token.Principal.GetName()
                    });
                }

                return new OkObjectResult(result);
            });
        }
    }
}
