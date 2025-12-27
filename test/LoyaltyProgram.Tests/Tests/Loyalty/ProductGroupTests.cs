using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Domain.Contracts;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Auth;
using LoyaltyProgram.Tests.Setup.Data;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Loyalty
{
    [Collection(nameof(FunctionTestCollection))]
    public class ProductGroupTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;

        private readonly SignedUpUserFixture signedUpUserFixture;

        public ProductGroupTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Fact]
        public async Task ShouldCreateNewProductGroup()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();

                Assert.True(response.IsSuccessStatusCode);
                Assert.True((long)result.Result > 0);
            }
        }

        [Fact]
        public async Task ShouldGetProductGroups()
        {
            using (var venue1 = new VenueFixture(signedUpUserFixture))
            using (var venue2 = new VenueFixture(signedUpUserFixture))
            using (var invitedUser1 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var createdUser1 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture))
            using (var invitedUser2 = new InviteFixture(venue2.Venue.Id, VenueUserRole.Director, signedUpUserFixture))
            using (var createdUser2 = new InvitedUserFixture(fixture, new AuthUser(invitedUser2.InvitedUser.Phone), signedUpUserFixture))
            using (new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture))
            using (new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture))
            using (new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture))
            using (new ProductGroupFixture(venue2.Venue.Id, signedUpUserFixture))
            using (new ProductGroupFixture(venue2.Venue.Id, signedUpUserFixture))
            using (new ProductGroupFixture(venue2.Venue.Id, signedUpUserFixture))
            {
                var response1 = await createdUser1.Client.GetAsync($"api/productGroups/");
                var response2 = await createdUser2.Client.GetAsync($"api/productGroups/");
                var response3 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/");

                Assert.True(response1.IsSuccessStatusCode);
                Assert.True(response2.IsSuccessStatusCode);
                Assert.True(response3.IsSuccessStatusCode);

                var result1 = await response1.DeserializeAsync<List<ProductGroupViewModel>>();
                var result2 = await response2.DeserializeAsync<List<ProductGroupViewModel>>();
                var result3 = await response3.DeserializeAsync<List<ProductGroupViewModel>>();

                Assert.True(result1.Count == 3);
                Assert.True(result2.Count == 3);
                Assert.True(result3.Count == 6);
            }
        }

        [Fact]
        public async Task ShouldGetProductGroup()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();

                var response3 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/{result.Result}/");
                var result3 = await response3.DeserializeAsync<ProductGroupViewModel>();

                Assert.True(response3.IsSuccessStatusCode);
                Assert.True(result3.Id > 0);

                result3.Should()
                    .BeEquivalentTo(group1, z => z.Excluding(y => y.Id));
            }
        }

        [Fact]
        public async Task ShouldGetProductGroupWithProducts()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var product = new ProductFixture(group.ProductGroup.Id, signedUpUserFixture))
            using (var product2 = new ProductFixture(group.ProductGroup.Id, signedUpUserFixture))
            {
                var response3 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/{group.ProductGroup.Id}/");
                var result3 = await response3.DeserializeAsync<ProductGroupViewModel>();

                Assert.True(response3.IsSuccessStatusCode);
                Assert.True(result3.Id > 0);
                Assert.True(result3.Products.Count == 2);
            }
        }

        [Fact]
        public async Task ShouldPutProductGroup()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();

                group1.Id = (long)result.Result;
                group1.Name = "Changed";
                group1.Icon = group1.Icon + 1;

                var productContent2 = ModelHelper.Convert(group1);
                var response3 = await signedUpUserFixture.Client.PutAsync($"api/productGroups", productContent2);
                var result3 = await response3.DeserializeAsync<CommandResult>();

                Assert.True(response3.IsSuccessStatusCode);
                Assert.True((long)result3.Result > 0);

                var response4 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/{result.Result}");
                var result4 = await response4.DeserializeAsync<ProductGroupViewModel>();

                result4.Should()
                    .BeEquivalentTo(group1);
            }
        }

        [Fact]
        public async Task ShouldArchiveProductGroup()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();

                var response3 = await signedUpUserFixture.Client.DeleteAsync($"api/productGroups/{result.Result}");
                var result3 = await response3.DeserializeAsync<CommandResult>();

                Assert.True(response3.IsSuccessStatusCode);
                Assert.True((long)result3.Result > 0);

                var response4 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/");
                var result4 = await response4.DeserializeAsync<List<ProductGroupViewModel>>();

                Assert.True(response4.IsSuccessStatusCode);
                Assert.True(result4.Count == 0);
            }
        }

        [Fact]
        public async Task ShouldArchiveProductGroupThatIsAssignedToArchivedLoyaltyGroup()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), venue.Venue.Id, signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var lpg = new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture))
            {
                lpg.Dispose();

                var response = await signedUpUserFixture.Client.DeleteAsync($"api/productGroups/{group.ProductGroup.Id}");
                var result = await response.DeserializeAsync<CommandResult>();

                Assert.True(response.IsSuccessStatusCode);
                Assert.True(result.Success);
            }
        }

        [Fact]
        public async Task ShouldNotArchiveProductGroupThatIsAssignedToActiveLoyaltyGroup()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            using (var program = new LoyaltyProgramFixture(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), venue.Venue.Id, signedUpUserFixture))
            using (var group = new ProductGroupFixture(venue.Venue.Id, signedUpUserFixture))
            using (var lpg = new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture))
            {
                var response = await signedUpUserFixture.Client.DeleteAsync($"api/productGroups/{group.ProductGroup.Id}");
                var result = await response.DeserializeAsync<CommandResult>();

                Assert.False(response.IsSuccessStatusCode);
                Assert.False(result.Success);
            }
        }
    }
}