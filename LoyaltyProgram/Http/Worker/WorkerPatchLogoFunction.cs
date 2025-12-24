using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.Worker
{
    public class WorkerPatchLogoFunction
    {
        private readonly WorkerAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public WorkerPatchLogoFunction(WorkerAppService service, LoyaltyVenueImageAppService imageService)
        {
            this.service = service;
            this.imageService = imageService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("WorkerPatchLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "workers/{id}/photo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken] FunctionTokenResult token,
            [Blob("worker-photo-{id}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(WorkerPatchLogoFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var image = await imageService.GetImageOrNullAsync(req);

                using (var stream = new MemoryStream())
                {
                    var blob = await imageService.GetBlobForImageAsync(container, $"photo-{Guid.NewGuid()}.jpg");

                    imageService.SaveImageOfWidthToStream(
                        stream,
                        image);

                    await blob.UploadFromStreamAsync(stream);

                    await service.PatchPhoto(blob.Uri.ToString(), id);
                }
                return new NoContentResult();
            });
        }
    }
}
