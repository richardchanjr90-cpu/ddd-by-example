using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Exceptions;
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

        [FunctionName("WorkerPutFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "workers")]WorkerViewModel model,
            ILogger log)
        {
            log.LogInformation($"{nameof(WorkerPutFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new OkObjectResult(await service.Update(model));
            });
        }
    }
}
