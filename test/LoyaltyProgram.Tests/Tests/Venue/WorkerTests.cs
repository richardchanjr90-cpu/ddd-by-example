using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Auth;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Venue
{
    [Collection(nameof(FunctionTestCollection))]
    public class WorkerTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;
        private readonly SignedUpUserFixture signedUpUserFixture;

        public WorkerTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Fact]
        public async Task ShouldGetWorkers()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedDirector = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var director = new InvitedUserFixture(fixture, new AuthUser(invitedDirector.InvitedUser.Phone), signedUpUserFixture))
            using (var invitedManager = new InviteFixture(venue.Venue.Id, VenueUserRole.Manager, signedUpUserFixture))
            using (var manager = new InvitedUserFixture(fixture, new AuthUser(invitedManager.InvitedUser.Phone), signedUpUserFixture))
            using (var invitedWorker = new InviteFixture(venue.Venue.Id, VenueUserRole.Worker, signedUpUserFixture))
            using (var worker = new InvitedUserFixture(fixture, new AuthUser(invitedWorker.InvitedUser.Phone), signedUpUserFixture))
            {
                var response = await signedUpUserFixture.Client.GetAsync("api/workers");
                var workers = await response.DeserializeAsync<List<WorkerViewModel>>();
                Assert.True(response.IsSuccessStatusCode);
                Assert.True(workers.Count == 3);

                var response2 = await director.Client.GetAsync("api/workers");
                var workers2 = await response2.DeserializeAsync<List<WorkerViewModel>>();
                Assert.True(response2.IsSuccessStatusCode);
                Assert.True(workers2.Count == 2);

                var response3 = await manager.Client.GetAsync("api/workers");
                var workers3 = await response3.DeserializeAsync<List<WorkerViewModel>>();
                Assert.True(response3.IsSuccessStatusCode);
                Assert.True(workers3.Count == 1);

                var response4 = await worker.Client.GetAsync("api/workers");
                var workers4 = await response4.DeserializeAsync<List<WorkerViewModel>>();
                Assert.True(response4.IsSuccessStatusCode);
                Assert.True(workers4.Count == 0);
            }
        }

        [Fact]
        public async Task ShouldGetSpecificWorker()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedDirector = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var director = new InvitedUserFixture(fixture, new AuthUser(invitedDirector.InvitedUser.Phone), signedUpUserFixture))
            {
                var getResponseMessage = await signedUpUserFixture.Client.GetAsync("api/workers/");
                var workers = await getResponseMessage.DeserializeAsync<List<WorkerViewModel>>();
                var getResult = workers.Single(x => x.Id == invitedDirector.InvitedUser.Id);

                Assert.Equal(getResult.Id, invitedDirector.InvitedUser.Id);
                Assert.Equal(getResult.Name, director.Signup.Name);
                Assert.Equal(getResult.Phone, invitedDirector.InvitedUser.Phone);
                Assert.Equal(getResult.PositionName, invitedDirector.InvitedUser.PositionName);
                Assert.Equal(getResult.LastName, director.Signup.Surname);
                Assert.Equal(getResult.Email, director.Signup.Email);
            }
        }

        [Fact]
        public async Task ShouldArchiveWorker()
        {
        }

        //[Fact]
        //public async Task ShouldHaveAccessOnlyToYourVenues()
        //{
        //}

        //[Fact]
        //public async Task ShouldRemoveWorkerFromVenue()
        //{
        //}
    }
}
