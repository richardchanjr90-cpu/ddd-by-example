using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Fixture
{
    public class ProductFixture : IDisposable
    {
        private readonly long groupId;

        public ProductViewModel Product { get; }

        public SignedUpUserFixture SignupFixture { get; }

        public ProductFixture(long groupId, SignedUpUserFixture fixture)
        {
            this.groupId = groupId;
            SignupFixture = fixture;
            Product = CreateGroupAsync().GetAwaiter().GetResult();
        }

        private async Task<ProductViewModel> CreateGroupAsync()
        {
            var product11 = ProductFactory.Get();

            var productContent = ModelHelper.Convert(product11);
            var response2 = await SignupFixture.Client.PostAsync($"api/productGroups/{groupId}/products", productContent);
            var result2 = await response2.DeserializeAsync<CommandResult>();

            var response3 = await SignupFixture.Client.GetAsync($"api/productGroups/{groupId}/products/{result2.Result}");
            var result3 = await response3.DeserializeAsync<ProductViewModel>();
            return result3;
        }

        private async Task DeleteProductAsync(long id)
        {
            var response = await SignupFixture.Client.DeleteAsync($"api/productGroups/{groupId}/products/{Product.Id}");
            var result = await response.DeserializeAsync<CommandResult>();
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            if (Product != null)
            {
                DeleteProductAsync(Product.Id).GetAwaiter().GetResult();
            }
        }
    }
}
