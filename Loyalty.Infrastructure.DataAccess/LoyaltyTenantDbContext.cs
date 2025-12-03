using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public class LoyaltyTenantDbContext : LoyaltyDbContext, ILoyaltyTenantDbContext
    {
        private readonly ITenantProvider provider;

        internal List<long> TenantIds => provider.GetTentants();

        public LoyaltyTenantDbContext(ITenantProvider provider, DbContextOptions options)
            : base(options)
        {
            this.provider = provider;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venue>().HasQueryFilter(e => TenantIds.Contains(e.Id));
            modelBuilder.Entity<LoyaltyProgram>().HasQueryFilter(e => TenantIds.Contains(e.VenueId));
            modelBuilder.Entity<ProductGroup>().HasQueryFilter(e => TenantIds.Contains(e.VenueId));
            modelBuilder.Entity<Worker>().HasQueryFilter(e => e.Venues.Any(x => TenantIds.Any(y => x.VenueId == y)));
            modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(e => TenantIds.Contains(e.Group.VenueId));
            modelBuilder.Entity<Purchase>().HasQueryFilter(e => TenantIds.Contains(e.VenueId));
            modelBuilder.Entity<Product>().HasQueryFilter(e => TenantIds.Contains(e.ProductGroup.VenueId));
        }

        protected void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditableEntity)entry.Entity).CreatedBy = provider.Principal.GetUserId();
                    ((AuditableEntity)entry.Entity).Created = DateTime.UtcNow;
                }

                ((AuditableEntity)entry.Entity).ModifiedBy = provider.Principal.GetUserId();
                ((AuditableEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
