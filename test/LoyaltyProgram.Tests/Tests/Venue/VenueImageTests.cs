using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.Venue;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Venue
{
    [Collection(nameof(FunctionTestCollection))]
    public class VenueImageTests : IClassFixture<SignedUpUserFixture>, IDisposable
    {
        private readonly SignedUpUserFixture signupFixture;
        private readonly VenueFixture fixture;

        public VenueImageTests(SignedUpUserFixture signupFixture)
        {
            this.signupFixture = signupFixture;
            fixture = new VenueFixture(signupFixture);
        }

        [Fact]
        public async Task ShouldGetSasToken()
        {
            var response = await fixture.SignupFixture.Client.GetAsync($"api/security/sas");
            var resultString = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotEmpty(resultString);
        }

        [Fact]
        public async Task ShouldSaveImage()
        {
            var imageContent = ImageFactory.GetImageContent();
            var response = await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/details/images", imageContent);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ShouldAddImagesToVenue()
        {
            var imageContent = ImageFactory.GetImageContent();
            var response = await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/details/images", imageContent);
            var getResponseMessage = await fixture.SignupFixture.Client.GetAsync("api/venues/" + fixture.Venue.Id);
            var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(getResult.Images);
            Assert.True(getResult.Images.Count > 0);
            Assert.True(getResult.Images.Count > 0);
            Uri uri = null;

            bool result = Uri.TryCreate(getResult.Images.First(), UriKind.Absolute, out uri);
            Assert.True(result);
        }

        [Theory]
        [InlineData(800, 600)]
        [InlineData(1920, 1080)]
        public async Task ShouldAddImagesToVenueAndCheckThatItIsTheSame(int width, int height)
        {
            var sas = await fixture.SignupFixture.Client.GetAsync($"api/security/sas");
            var sasToken = await sas.Content.ReadAsStringAsync();

            var imageBytes = ImageFactory.GetImage(width, height);
            var imageContent = ImageFactory.GetImageContent(imageBytes);
            await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/details/images", imageContent);
            var getResponseMessage = await fixture.SignupFixture.Client.GetAsync("api/venues/" + fixture.Venue.Id);
            var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();

            foreach (var image in getResult.Images)
            {
                var uri = $"{image}{sasToken}";
                var loadedImage = ImageFactory.Load(uri);

                var loadedMap = new Bitmap(new MemoryStream(loadedImage));
                var originamMap = new Bitmap(new MemoryStream(imageBytes));
                
                Assert.Equal(originamMap.Width, originamMap.Width);
                Assert.Equal(loadedMap.Height, loadedMap.Height);

                var originalHash = ImageFactory.GetHash(originamMap);
                var loadHash = ImageFactory.GetHash(loadedMap);

                int equalElements = originalHash.Zip(loadHash, (i, j) => i == j).Count(eq => eq); Assert.True(equalElements >= 256 * 0.9);
            }
        }

        [Theory]
        [InlineData(800, 600)]
        [InlineData(1920, 1080)]
        public async Task ShouldHaveOnlyOwnImagesThatAreTheSameTest(int width, int height)
        {
            var sas = await fixture.SignupFixture.Client.GetAsync($"api/security/sas");
            var sasToken = await sas.Content.ReadAsStringAsync();

            var venue1 = new VenueFixture(signupFixture);
            var venue2 = new VenueFixture(signupFixture);


            var imageBytes1 = ImageFactory.GetImage(width, height);
            var imageContent1 = ImageFactory.GetImageContent(imageBytes1);
            await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/details/images", imageContent1);
            var getResponseMessage1 = await fixture.SignupFixture.Client.GetAsync("api/venues/" + venue1.Venue.Id);
            var getResult1 = await getResponseMessage1.DeserializeAsync<VenueViewModel>();

            var imageBytes2 = ImageFactory.GetImage(width, height);
            var imageContent2 = ImageFactory.GetImageContent(imageBytes2);
            await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/details/images", imageContent2);
            var getResponseMessage2 = await fixture.SignupFixture.Client.GetAsync("api/venues/" + venue2.Venue.Id);
            var getResult2 = await getResponseMessage2.DeserializeAsync<VenueViewModel>();

            foreach (var image in getResult1.Images)
            {
                var uri = $"{image}{sasToken}";
                var loadedImage = ImageFactory.Load(uri);

                var loadedMap = new Bitmap(new MemoryStream(loadedImage));
                var originamMap = new Bitmap(new MemoryStream(imageBytes1));

                Assert.Equal(originamMap.Width, originamMap.Width);
                Assert.Equal(loadedMap.Height, loadedMap.Height);

                var originalHash = ImageFactory.GetHash(originamMap);
                var loadHash = ImageFactory.GetHash(loadedMap);

                int equalElements = originalHash.Zip(loadHash, (i, j) => i == j).Count(eq => eq);
                Assert.True(equalElements >= 256 * 0.9);
            }

            foreach (var image in getResult2.Images)
            {
                var uri = $"{image}{sasToken}";
                var loadedImage = ImageFactory.Load(uri);

                var loadedMap = new Bitmap(new MemoryStream(loadedImage));
                var originamMap = new Bitmap(new MemoryStream(imageBytes1));

                Assert.Equal(originamMap.Width, originamMap.Width);
                Assert.Equal(loadedMap.Height, loadedMap.Height);

                var originalHash = ImageFactory.GetHash(originamMap);
                var loadHash = ImageFactory.GetHash(loadedMap);

                int equalElements = originalHash.Zip(loadHash, (i, j) => i == j).Count(eq => eq);
                Assert.True(equalElements >= 256 * 0.9);
            }

            venue1.Dispose();
            venue2.Dispose();
        }

        [Theory]
        [InlineData(600, 600)]
        [InlineData(799, 600)]
        [InlineData(800, 599)]
        [InlineData(2561, 1440)]
        [InlineData(2560, 1441)]
        [InlineData(1, 1)]
        public async Task ShouldNotAddImagesToVenue(int width, int height)
        {
            var imageContent = ImageFactory.GetImageContent(width, height);
            var response = await fixture.SignupFixture.Client.PostAsync($"api/venues/{fixture.Venue.Id}/details/images", imageContent);
            var resultString = await response.Content.ReadAsStringAsync();

            Assert.StartsWith("Validation failed", resultString);
        }

        public void Dispose()
        {
            fixture?.Dispose();
        }
    }
}
