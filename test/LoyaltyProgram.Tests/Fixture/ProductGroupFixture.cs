using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Fixture
{
    public class ProductGroupFixture : IDisposable
    {
        private readonly long venueId;

        public ProductGroupViewModel ProductGroup { get; }

        public SignedUpUserFixture SignupFixture { get; }

        public ProductGroupFixture(long venueId, SignedUpUserFixture fixture)
        {
            this.venueId = venueId;
            SignupFixture = fixture;
            ProductGroup = CreateGroupAsync().GetAwaiter().GetResult();
        }

        private async Task<ProductGroupViewModel> CreateGroupAsync()
        {
            var group1 = ProductGroupFactory.Get(venueId);

            var groupContent = ModelHelper.Convert(group1);
            var response = await SignupFixture.Client.PostAsync("api/productgroups", groupContent);
            var result = await response.DeserializeAsync<CommandResult>();

            var response3 = await SignupFixture.Client.GetAsync($"api/productGroups/{result.Result}/");
            var result3 = await response3.DeserializeAsync<ProductGroupViewModel>();
            return result3;
        }

        private async Task DeleteGroupAsync(long id)
        {
            var response = await SignupFixture.Client.DeleteAsync("api/productgroups/" + id);
            var result = await response.DeserializeAsync<CommandResult>();
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            if (ProductGroup != null)
            {
                DeleteGroupAsync(ProductGroup.Id).GetAwaiter().GetResult();
            }
        }
    }
}
