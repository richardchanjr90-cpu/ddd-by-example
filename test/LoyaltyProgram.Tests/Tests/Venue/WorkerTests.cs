using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LoyaltyProgram.Tests.Fixture;
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
