using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Ignore(b => b.DomainEvents);
            builder.ToTable("OrderItem", SchemaName.Loyalty);
        }
    }
}
