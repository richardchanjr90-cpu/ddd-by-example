using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Worker
{
    public class WorkerGetFunction
    {
        private readonly WorkerAppService service;

        public WorkerGetFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [FunctionName("WorkerGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            long venueId,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{venueId}/workers/{id}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(WorkerGetFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}