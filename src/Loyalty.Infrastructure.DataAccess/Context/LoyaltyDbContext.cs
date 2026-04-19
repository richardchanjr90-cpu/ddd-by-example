using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.EntityConfigurations;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess.Context
{
    public class LoyaltyDbContext : TransactionalContext, ILoyaltyDbContext
    {
        private readonly IMediator mediator;

        public LoyaltyDbContext(DbContextOptions<LoyaltyDbContext> options,
            IMediator mediator)
            : base(options)
        {
            this.mediator = mediator;
        }

        public DbSet<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<VenueWorker> VenueWorkers { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<UserCode> UserCodes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await mediator.DispatchDomainEventsAsync(this);
            var result = await SaveChangesAsync(cancellationToken);

            return true;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
            modelBuilder.Entity<UserCode>()
                .HasIndex(p => new { p.CodeValue })
                .IsUnique();

            modelBuilder.ApplyConfiguration(new VenueConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductGroupConfiguration());
            modelBuilder.ApplyConfiguration(new LoyaltyProductGroupConfiguration());
            modelBuilder.ApplyConfiguration(new LoyaltyProgramConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new VenueWorkerConfiguration());
        }
    }
}
