using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class LoyaltyProgramConfiguration : IEntityTypeConfiguration<LoyaltyProgram>
    {
        public void Configure(EntityTypeBuilder<LoyaltyProgram> builder)
        {
            builder.ToTable("LoyaltyProgram", SchemaName.Loyalty);
            builder.HasKey(o => o.Id);
            builder.Ignore(b => b.DomainEvents);

            builder
                .HasIndex(p => new { p.VenueId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");
        }
    }
}
