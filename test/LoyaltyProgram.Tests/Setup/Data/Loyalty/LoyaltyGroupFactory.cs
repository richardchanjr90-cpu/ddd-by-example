using Bogus;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;

namespace LoyaltyProgram.Tests.Setup.Data.Loyalty
{
    class LoyaltyGroupFactory
    {
        public static LoyaltyProductGroupViewModel Get(long groupId, long programId)
        {
            var faker = new Faker<LoyaltyProductGroupViewModel>();

            faker.RuleFor(x => x.Name, f => f.Commerce.Department());
            faker.RuleFor(x => x.Description, f => f.Commerce.ProductAdjective());
            faker.RuleFor(x => x.ProductGroupId, f => groupId);
            faker.RuleFor(x => x.LoyaltyProgramId, f => programId);
            faker.RuleFor(x => x.Rules, f => RuleFactory.Get());

            return faker.Generate();
        }
    }
}
