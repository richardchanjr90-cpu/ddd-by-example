using Loyalty.Core.Outbox.Entities;
using Loyalty.Infrastructure.Events.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Events.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Events.DataAccess.Context
{
    public class IntegrationEventsContext : DbContext, IIntegrationEventsContext
    {
        public DbSet<IntegrationEventLogEntry> IntegrationEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {          
            builder.ApplyConfiguration(new IntegrationEventLogEntryConfiguration());
        }
    }
}
