using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess
{
    public class LoyaltyDbContext : DbContext, ILoyaltyDbContext
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
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                throw new LoyaltyValidationException("Duplicated entity", ex, ErrorCode.DUPLICATED_ENTITY);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                throw new LoyaltyValidationException("Duplicated entity", ex, ErrorCode.DUPLICATED_ENTITY);
            }
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
                .HasIndex(u => u.Phone)
                .IsUnique();

            modelBuilder.Entity<Worker>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            modelBuilder.Entity<Worker>()
                .HasIndex(u => u.WorkerId)
                .IsUnique()
                .HasFilter("([IsArchived] = 0 AND WorkerId IS NOT NULL)");

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

            modelBuilder.Entity<VenueWorker>()
                .HasIndex(p => new { p.VenueId, p.WorkerId })
                .IsUnique();
        }
    }
}
