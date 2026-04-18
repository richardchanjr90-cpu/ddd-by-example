using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class LoyaltyProductGroupConfiguration : IEntityTypeConfiguration<LoyaltyProductGroup>
    {
        public void Configure(EntityTypeBuilder<LoyaltyProductGroup> builder)
        {
            builder.ToTable("LoyaltyProductGroup", SchemaName.Loyalty);
            builder.HasKey(o => o.Id);
            builder.Ignore(b => b.DomainEvents);

            builder
                .HasIndex(p => new { p.LoyaltyProgramId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");
        }
    }
}
