using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace LoyaltyProgram.Http.VenueImages
{
    public static class VenueProvideSasTokenFunction
    {
        [FunctionName("VenueProvideSasTokenFunction")]
        public static async Task<IActionResult> Run(
            long id,
            int index,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "security/sas")]
            HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation($"{nameof(VenueProvideSasTokenFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                return new NoContentResult();
            });
        }
    }
}
