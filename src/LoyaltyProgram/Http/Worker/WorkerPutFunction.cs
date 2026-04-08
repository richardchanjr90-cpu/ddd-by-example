using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Infrastructure.IoC;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Worker
{
    public class WorkerPutFunction
    {
        private readonly WorkerAppService service;

        public WorkerPutFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("WorkerPutFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "workers")]
            [RequestBodyType(typeof(UpdateInviteViewModel), "UpdateWorkerViewModel")] UpdateInviteViewModel model,
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(WorkerPutFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                model = await req.Cast<UpdateInviteViewModel>();
                var result = await service.UpdateInvited(model);

                return new OkObjectResult(result);
            });
        }
    }
}