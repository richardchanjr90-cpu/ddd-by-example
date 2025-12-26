using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            using (var invitedUser1 = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var createdUser1 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone, invitedUser1.InvitedUser.Email), signedUpUserFixture))
            using (var invitedUser2 = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var createdUser2 = new InvitedUserFixture(fixture, new AuthUser(invitedUser2.InvitedUser.Phone, invitedUser2.InvitedUser.Email), signedUpUserFixture))
            using (var invitedUser3 = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var createdUser3 = new InvitedUserFixture(fixture, new AuthUser(invitedUser3.InvitedUser.Phone, invitedUser3.InvitedUser.Email), signedUpUserFixture))
            {
                var response = await createdUser.Client.GetAsync("api/workers");
                var workers = await response.DeserializeAsync<List<WorkerViewModel>>();

                Assert.True(response.IsSuccessStatusCode);
                Assert.True(workers.Count == 1);

                foreach (var worker in workers)
                {
                    Assert.True(worker.Id > 0);
                    Assert.Null(worker.WorkerId);

                    worker.Should()
                        .BeEquivalentTo(inviteeUser.InvitedUser, z => z.Excluding(y => y.Id));
                }
            }
        }

        [Fact]
        public async Task ShouldGetWorkersLowerByRole()
        {
        }

        [Fact]
        public async Task ShouldNotGetWorkersHigherByRole()
        {
        }

        [Fact]
        public async Task ShouldNotGetYourself()
        {
        }

        [Fact]
        public async Task ShouldGetSpecificWorker()
        {
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
        //public async Task ShouldBeInRoleForThatVenue()
        //{
        //}

        //[Fact]
        //public async Task ShouldRemoveWorkerFromVenue()
        //{
        //}

        //[Fact]
        //public async Task ShouldChangeWorkerRoleInVenue()
        //{
        //}
    }
}
