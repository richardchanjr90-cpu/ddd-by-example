using Loyalty.Core.Outbox.Entities;
using Loyalty.Core.Outbox.Entities.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loyalty.Infrastructure.Events.DataAccess.EntityConfigurations
{
    public class IntegrationEventLogEntryConfiguration : IEntityTypeConfiguration<IntegrationEventLogEntry>
    {
        public void Configure(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEvents", SchemaName.Schema);
        }
    }
}
