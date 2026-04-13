using System;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", SchemaName.Loyalty);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(o => o.Id);

            builder
                .HasIndex(p => new { p.ProductGroupId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            builder.HasOne<Venue>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("VenueId");

            builder.Property(o => o.Id)
                .UseHiLo("producteq");
        }
    }
}
