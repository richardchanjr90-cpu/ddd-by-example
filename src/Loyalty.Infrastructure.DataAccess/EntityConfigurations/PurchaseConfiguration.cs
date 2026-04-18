using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.DataAccess.EntityConfigurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase", SchemaName.Loyalty);
            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);
        }
    }
}
