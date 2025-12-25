using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Venue
{
    [Collection(nameof(FunctionTestCollection))]
    public class VenueLogoTests : IClassFixture<SignedUpUserFixture>, IDisposable
    {
        private readonly VenueFixture fixture;

        public VenueLogoTests(SignedUpUserFixture signupFixture)
        {
            fixture = new VenueFixture(signupFixture);
        }

        [Fact]
        public async Task ShouldSaveLogo()
        {
            var imageContent = ImageFactory.GetImageContent();
            var response = await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/logo", imageContent);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ShouldAddLogoToVenue()
        {
            var imageContent = ImageFactory.GetImageContent();
            var response = await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/logo", imageContent);
            var getResponseMessage = await fixture.SignupFixture.Client.GetAsync("api/venues/" + fixture.Venue.Id);
            var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(getResult.LogoUrl);
            Uri uri = null;

            bool result = Uri.TryCreate(getResult.LogoUrl, UriKind.Absolute, out uri);
            Assert.True(result);
        }


        public void Dispose()
        {
            fixture?.Dispose();
        }
    }
}