using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Loyalty.Application.ViewModels.Product;
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
    public class ProductTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly SignedUpUserFixture signedUpUserFixture;

        public ProductTests(SignedUpUserFixture signedUpUserFixture)
        {
            this.signedUpUserFixture = signedUpUserFixture;
        }

        [Fact]
        public async Task ShouldCreateNewProduct()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);
                var product11 = ProductFactory.Get();

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();

                var productContent = ModelHelper.Convert(product11);
                var response2 = await signedUpUserFixture.Client.PostAsync($"api/productGroups/{result.Result}/products", productContent);
                var result2 = await response2.DeserializeAsync<CommandResult>();

                Assert.True(response2.IsSuccessStatusCode);
                Assert.True((long)result2.Result > 0);
            }
        }

        [Fact]
        public async Task ShouldGetProduct()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);
                var product11 = ProductFactory.Get();

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);

                var result = await response.DeserializeAsync<CommandResult>();

                var productContent = ModelHelper.Convert(product11);
                var response2 = await signedUpUserFixture.Client.PostAsync($"api/productGroups/{result.Result}/products", productContent);
                var result2 = await response2.DeserializeAsync<CommandResult>();

                var response3 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/{result.Result}/products/{result2.Result}");
                var result3 = await response3.DeserializeAsync<ProductViewModel>();

                Assert.True(response3.IsSuccessStatusCode);
                Assert.True(result3.Id > 0);

                result3.Should()
                    .BeEquivalentTo(product11, z => z.Excluding(y => y.Id));
            }
        }

        [Fact]
        public async Task ShouldPutProduct()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);
                var product11 = ProductFactory.Get();

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();

                var productContent = ModelHelper.Convert(product11);
                var response2 = await signedUpUserFixture.Client.PostAsync($"api/productGroups/{result.Result}/products", productContent);
                var result2 = await response2.DeserializeAsync<CommandResult>();

                product11.Id = (long) result2.Result;
                product11.Name = "Changed";
                product11.Icon = product11.Icon + 1;

                var productContent2 = ModelHelper.Convert(product11);
                var response3 = await signedUpUserFixture.Client.PutAsync($"api/productGroups/{result.Result}/products", productContent2);
                var result3 = await response3.DeserializeAsync<CommandResult>();
                Assert.True(response3.IsSuccessStatusCode);
                Assert.True((long)result3.Result > 0);

                var response4 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/{result.Result}/products/{result2.Result}");
                var result4 = await response4.DeserializeAsync<ProductViewModel>();

                result4.Should()
                    .BeEquivalentTo(product11);
            }
        }

        [Fact]
        public async Task ShouldArchiveProduct()
        {
            using (var venue = new VenueFixture(signedUpUserFixture))
            {
                var group1 = ProductGroupFactory.Get(venue.Venue.Id);
                var product11 = ProductFactory.Get();

                var groupContent = ModelHelper.Convert(group1);
                var response = await signedUpUserFixture.Client.PostAsync("api/productgroups", groupContent);
                var result = await response.DeserializeAsync<CommandResult>();
                var productContent = ModelHelper.Convert(product11);
                var response2 = await signedUpUserFixture.Client.PostAsync($"api/productGroups/{result.Result}/products", productContent);
                var result2 = await response2.DeserializeAsync<CommandResult>();

                var response3 = await signedUpUserFixture.Client.DeleteAsync($"api/productGroups/{result.Result}/products/{result2.Result}");
                var result3 = await response3.DeserializeAsync<CommandResult>();

                Assert.True(response3.IsSuccessStatusCode);
                Assert.True((long)result3.Result > 0);

                var response4 = await signedUpUserFixture.Client.GetAsync($"api/productGroups/{result.Result}/products/");
                var result4 = await response4.DeserializeAsync<List<ProductViewModel>>();

                Assert.True(response4.IsSuccessStatusCode);
                Assert.True(result4.Count == 0);
            }
        }
    }
}