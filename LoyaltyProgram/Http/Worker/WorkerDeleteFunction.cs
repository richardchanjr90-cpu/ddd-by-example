using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Worker
{
    public static class WorkerDeleteFunction
    {
        [FunctionName("WorkerDeleteFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{venueId}/workers/{id}")]WorkerViewModel model,
            HttpRequest req,
            ILogger log,
            [Inject]WorkerAppService service)
        {
            log.LogInformation($"{nameof(WorkerDeleteFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Archive(id));
            });
        }
    }
}
