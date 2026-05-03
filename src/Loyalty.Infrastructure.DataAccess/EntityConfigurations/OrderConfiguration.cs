using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasMany(bc => bc.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(bc => bc.OrderId);

            builder
                .Property(e => e.Status)
                .HasConversion(
                    v => v.Id,
                    v => OrderStatusEnumeration.From(v));

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder
                .Property(x => x.Rate)
                .IsRequired(false);

            builder.Ignore(b => b.DomainEvents);
            builder.ToTable("Order", SchemaName.Loyalty);
        }
    }
}
