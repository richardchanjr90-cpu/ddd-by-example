using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Storage.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace LoyaltyProgram.Http.VenueImages
{
    public static class VenuePostImageFunction
    {
        [FunctionName("VenuePostImageFunction")]
        public static async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/details/images")]
            HttpRequestMessage req,
            ILogger log,
            [Queue("venue-images", Connection = "QueueConnectionString")] ICollector<VenueImage> queueItems)
        {
            log.LogInformation($"{nameof(VenuePostImageFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                var content = await req.Content.ReadAsMultipartAsync();

                foreach (HttpContent file in content.Contents)
                {
                    var imageStream = await file.ReadAsStreamAsync();
                    var venueImage = new VenueImage
                    {
                        VenueId = id,
                        Image = imageStream
                    };
                    queueItems.Add(venueImage);
                }

                return new OkObjectResult(new object());
            });
        }

        private static bool IsImageValid(Stream stream)
        {
            try
            {
                var result = Image.Identify(stream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            throw new NotImplementedException();
        }
    }
}
