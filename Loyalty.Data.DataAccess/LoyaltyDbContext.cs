using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Data.Entities;
using Loyalty.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Data.DataAccess
{
    public class LoyaltyDbContext : DbContext, ILoyaltyDbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LoyaltyProduct> LoyaltyProducts { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<VenueDetails> VenueDetails { get; set; }

        public LoyaltyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditableEntity) entry.Entity).CreatedBy = Guid.Empty;
                    ((AuditableEntity)entry.Entity).Created = DateTime.UtcNow;
                }

                ((AuditableEntity) entry.Entity).ModifiedBy = Guid.Empty;
                ((AuditableEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
