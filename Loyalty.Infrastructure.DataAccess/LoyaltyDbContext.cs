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
        public LoyaltyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public DbSet<LoyaltyGroupRule> LoyaltyRules { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<VenueDetails> VenueDetails { get; set; }

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
            modelBuilder.Entity<VenueDetails>()
                .HasOne(p => p.Venue)
                .WithOne(x => x.VenueDetails)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductGroup>()
                .HasMany(b => b.Products)
                .WithOne(x => x.ProductGroup)
                .OnDelete(DeleteBehavior.SetNull);

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
                .HasIndex(u => u.Phone)
                .IsUnique()
                .HasFilter("[IsArchived] = 0");

            modelBuilder.Entity<Worker>()
                .HasIndex(p => new { p.WorkerId, p.VenueId }).IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.ProductGroupId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            //todo: check on backend;
            modelBuilder.Entity<ProductGroup>()
                .HasIndex(p => new { p.VenueId, p.Name }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            modelBuilder.Entity<LoyaltyProductGroup>()
                .HasIndex(p => new { p.LoyaltyProgramId, p.ProductGroupId }).IsUnique()
                .HasFilter("[IsArchived] = 0");

            modelBuilder.Entity<Location>()
                .HasIndex(p => new { p.Longitude, p.Latitude }).IsUnique();

            modelBuilder.Entity<Venue>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<LoyaltyProgram>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<Worker>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<ProductGroup>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<Purchase>().HasQueryFilter(p => p.BurnDate.HasValue);

            modelBuilder.Entity<Location>().HasQueryFilter(p => !p.IsArchived);
            modelBuilder.Entity<VenueDetails>().HasQueryFilter(p => !p.IsArchived);
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditableEntity)entry.Entity).CreatedBy = Guid.Empty;
                    ((AuditableEntity)entry.Entity).Created = DateTime.UtcNow;
                }

                ((AuditableEntity)entry.Entity).ModifiedBy = Guid.Empty;
                ((AuditableEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
