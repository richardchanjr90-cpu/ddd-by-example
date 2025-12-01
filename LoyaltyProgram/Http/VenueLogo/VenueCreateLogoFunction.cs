using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Storage.Dto;
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
    public class VenueCreateLogoFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueCreateLogoFunction(LoyaltyVenueAppService service, LoyaltyVenueImageAppService imageService)
        {
            this.service = service;
            this.imageService = imageService;
        }

        [FunctionName("VenueCreateLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-logo-{id}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenueCreateImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var items = await imageService.GetCount(container);
                var images = await imageService.ConvertImages(req, id);

                if (items + images.Count > 1)
                {
                    return new BadRequestErrorMessageResult("Cannot create more than 1 logo"); 
                }

                if (images.Count > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        var item = images.First();
                        var imageStream = Image.Load(item.Image);
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