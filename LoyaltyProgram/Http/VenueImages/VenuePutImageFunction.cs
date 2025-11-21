using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenuePutImageFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenuePutImageFunction(
            LoyaltyVenueAppService service,
            LoyaltyVenueImageAppService imageService)
        {
            this.service = service;
            this.imageService = imageService;
        }

        [FunctionName("VenuePutImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            string index,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken] FunctionTokenResult token,
            [Blob("venue-images-{id}/original-image-{index}.jpg", FileAccess.Write)]
            Stream blobStream,
            [Queue("venue-images", Connection = "QueueConnectionString")]
            ICollector<VenueQueueImageDto> queueItems)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var images = await imageService.ConvertImages(req, id, Guid.Parse(index));

                if (images != null && images.Count > 0)
                {
                    var imageStream = Image.Load(images.First().Image);
                    imageStream.SaveAsJpeg(blobStream);
                    queueItems.Add(new VenueQueueImageDto
                    {
                        Index = Guid.Parse(index),
                        VenueId = id
                    });
                }

                return new NoContentResult();
            });
        }
    }
}