using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.Worker
{
    public static class WorkerGetAllFunction
    {
        [FunctionName("WorkerGetAllFunction")]
        public static async Task<IActionResult> Run(
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/workers")]
            HttpRequest req,
            ILogger log,
            [Inject]WorkerAppService service)
        {
            log.LogInformation($"{nameof(WorkerGetAllFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get());
            });
        }
    }
}
