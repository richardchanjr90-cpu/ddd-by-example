using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.ToTable("ProductGroup", SchemaName.Loyalty);
            builder.HasKey(o => o.Id);

            builder
                .HasIndex(p => new { p.VenueId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            builder.Property(o => o.Id)
                .UseHiLo("productgroupeq");
        }
    }
}
