using System.Threading.Tasks;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.LoyaltyProductGroup
{
    public class LoyaltyProductGroupDeleteFunction
    {
        private readonly LoyaltyProductGroupAppService service;

        public LoyaltyProductGroupDeleteFunction(LoyaltyProductGroupAppService service)
        {
            this.service = service;
        }

        [FunctionName("LoyaltyProductGroupDeleteFunction")]
        public async Task<IActionResult> Run(
            long loyaltyProgramId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route =
                "programs/{loyaltyProgramId}/loyaltygroups/{id}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(LoyaltyProductGroupDeleteFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () => { return new OkObjectResult(await service.Archive(id)); });
        }
    }
}