using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class LoyaltyGroupRuleConfiguration : IEntityTypeConfiguration<LoyaltyGroupRule>
    {
        public void Configure(EntityTypeBuilder<LoyaltyGroupRule> builder)
        {
            builder.ToTable("LoyaltyGroupRule", SchemaName.Loyalty);
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseHiLo("loyaltygroupruleeq");
        }
    }
}
