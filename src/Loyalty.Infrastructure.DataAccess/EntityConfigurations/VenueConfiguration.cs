using System.Text.Json;
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
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(b => b.DomainEvents);
            builder.ToTable("Venue", SchemaName.Loyalty);

            builder.Property(o => o.Id)
                .UseHiLo("venueeq");

            builder
                .OwnsOne(o => o.ContactInfo, a =>
                {
                    a.WithOwner();
                });

            builder
                .OwnsOne(o => o.Location, a =>
                {
                    a.WithOwner();
                });

            builder
                .OwnsOne(o => o.Details, a =>
                {
                    a.WithOwner();
                });
        }
    }
}
