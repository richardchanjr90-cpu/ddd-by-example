using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess
{
    public class LoyaltyTenantDbContext : LoyaltyDbContext, ILoyaltyTenantDbContext
    {
        private readonly ITenantProvider provider;

        internal List<long> TenantIds => provider.GetTenants();

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public virtual IEnumerable<Entity> GetModifiedOrAddedEntitiesBeforeSaveChanges()
        {
            var entities = (from e in ChangeTracker.Entries()
                       where e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified)
                       select (Entity)e.Entity)
                .ToList();

            return entities;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //todo: when named query filter appear: separate archive and tenant filters;
            modelBuilder.Entity<Venue>().HasQueryFilter(e => TenantIds.Contains(e.Id) && !e.IsArchived);
            modelBuilder.Entity<LoyaltyProgram>().HasQueryFilter(e => TenantIds.Contains(e.VenueId) && !e.IsArchived);
            modelBuilder.Entity<ProductGroup>().HasQueryFilter(e => TenantIds.Contains(e.VenueId) && !e.IsArchived);
            modelBuilder.Entity<Worker>().HasQueryFilter(e => e.Venues.Any(x => TenantIds.Contains(x.VenueId)) && !e.IsArchived);
            modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(e => TenantIds.Contains(e.Group.VenueId) && !e.IsArchived);
            modelBuilder.Entity<Purchase>().HasQueryFilter(e => TenantIds.Contains(e.VenueId));
            modelBuilder.Entity<Product>().HasQueryFilter(e => TenantIds.Contains(e.ProductGroup.VenueId) && !e.IsArchived);
            modelBuilder.Entity<Order>().HasQueryFilter(e => TenantIds.Contains(e.TenantId));
        }

        protected void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().
                Where(e =>
                e.Entity is IAuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            var ids = (from e in ChangeTracker.Entries()
                       where e.Entity is TenantEntity && e.State != EntityState.Unchanged && !(e.State == EntityState.Added && e.Entity is Venue)
                       select ((TenantEntity)e.Entity).TenantId)
                .Distinct()
                .ToList();

            foreach (var id in ids)
            {
                if (!TenantIds.Contains(id))
                {
                    throw new AuthenticationException("Access to another venue. Cross-tenant requests are not allowed.");
                }
            }

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((IAuditableEntity)entry.Entity).CreatedBy = provider.Principal.GetUserId();
                    ((IAuditableEntity)entry.Entity).Created = DateTime.UtcNow;
                }

                ((IAuditableEntity)entry.Entity).ModifiedBy = provider.Principal.GetUserId();
                ((IAuditableEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
