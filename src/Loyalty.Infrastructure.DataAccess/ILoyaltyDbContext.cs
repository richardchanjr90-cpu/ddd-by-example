using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess
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