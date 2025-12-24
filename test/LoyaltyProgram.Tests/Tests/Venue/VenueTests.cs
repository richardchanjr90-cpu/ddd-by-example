using System;
using System.Collections.Generic;
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
    public class VenueTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;
        private readonly SignedUpUserFixture signedUpUserFixture;

        public VenueTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Fact]
        public void ShouldCreateNewVenue()
        {
            using (var venue1 = new VenueFixture(signedUpUserFixture))
            {
                Assert.True(venue1.Venue.Id > 0);
            }
        }

        [Fact]
        public async Task ShouldUpdateVenue()
        {
            using (var venue1 = new VenueFixture(signedUpUserFixture))
            {
                var venue = venue1.Venue;

                var venue2 = VenueFactory.GetVenue();
                venue2.WebSites.Add("http://google.com");
                venue2.Phones.Add("+375292202219");
                venue2.Id = venue.Id;

                var content2 = ModelHelper.Convert(venue2);
                var response2 = await signedUpUserFixture.Client.PutAsync("api/venues", content2);

                Assert.True(response2.IsSuccessStatusCode);

                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Id);
                var getResult2 = await getResponseMessage2.DeserializeAsync<VenueViewModel>();

                Assert.Equal(venue.Id, getResult2.Id);
                Assert.NotEqual(venue.Description, getResult2.Description);
                Assert.NotEqual(venue.FullDescription, getResult2.FullDescription);
                Assert.NotEqual(venue.Location, getResult2.Location);
                Assert.NotEqual(venue.WebSites.Count, getResult2.WebSites.Count);
                Assert.NotEqual(venue.Phones.Count, getResult2.Phones.Count);
            }
        }

        [Fact]
        public async Task ShouldPublishValidVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var imageContent = ImageFactory.GetImageContent();
                await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/logo", imageContent);
                var imageContent2 = ImageFactory.GetImageContent();
                await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/details/images", imageContent2);

                var getResponseMessage = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();

                getResult.IsPublished = true;

                var content = ModelHelper.Convert(getResult);
                var response2 = await signedUpUserFixture.Client.PutAsync("api/venues", content);
                Assert.True(response2.IsSuccessStatusCode);

                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult2 = await getResponseMessage2.DeserializeAsync<VenueViewModel>();

                Assert.True(getResult2.IsPublished);
            }

        }

        [Fact]
        public async Task ShouldNotPublishInValidVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                venue.Venue.IsPublished = true;
                var content = ModelHelper.Convert(venue);
                var response2 = await signedUpUserFixture.Client.PutAsync("api/venues", content);
                var getResult = await response2.DeserializeAsync<CommandResult>();
                Assert.False(getResult.Success);
            }
        }

        [Fact]
        public async Task ShouldApprovePublishedVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var imageContent = ImageFactory.GetImageContent();
                await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/logo", imageContent);
                var imageContent2 = ImageFactory.GetImageContent();
                await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/details/images", imageContent2);

                var getResponseMessage = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();

                getResult.IsPublished = true;

                var content = ModelHelper.Convert(getResult);
                await signedUpUserFixture.Client.PutAsync("api/venues", content);
                signedUpUserFixture.Client.DefaultRequestHeaders.Remove("X-ADMIN-SECRET");
                signedUpUserFixture.Client.DefaultRequestHeaders.Add("X-ADMIN-SECRET", "imnotjokingthisoneissecretqwezxc1!@#");
                var getResponseMessage2 = await signedUpUserFixture.Client.PatchAsync($"api/venues/{venue.Venue.Id}/approve", content);

                Assert.True(getResponseMessage2.IsSuccessStatusCode);

                var getResponseMessage3 = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult3 = await getResponseMessage3.DeserializeAsync<VenueViewModel>();

                Assert.True(getResult3.IsPublished);
                Assert.True(getResult3.IsApproved);
            }
        }

        [Fact]
        public async Task ShouldNotApproveNotPublishedVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var content = ModelHelper.Convert(venue);

                signedUpUserFixture.Client.DefaultRequestHeaders.Remove("X-ADMIN-SECRET");
                signedUpUserFixture.Client.DefaultRequestHeaders.Add("X-ADMIN-SECRET",
                    "imnotjokingthisoneissecretqwezxc1!@#");
                var getResponseMessage2 =
                    await signedUpUserFixture.Client.PatchAsync($"api/venues/{venue.Venue.Id}/approve", content);

                var getResult = await getResponseMessage2.DeserializeAsync<CommandResult>();
                Assert.False(getResult.Success);

                var getResponseMessage3 = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult3 = await getResponseMessage3.DeserializeAsync<VenueViewModel>();

                Assert.False(getResult3.IsPublished);
                Assert.False(getResult3.IsApproved);
            }
        }

        [Fact]
        public async Task ShouldGetVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var getResponseMessage = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();

                getResult.Should()
                    .BeEquivalentTo(venue.Venue, z => z.Excluding(y => y.Id).Excluding(x => x.Images));
            }
        }

        [Fact]
        public async Task ShouldArchiveVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var deleteResponseMessage2 = await signedUpUserFixture.Client.DeleteAsync("api/venues/" + venue.Venue.Id);
                Assert.True(deleteResponseMessage2.IsSuccessStatusCode);

                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync("api/venues/" + venue.Venue.Id);
                var getResult = await getResponseMessage2.DeserializeAsync<CommandResult>();
                Assert.Null(getResult);
            }
        }

        [Fact]
        public async void ShouldGetAllUserVenue()
        {
            using (var user = new SignedUpUserFixture(fixture))
            using (new VenueFixture(user))
            using (new VenueFixture(user))
            {
                var getResponseMessage = await user.Client.GetAsync("api/venues/");
                var getResult = await getResponseMessage.DeserializeAsync<List<VenueViewModel>>();

                Assert.True(getResponseMessage.IsSuccessStatusCode);
                Assert.True(getResult.Count == 2);
            }
        }

        [Fact]
        public async void ShouldGetOnlyOneVenueDueToLackOfAccess()
        {
            using (var user = new SignedUpUserFixture(fixture))
            using (new VenueFixture(user))
            {
                var venue2 = VenueFactory.GetVenue();
                var content2 = ModelHelper.Convert(venue2);
                var result = await user.Client.PostAsync("api/venues", content2);
                var getResult2 = await result.DeserializeAsync<CommandResult>();

                var getResponseMessage = await user.Client.GetAsync("api/venues/");
                var getResult = await getResponseMessage.DeserializeAsync<List<VenueViewModel>>();

                Assert.True(getResponseMessage.IsSuccessStatusCode);
                Assert.True(getResult.Count == 1);
                await user.UpdateTokenAsync();

                await user.Client.DeleteAsync("api/venues/" + getResult2.Result);
            }
        }

    }
}