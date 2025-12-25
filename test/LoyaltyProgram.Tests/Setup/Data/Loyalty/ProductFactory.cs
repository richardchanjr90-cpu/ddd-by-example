using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyProgram.Tests.Setup.Data.Loyalty
{
    public class ProductFactory
    {
        public static ProductViewModel Get()
        {
            var faker = new Faker<ProductViewModel>();

            faker.RuleFor(x => x.Name, f => f.Commerce.ProductName());
            faker.RuleFor(x => x.Icon, f => (int)f.PickRandom<ProductIconType>());

            return faker.Generate();
        }
    }
}
