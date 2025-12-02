using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess.Migrations
{
    public class LoyaltyTenantDbContext : LoyaltyDbContext
    {
        private readonly ITenantProvider provider;

        internal List<long> TenantIds => provider.GetTentants();

        public LoyaltyTenantDbContext(ITenantProvider provider, DbContextOptions options) 
            : base(options)
        {
            this.provider = provider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<Venue>().HasQueryFilter(e => TenantIds.Contains(e.Id));
           modelBuilder.Entity<LoyaltyProgram>().HasQueryFilter(e => TenantIds.Contains(e.VenueId));
           modelBuilder.Entity<ProductGroup>().HasQueryFilter(e => TenantIds.Contains(e.VenueId)); 
           // modelBuilder.Entity<Worker>().HasQueryFilter(e => ids.Contains(e.)); 
         //modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(e => ids.Contains(e.));
         //modelBuilder.Entity<Purchase>().HasQueryFilter(e => ids.Contains(e.));
         //modelBuilder.Entity<Product>().HasQueryFilter(e => ids.Contains(e));
        }
    }
}
