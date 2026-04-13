using System.Text.Json;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Aggregates.Venues.ValueObjects;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder
                .HasMany(b => b.ProductGroups)
                .WithOne(x => x.OwnerVenue)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(b => b.LoyaltyPrograms)
                .WithOne(x => x.OwnerVenue)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.SocialNetworks).HasConversion(
                v => JsonSerializer.Serialize(
                    v,
                    new JsonSerializerOptions()
                    {
                        IgnoreNullValues = true
                    }),
                v => JsonSerializer.Deserialize<SocialNetworks>(v,
                    new JsonSerializerOptions()
                    {
                        IgnoreNullValues = true
                    }));

            builder.Ignore(b => b.DomainEvents);
            builder.ToTable("Venue", SchemaName.Loyalty);
        }
    }
}
