using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Tests.Shared.Factory;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Loyalty.Domain.Handlers.Queries.Tests
{
    public class VenueFunctionsTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void Http_trigger_should_return_known_string()
        {
            var model = new VenueViewModel();
            var service = new LoyaltyVenueAppService(null, null);

            var request = TestFactory.CreateHttpRequest(model);
            //var response = (OkObjectResult)await VenuePutFunction.Run(model, request, logger, service);
            //Assert.Equal("Hello, Bill", response.Value);
        }
    }
}
