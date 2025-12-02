using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess
{
    public class LoyaltyDbContext : DbContext, ILoyaltyTenantDbContext
    {
        public LoyaltyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public DbSet<LoyaltyGroupRule> LoyaltyRules { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<VenueWorker> VenueWorkers { get; set; }

        public DbSet<Worker> Workers { get; set; }

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
            modelBuilder.Entity<ProductGroup>()
                .HasMany(b => b.Products)
                .WithOne(x => x.ProductGroup)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venue>()
                .HasMany(b => b.LoyaltyPrograms)
                .WithOne(x => x.OwnerVenue)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LoyaltyProductGroup>()
                .HasOne(b => b.Group)
                .WithMany(x => x.LoyaltyProductGroups)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venue>()
                .HasMany(b => b.ProductGroups)
                .WithOne(x => x.OwnerVenue)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Worker>()
                .HasIndex(u => u.Phone);

            //[Role] <> 3 -- owner might create more than 1 venue, as he might be only owner in that venues.
            modelBuilder.Entity<Worker>()
                .HasIndex(u => u.WorkerId)
                .IsUnique()
                .HasFilter("([IsArchived] = 0 AND WorkerId IS NOT NULL)");

            //modelBuilder.Entity<Worker>()
            //    .HasIndex(p => new { p.WorkerId, p.VenueId }).IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.ProductGroupId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            modelBuilder.Entity<LoyaltyProgram>()
                .HasIndex(p => new { p.VenueId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            modelBuilder.Entity<LoyaltyProductGroup>()
                .HasIndex(p => new { p.LoyaltyProgramId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            //todo: check on backend;
            modelBuilder.Entity<ProductGroup>()
                .HasIndex(p => new { p.VenueId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            modelBuilder.Entity<VenueWorker>()
                .HasKey(bc => new { bc.VenueId, bc.WorkerId });

            modelBuilder.Entity<VenueWorker>()
                .HasOne(bc => bc.Venue)
                .WithMany(b => b.Workers)
                .HasForeignKey(bc => bc.VenueId);

            modelBuilder.Entity<VenueWorker>()
                .HasOne(bc => bc.Worker)
                .WithMany(c => c.Venues)
                .HasForeignKey(bc => bc.WorkerId);

            //modelBuilder.Entity<LoyaltyProductGroup>()
            //    .HasIndex(p => new {p.LoyaltyProgramId, p.ProductGroupId}).IsUnique()
            //    .HasFilter("[IsArchived] = 0");
            modelBuilder.Entity<Venue>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<LoyaltyProgram>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<Worker>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<ProductGroup>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<Purchase>().HasQueryFilter(p => p.BurnDate.HasValue);
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditableEntity) entry.Entity).CreatedBy = null;
                    ((AuditableEntity) entry.Entity).Created = DateTime.UtcNow;
                }

                ((AuditableEntity) entry.Entity).ModifiedBy = null;
                ((AuditableEntity) entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}