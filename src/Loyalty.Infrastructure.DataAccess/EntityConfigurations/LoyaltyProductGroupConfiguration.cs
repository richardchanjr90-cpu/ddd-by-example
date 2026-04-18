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
                .HasOne(b => b.Group)
                .WithMany(x => x.LoyaltyProductGroups)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(p => new { p.LoyaltyProgramId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");
        }
    }
}
