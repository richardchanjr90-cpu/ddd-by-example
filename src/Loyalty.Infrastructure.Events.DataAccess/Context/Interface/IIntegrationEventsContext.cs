using Loyalty.Core.Contracts;
using Loyalty.Core.Outbox.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Events.DataAccess.Context.Interface
{
    public interface IIntegrationEventsContext : IDbContext
    {
        public DbSet<IntegrationEventLogEntry> IntegrationEvents { get; set; }
    }
}
