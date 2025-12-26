using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
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
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email), signedUpUserFixture))
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
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email), signedUpUserFixture))
            {
                var worker = WorkerFactory.GetInvite(role);
                worker.VenueId = venue.Venue.Id;

                var content = ModelHelper.Convert(worker);
                var response = await createdUser.Client.PostAsync("api/workers/invited", content);

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
                var worker = WorkerFactory.GetInvite(role);
                worker.VenueId = venue.Venue.Id;

                var content = ModelHelper.Convert(worker);
                var response = await signedUpUserFixture.Client.PostAsync("api/workers/invited", content);

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
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email), signedUpUserFixture))
            {
                var worker = WorkerFactory.GetInvite(role);
                worker.VenueId = venue.Venue.Id;

                var content = ModelHelper.Convert(worker);
                var response = await createdUser.Client.PostAsync("api/workers/invited", content);

                Assert.False(response.IsSuccessStatusCode);
                Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
            }
        }

        [Theory]
        [InlineData(VenueUserRole.Director, VenueUserRole.Worker, VenueUserRole.Manager)]
        [InlineData(VenueUserRole.Director, VenueUserRole.Manager, VenueUserRole.Worker)]
        public async Task ShouldChangeWorkerRoleInVenue(VenueUserRole inviterRole, VenueUserRole fromRole, VenueUserRole toRole)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, inviterRole, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email), signedUpUserFixture))
            using (new InviteFixture(venue.Venue.Id, fromRole, createdUser))
            {
                var response = await createdUser.Client.GetAsync("api/workers");
                var workers = await response.DeserializeAsync<List<WorkerViewModel>>();

                Assert.True(response.IsSuccessStatusCode);

                foreach (var worker in workers)
                {
                    Assert.True(worker.Role == (int)fromRole);

                    var invite = new InviteViewModel();
                    invite.Role = (int) toRole;
                    invite.Id = worker.Id;
                    invite.VenueId = venue.Venue.Id;
                    invite.Name = worker.Name;
                    invite.PositionName = worker.PositionName;
                    invite.Phone = worker.Phone;

                    var content = ModelHelper.Convert(invite);
                    var response2 = await createdUser.Client.PutAsync("api/workers/invited", content);
                    var result = await response2.DeserializeAsync<CommandResult>();

                    Assert.True(response2.IsSuccessStatusCode);
                    Assert.True(result.Success);

                    var response3 = await createdUser.Client.GetAsync($"api/workers/{worker.Id}");
                    var workerChanged = await response3.DeserializeAsync<WorkerViewModel>();


                    Assert.True(workerChanged.Role == (int) toRole);
                }
            }
        }


        [Theory]
        [InlineData(VenueUserRole.Director, VenueUserRole.Worker)]
        [InlineData(VenueUserRole.Manager, VenueUserRole.Worker)]
        public async Task ShouldArchiveInvitedPerson(VenueUserRole inviterRole, VenueUserRole role)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var invitedUser = new InviteFixture(venue.Venue.Id, inviterRole, signedUpUserFixture))
            using (var createdUser = new InvitedUserFixture(fixture, new AuthUser(invitedUser.InvitedUser.Phone, invitedUser.InvitedUser.Email), signedUpUserFixture))
            using (var inviteeUser = new InviteFixture(venue.Venue.Id, role, createdUser))
            {
                var response = await createdUser.Client.GetAsync("api/workers");
                var workers = await response.DeserializeAsync<List<WorkerViewModel>>();

                Assert.True(response.IsSuccessStatusCode);
                Assert.True(workers.Count == 1);

                var response2 = await createdUser.Client.DeleteAsync($"api/workers/{workers.First().Id}");

                Assert.True(response2.IsSuccessStatusCode);
            
                var response3 = await createdUser.Client.GetAsync("api/workers");
                var workers2 = await response3.DeserializeAsync<List<WorkerViewModel>>();

                Assert.True(workers2.Count == 0);
            }
        }

    }
}