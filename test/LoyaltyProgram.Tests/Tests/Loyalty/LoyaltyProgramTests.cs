using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Loyalty
{
    [Collection(nameof(FunctionTestCollection))]
    public class LoyaltyProgramTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;
        private readonly SignedUpUserFixture signedUpUserFixture;

        public class ValidDateTestData : TheoryData<DateTime, DateTime>
        {
            public ValidDateTestData()
            {
                Add(DateTime.Now.AddDays(2), DateTime.Now.AddYears(1));
                Add(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            }
        }

        public class InvalidDateTestData : TheoryData<DateTime, DateTime>
        {
            public InvalidDateTestData()
            {
                Add(DateTime.Today.AddDays(-3), DateTime.Now.AddYears(1));
                Add(DateTime.Today.AddDays(-1), DateTime.Now.AddYears(1));
                Add(DateTime.Today.AddDays(3), DateTime.Now.AddDays(-5));
                Add(DateTime.Today.AddDays(5), DateTime.Now.AddDays(1));
                Add(DateTime.Today.AddDays(1), DateTime.Now.AddYears(11));
                Add(DateTime.Today.AddDays(1), new DateTime());
            }
        }

        public LoyaltyProgramTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldCreateNewProgram(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            {
                Assert.True(program.LoyaltyProgram.Id > 0);
            }
        }

        [Theory]
        [ClassData(typeof(InvalidDateTestData))]
        public async Task ShouldNotCreateNewProgramInThePast(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var data = LoyaltyProgramFactory.Get(start, finish);

                var dataContent = ModelHelper.Convert(data);
                var response = await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/programs", dataContent);
                var result = await response.DeserializeAsync<CommandResult>();

                Assert.False(response.IsSuccessStatusCode);
                Assert.False(result.Success);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldArchiveProgram(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            {
                var response = await signedUpUserFixture.Client.DeleteAsync($"api/venues/{venue.Venue.Id}/programs/{program.LoyaltyProgram.Id}/");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.DeserializeAsync<CommandResult>();
                Assert.True(result.Success);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldPutProductProgram(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            {
                program.LoyaltyProgram.Description = "Changed";
                program.LoyaltyProgram.Name = "Changed";
                program.LoyaltyProgram.StartedDate = program.LoyaltyProgram.StartedDate.AddDays(1);
                program.LoyaltyProgram.EndedDate = program.LoyaltyProgram.EndedDate.AddDays(1);

                var productContent = ModelHelper.Convert(program.LoyaltyProgram);

                var response = await signedUpUserFixture.Client.PutAsync($"api/venues/{venue.Venue.Id}/programs/", productContent);
                Assert.True(response.IsSuccessStatusCode);
                var result = await response.DeserializeAsync<CommandResult>();
                Assert.True(result.Success);
                Assert.True((long)result.Result > 0);

                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync($"api/venues/{venue.Venue.Id}/programs/{program.LoyaltyProgram.Id}/");
                var getResult2 = await getResponseMessage2.DeserializeAsync<LoyaltyProgramViewModel>();

                Assert.Equal(program.LoyaltyProgram.Id, getResult2.Id);
                Assert.Equal(program.LoyaltyProgram.Description, getResult2.Description);
                Assert.Equal(program.LoyaltyProgram.StartedDate, getResult2.StartedDate);
                Assert.Equal(program.LoyaltyProgram.IsPublished, getResult2.IsPublished);
                Assert.Equal(program.LoyaltyProgram.EndedDate, getResult2.EndedDate);
                Assert.Equal(program.LoyaltyProgram.Name, getResult2.Name);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldPublishLoyaltyProgram(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var lpgGet = new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture))
            {

                program.LoyaltyProgram.IsPublished = true;
                var productContent = ModelHelper.Convert(program.LoyaltyProgram);

                var response = await signedUpUserFixture.Client.PutAsync($"api/venues/{venue.Venue.Id}/programs/", productContent);
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.DeserializeAsync<CommandResult>();
                Assert.True(result.Success);
                Assert.True((long)result.Result > 0);

                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync($"api/venues/{venue.Venue.Id}/programs/{program.LoyaltyProgram.Id}/");
                var getResult2 = await getResponseMessage2.DeserializeAsync<LoyaltyProgramViewModel>();

                Assert.True(getResult2.IsPublished);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldNotPublishLoyaltyWithoutGroups(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            {
                program.LoyaltyProgram.IsPublished = true;
                var productContent = ModelHelper.Convert(program.LoyaltyProgram);

                var response = await signedUpUserFixture.Client.PutAsync($"api/venues/{venue.Venue.Id}/programs/", productContent);
                Assert.False(response.IsSuccessStatusCode);

                var result = await response.DeserializeAsync<CommandResult>();
                Assert.False(result.Success);
                
                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync($"api/venues/{venue.Venue.Id}/programs/{program.LoyaltyProgram.Id}/");
                var getResult2 = await getResponseMessage2.DeserializeAsync<LoyaltyProgramViewModel>();

                Assert.False(getResult2.IsPublished);
            }
        }

    }
}