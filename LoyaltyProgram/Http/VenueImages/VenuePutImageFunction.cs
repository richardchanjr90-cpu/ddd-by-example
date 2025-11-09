using System;
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

namespace LoyaltyProgram.Http.VenueImages
{
    public class VenuePutImageFunction
    {
        private readonly LoyaltyVenueImageAppService service;

        public VenuePutImageFunction(LoyaltyVenueImageAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenuePutImageFunction")]
        public async Task<IActionResult> Run(
            long id,
            string index,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [Blob("venue-images-{id}/original-image-{index}.jpg", FileAccess.Write)]
            Stream blobStream,
            [Queue("venue-images", Connection = "QueueConnectionString")]
            ICollector<VenueNewBlobImageDto> queueItems)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                var images = await service.ConvertImages(req, id, Guid.Parse(index));

                if (images != null && images.Count > 0)
                {
                    var imageStream = Image.Load(images.First().Image);
                    imageStream.SaveAsJpeg(blobStream);
                    queueItems.Add(new VenueNewBlobImageDto
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