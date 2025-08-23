using Loyalty.Tests.Shared;
using Loyalty.Tests.Shared.Factory;
using LoyaltyProgram.Http.Venue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Loyalty.Core.ViewModels;
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


            var request = TestFactory.CreateHttpRequest("name", "Bill");
            var response = (OkObjectResult)await VenuePutFunction.Run(model, request, logger, null);
            Assert.Equal("Hello, Bill", response.Value);
        }
    }
}
