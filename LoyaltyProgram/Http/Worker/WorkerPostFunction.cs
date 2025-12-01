using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Worker
{
    public class WorkerPostFunction
    {
        private readonly WorkerAppService service;

        public WorkerPostFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [FunctionName("WorkerPostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "workers")]
            WorkerViewModel model,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            ILogger log,
            [Queue("worker-invite", Connection = "QueueConnectionString")] ICollector<WorkerInviteDto> queueItems)
        {
            log.LogInformation($"{nameof(WorkerPostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var result = await service.Create(model);

                queueItems.Add(new WorkerInviteDto
                {
                    WorkerPhone = model.Phone,
                    Inviter = token.Principal.Identity.Name
                });

                return new OkObjectResult(result);
            });
        }
    }
}