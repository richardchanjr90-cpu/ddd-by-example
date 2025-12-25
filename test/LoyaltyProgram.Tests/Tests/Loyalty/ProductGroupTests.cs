using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Tests.Loyalty
{
    [Collection(nameof(FunctionTestCollection))]
    public class ProductGroupTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly SignedUpUserFixture signedUpUserFixture;

        public ProductGroupTests(SignedUpUserFixture signedUpUserFixture)
        {
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
        public async Task ShouldArchiveProduct()
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
    }
}