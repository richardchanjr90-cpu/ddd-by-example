using Loyalty.Core.ViewModels;
using Loyalty.Tests.Shared;
using Loyalty.Tests.Shared.Factory;
using Loyalty.Venue.Service;
using LoyaltyProgram.Http.Venue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace Loyalty.Domain.Handlers.Queries.Tests
{
    public class VenueFunctionsTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void Http_trigger_should_return_known_string()
        {
            VenueViewModel model = new VenueViewModel();
            var s = new LoyaltyVenueAppService(null, null);

            var request = TestFactory.CreateHttpRequest("name", "Bill");
            var response = (OkObjectResult)await VenuePutFunction.Run(model, request, logger, s);
            Assert.Equal("Hello, Bill", response.Value);
        }
    }
}
