using System;
using Loyalty.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class VenueMenuConfiguration : IEntityTypeConfiguration<VenueMenuProductGroup>
    {
        public void Configure(EntityTypeBuilder<VenueMenuProductGroup> builder)
        {
            builder
                .HasKey(t => new { t.MenuId, t.ProductGroupId });

            builder
                .HasOne(pt => pt.Menu)
                .WithMany(p => p.ProductGroups)
                .HasForeignKey(pt => pt.ProductGroupId);

            builder
                .HasOne(pt => pt.ProductGroup)
                .WithMany(t => t.Menus)
                .HasForeignKey(pt => pt.MenuId);
        }
    }
}
