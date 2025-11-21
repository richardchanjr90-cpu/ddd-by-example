using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenueCreateImageFunction
    {
        private readonly LoyaltyVenueImageAppService service;

        public VenueCreateImageFunction(LoyaltyVenueImageAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenueCreateImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/details/images")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken] FunctionTokenResult token,
            [Blob("venue-images-{id}", FileAccess.Write)] CloudBlobContainer container,
            [Queue("venue-images", Connection = "QueueConnectionString")] ICollector<VenueQueueImageDto> queueItems)
        {
            log.LogInformation($"{nameof(VenueCreateImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var items = await service.GetCount(container);
                var images = await service.ConvertImages(req, id);

                if (items + images.Count > 10)
                {
                    return new BadRequestErrorMessageResult("Cannot create more than 10 images"); 
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
                        var blob = container.GetBlockBlobReference($"original-image-{item.Index}.jpg");
                        await blob.UploadFromStreamAsync(stream);
                        queueItems.Add(new VenueQueueImageDto
                        {
                            Index = item.Index,
                            VenueId = item.VenueId
                        });
                    }
                }

                return new NoContentResult();
            });
        }
    }
}