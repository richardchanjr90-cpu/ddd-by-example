using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess
{
    public class LoyaltyDbContext : DbContext, ILoyaltyDbContext
    {
        public DbSet<Location> Locations { get; set; }

        public DbSet<LoyaltyProductGroup> LoyaltyProducts { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public DbSet<LoyaltyProgramRule> LoyaltyRules { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<Product> Products { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VenueDetails>()
                .HasOne(p => p.Venue)
                .WithOne(x => x.VenueDetails)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venue>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<LoyaltyProgram>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(p => !p.IsArchived);
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
