using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Contracts;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Auth;
using LoyaltyProgram.Tests.Setup.Data;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Venue
{
    [Collection(nameof(FunctionTestCollection))]
    public class WorkerLogoTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;
        private readonly SignedUpUserFixture signedUpUserFixture;

        public WorkerLogoTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }


        [Theory]
        [InlineData(800, 800)]
        [InlineData(2000, 2000)]
        public async Task ShouldSaveAvatar(int width, int height)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, VenueUserRole.Worker, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone), signedUpUserFixture))
            {
                var imageContent = ImageFactory.GetImageContent(width, height);
                var response = await createdUser.Client.PatchAsync($"api/userprofiles/avatar", imageContent);
                Assert.True(response.IsSuccessStatusCode);

                var getResponseMessage = await signedUpUserFixture.Client.GetAsync("api/workers/");
                var workers = await getResponseMessage.DeserializeAsync<List<WorkerViewModel>>();
                var getResult = workers.Single(x => x.Id == invitedUser.InvitedUser.Id);

                Assert.True(response.IsSuccessStatusCode);
                Assert.NotNull(getResult.PhotoUri);
                Uri uri = null;

                bool result = Uri.TryCreate(getResult.PhotoUri, UriKind.Absolute, out uri);
                Assert.True(result);
            }
        }

        [Theory]
        [InlineData(800, 800)]
        [InlineData(2000, 2000)]
        public async Task ShouldSaveAvatarAndItShouldBeTheSame(int width, int height)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, VenueUserRole.Worker, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone), signedUpUserFixture))
            {
                var sas = await signedUpUserFixture.Client.GetAsync($"api/security/sas");
                var sasToken = await sas.Content.ReadAsStringAsync();

                var imageBytes = ImageFactory.GetImage(width, height);
                var imageContent = ImageFactory.GetImageContent(imageBytes);
                var response = await createdUser.Client.PatchAsync($"api/userprofiles/avatar", imageContent);

                var getResponseMessage = await signedUpUserFixture.Client.GetAsync("api/workers/");
                var workers = await getResponseMessage.DeserializeAsync<List<WorkerViewModel>>();
                var getResult = workers.Single(x => x.Id == invitedUser.InvitedUser.Id);

                var uri = $"{getResult.PhotoUri}{sasToken}";
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
        [InlineData(1, 1)]
        [InlineData(400, 200)]
        [InlineData(200, 400)]
        [InlineData(3000, 3000)]
        public async Task ShouldNotAddPhotoToUser(int width, int height)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, VenueUserRole.Worker, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone), signedUpUserFixture))
            {
                var imageContent = ImageFactory.GetImageContent(width, height);
                var response = await createdUser.Client.PatchAsync($"api/userprofiles/avatar", imageContent);
                var getResult = await response.DeserializeAsync<CommandResult>();
                Assert.False(getResult.Success);
            }
        }
    }
}