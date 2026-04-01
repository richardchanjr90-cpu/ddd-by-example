using System;
using Loyalty.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(bc => bc.Menu)
                .WithMany(c => c.Orders)
                .HasForeignKey(bc => bc.MenuId);

            builder
                .HasMany(bc => bc.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(bc => bc.OrderId);
        }
    }
}
