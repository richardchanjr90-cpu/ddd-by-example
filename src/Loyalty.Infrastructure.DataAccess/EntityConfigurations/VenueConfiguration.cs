using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

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
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}),
                v => JsonConvert.DeserializeObject<SocialNetworks>(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}));
        }
    }
}
