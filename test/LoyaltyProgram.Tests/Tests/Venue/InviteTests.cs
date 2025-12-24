using System.Collections.Generic;
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
    public class InviteTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;
        private readonly SignedUpUserFixture signedUpUserFixture;

        public InviteTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Fact]
        public async Task OwnerShouldInviteWorkerToVenue()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedWorker = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            {
                Assert.True(invitedWorker.InvitedUser.Id > 0);
            }
        }

        [Theory]
        [InlineData(VenueUserRole.Director)]
        [InlineData(VenueUserRole.Manager)]
        [InlineData(VenueUserRole.Worker)]
        public async Task OwnerShouldInviteWorkersToVenueAndGet(VenueUserRole role)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedWorker1 = new InviteFixture(venue.Venue.Id, role, signedUpUserFixture))
            {
                //var content = ModelHelper.Convert(venue);
                var response = await signedUpUserFixture.Client.GetAsync("api/workers");
                var workers = await response.DeserializeAsync<List<WorkerViewModel>>();

                Assert.True(response.IsSuccessStatusCode);
                Assert.True(workers.Count == 1);

                foreach (var worker in workers)
                {
                    Assert.True(worker.Id > 0);
                    Assert.Null(worker.WorkerId);

                    worker.Should()
                        .BeEquivalentTo(invitedWorker1.InvitedUser, z => z.Excluding(y => y.Id));
                }
            }
        }

        [Theory]
        [InlineData(VenueUserRole.Director, VenueUserRole.Manager)]
        [InlineData(VenueUserRole.Director, VenueUserRole.Worker)]
        [InlineData(VenueUserRole.Manager, VenueUserRole.Worker)]
        public async Task UserShouldInviteWorkersToVenueAndGet(VenueUserRole inviterRole, VenueUserRole role)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, inviterRole, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email)))
            using (var inviteeUser = new InviteFixture(venue.Venue.Id, role, createdUser))
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

        [Theory]
        [InlineData(VenueUserRole.Manager, VenueUserRole.Owner)]
        [InlineData(VenueUserRole.Manager, VenueUserRole.Director)]
        [InlineData(VenueUserRole.Manager, VenueUserRole.Manager)]
        public async Task UserShouldNotHaveAccessToInviteWorkers(VenueUserRole inviterRole, VenueUserRole role)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, inviterRole, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email)))
            {
                var worker =  WorkerFactory.GetWorker(role);
                worker.VenueIds.Add(venue.Venue.Id);

                var content = ModelHelper.Convert(worker);
                var response = await createdUser.Client.PostAsync("api/workers", content);

                var getResult = await response.DeserializeAsync<CommandResult>();
                Assert.False(getResult.Success);
            }
        }

        [Theory]
        [InlineData(VenueUserRole.Owner, VenueUserRole.Owner)]
        public async Task ShouldNotBeASecondOwner(VenueUserRole inviterRole, VenueUserRole role)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var worker = WorkerFactory.GetWorker(role);
                worker.VenueIds.Add(venue.Venue.Id);

                var content = ModelHelper.Convert(worker);
                var response = await signedUpUserFixture.Client.PostAsync("api/workers", content);

                var getResult = await response.DeserializeAsync<CommandResult>();
                Assert.False(getResult.Success);
            }
        }

        [Theory]
        [InlineData(VenueUserRole.Worker, VenueUserRole.Worker)]
        public async Task WorkerShouldNotInvite(VenueUserRole inviterRole, VenueUserRole role)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, inviterRole, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email)))
            {
                var worker = WorkerFactory.GetWorker(role);
                worker.VenueIds.Add(venue.Venue.Id);

                var content = ModelHelper.Convert(worker);
                var response = await createdUser.Client.PostAsync("api/workers", content);

                Assert.False(response.IsSuccessStatusCode);
                Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
            }
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
        //public async Task ShouldBeInDifferentRolesForDifferentVenues()
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

        //[Fact]
        //public async Task ShouldResendInviteToWorkerIfAsked()
        //{
        //    using (var venue = new VenueFixture(signedUpUserFixture))
        //    using (var invitedWorker = new InviteFixture(venue.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
        //    using (var signedUpWorker = new InvitedUserFixture(fixture,
        //        new AuthUser(invitedWorker.InvitedUser.Phone,
        //            invitedWorker.InvitedUser.Email)))
        //    {



        //    }
        //}
    }
}