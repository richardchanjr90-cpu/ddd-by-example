using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Http.VenueImages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;

namespace LoyaltyProgram.Http.VenueLogo
{
    public class VenuePutLogoFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenuePutLogoFunction(
            LoyaltyVenueAppService service, 
            LoyaltyVenueImageAppService imageService)
        {
            this.service = service;
            this.imageService = imageService;
        }

        [FunctionName("VenuePutLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-logo-{id}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var image = imageService.GetImages(req).FirstOrDefault();

                if (image != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        var imageStream = Image.Load(image);
                        imageStream.SaveAsJpeg(stream);
                        stream.Position = 0;
                        await container.CreateIfNotExistsAsync();
                        var blob = container.GetBlockBlobReference("logo.jpg");
                        await blob.UploadFromStreamAsync(stream);
                    }
                }

                var url = (await imageService.GetImages(container, null)).FirstOrDefault();
                await service.Patch(id, url);

                return new NoContentResult();
            });
        }
    }
}