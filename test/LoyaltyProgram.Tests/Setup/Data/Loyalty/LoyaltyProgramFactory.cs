using System;
using Bogus;
using Loyalty.Application.ViewModels.LoyaltyProgram;

namespace LoyaltyProgram.Tests.Setup.Data.Loyalty
{
    class LoyaltyProgramFactory
    {
        public static LoyaltyProgramViewModel Get(DateTime start, DateTime finish)
        {
            var faker = new Faker<LoyaltyProgramViewModel>();

            faker.RuleFor(x => x.Name, f => f.Finance.AccountName());
            faker.RuleFor(x => x.Description, f => f.Commerce.ProductAdjective());
            faker.RuleFor(x => x.StartedDate, f => start);
            faker.RuleFor(x => x.EndedDate, f => finish);

            return faker.Generate();
        }
    }
}
