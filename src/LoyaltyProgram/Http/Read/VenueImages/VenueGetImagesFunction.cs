using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Read.VenueImages
{
    public class VenueGetImagesFunction
    {
        private readonly LoyaltyVenueImageAppService service;

        public VenueGetImagesFunction(LoyaltyVenueImageAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string[]))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenueGetImagesFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "venues/{id}/details/images")]
            HttpRequest req,
            ILogger log,
            [FunctionToken] FunctionTokenResult token,
            [Blob("venue-images-{id}", FileAccess.Read)]
            CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenueGetImagesFunction)} was triggered.");
            var results = await service.GetImages(container, req.Query["prefix"]);

            return Handler.Wrap(token, () =>
            {
                return new OkObjectResult(results);
            });
        }
    }
}