using System.Collections.Generic;
using System.Linq;
using Bogus;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyProgram.Tests.Setup.Data.Loyalty
{
    public class ProductGroupFactory
    {
        public static ProductGroupViewModel Get(long venueId)
        {
            var faker = new Faker<ProductGroupViewModel>();

            faker.RuleFor(x => x.VenueId, f => venueId);
            faker.RuleFor(x => x.Name, f => $"{f.Commerce.ProductMaterial()} {f.Commerce.Categories(1).First()}");
            faker.RuleFor(x => x.Icon, f => (int)f.PickRandom<ProductGroupIconType>());
            faker.RuleFor(x => x.Products, f => new List<ProductViewModel>());
            return faker.Generate();
        }
    }
}
