using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.Worker
{
    public class WorkerPostFunction
    {
        private readonly LoyaltyVenueImageAppService imageService;
        private readonly WorkerAppService service;

        public WorkerPostFunction(WorkerAppService service, LoyaltyVenueImageAppService imageService)
        {
            this.imageService = imageService;
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("WorkerPostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "workers")]
            [RequestBodyType(typeof(WorkerViewModel), "WorkerViewModel")] WorkerViewModel model,
            HttpRequest req,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            ILogger log,
            [Queue("worker-invite", Connection = "QueueConnectionString")] ICollector<WorkerInviteDto> queueItems)
        {
            log.LogInformation($"{nameof(WorkerPostFunction)} was triggered.");

            return await Handler.WrapAsync(log, token, async () =>
            {
                model = await req.Cast<WorkerViewModel>();

                try
                {
                    var result = await service.Create(model);
                    return new OkObjectResult(result);
                }
                finally
                {
                    queueItems.Add(new WorkerInviteDto
                    {
                        WorkerPhone = model.Phone,
                        Inviter = token.Principal.Identity.Name
                    });
                }
            });
        }
    }
}
