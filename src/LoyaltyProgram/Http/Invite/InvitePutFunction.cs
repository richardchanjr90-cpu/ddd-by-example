using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Invite
{
    public class InvitePutFunction
    {
        private readonly WorkerAppService service;

        public InvitePutFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("InvitePutFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "workers/invited")]
            [RequestBodyType(typeof(UpdateInviteViewModel), "InviteViewModel")] UpdateInviteViewModel model,
            HttpRequest req,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            [Queue("worker-invite", Connection = "QueueConnectionString")] ICollector<WorkerInviteDto> queueItems,
            ILogger log)
        {
            log.LogInformation($"{nameof(InvitePutFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                model = await req.Cast<UpdateInviteViewModel>();
                var result = await service.UpdateInvited(model);
                var worker = await service.Get(model.Id);

                queueItems.Add(new WorkerInviteDto
                {
                    WorkerPhone = worker.Phone,
                    Inviter = token.Principal.GetName()
                });

                return new OkObjectResult(result);
            });
        }
    }
}