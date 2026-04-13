using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Aggregates.Workers;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess.Context.Interface
{
    public interface ILoyaltyDbContext : IDbContext
    {
        DbSet<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        DbSet<LoyaltyGroupRule> LoyaltyRules { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<ProductGroup> ProductGroups { get; set; }

        DbSet<Purchase> Purchases { get; set; }

        DbSet<Venue> Venues { get; set; }

        DbSet<VenueWorker> VenueWorkers { get; set; }

        DbSet<Worker> Workers { get; set; }

        DbSet<UserCode> UserCodes { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderItem> OrderItems { get; set; }
    }
}