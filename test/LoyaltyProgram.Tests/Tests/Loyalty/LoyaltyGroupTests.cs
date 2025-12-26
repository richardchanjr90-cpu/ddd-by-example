using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Loyalty
{
    [Collection(nameof(FunctionTestCollection))]
    public class LoyaltyGroupTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly SignedUpUserFixture signedUpUserFixture;

        public class ValidDateTestData : TheoryData<DateTime, DateTime>
        {
            public ValidDateTestData()
            {
                Add(DateTime.Now.AddDays(2), DateTime.Now.AddYears(1));
            }
        }

        public LoyaltyGroupTests(SignedUpUserFixture signedUpUserFixture)
        {
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldCreateNewGroup(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var lpg = new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture))
            {
                Assert.True(lpg.LoyaltyProductGroup.Id > 0);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldNotCreateGroupWithLoyaltyGroupFromDifferentVenues(DateTime start, DateTime finish)
        {
            using (var venue1 = new VenueFixture(signedUpUserFixture))
            using (var venue2 = new VenueFixture(signedUpUserFixture))
            using (var program1 = new LoyaltyProgramFixture(start, finish, venue1.Venue.Id, signedUpUserFixture))
            using (var program2 = new LoyaltyProgramFixture(start, finish, venue2.Venue.Id, signedUpUserFixture))
            using (var group1 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture))
            {
                var group = LoyaltyGroupFactory.Get(group1.ProductGroup.Id, program2.LoyaltyProgram.Id);
                var groupContent = ModelHelper.Convert(group);

                var response = await signedUpUserFixture.Client.PostAsync($"api/programs/{program2.LoyaltyProgram.Id}/loyaltygroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();
                Assert.False(result.Success);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldArchiveGroup(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var lpg = new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture))
            {
                var response = await signedUpUserFixture.Client.DeleteAsync($"api/programs/{program.LoyaltyProgram.Id}/loyaltygroups/{lpg.LoyaltyProductGroup.Id}/");
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.DeserializeAsync<CommandResult>();
                Assert.True(result.Success);
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldPutProductGroup(DateTime start, DateTime finish)
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(start, finish, venue.Venue.Id, signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var lpgGet = new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture))
            {
                var lpg = new LoyaltyProductGroupViewModel();
                lpg.Id = lpgGet.LoyaltyProductGroup.Id;
                lpg.Description = "Changed";
                lpg.Rules = lpgGet.LoyaltyProductGroup.Rules;
                lpg.Name = "Changed";
                lpg.ProductGroupId = lpgGet.LoyaltyProductGroup.ProductGroup.Id;
                lpg.LoyaltyProgramId = lpgGet.LoyaltyProductGroup.LoyaltyProgramId;

                var productContent = ModelHelper.Convert(lpg);
                var response = await signedUpUserFixture.Client.PutAsync($"api/programs/{program.LoyaltyProgram.Id}/loyaltygroups/", productContent);
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.DeserializeAsync<CommandResult>();
                Assert.True(result.Success);
                Assert.True((long)result.Result > 0);

                var getResponseMessage2 = await signedUpUserFixture.Client.GetAsync($"api/programs/{program.LoyaltyProgram.Id}/loyaltygroups/{lpg.Id}/");
                var getResult2 = await getResponseMessage2.DeserializeAsync<LoyaltyProductGroupViewModel>();

                Assert.Equal(lpg.Id, getResult2.Id);
                Assert.Equal(lpg.Description, getResult2.Description);
                Assert.Equal(lpg.Name, getResult2.Name);
            }
        }
    }
}