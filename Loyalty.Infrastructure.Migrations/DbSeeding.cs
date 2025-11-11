using System;
using System.Linq;
using Loyalty.Core.Entities;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Infrastructure.Migrations
{
    public static class DbSeeding
    {
        private static void Main(params string[] args)
        {
            var defaultPath =
                $"{Environment.CurrentDirectory}\\..\\..\\..\\..\\..\\..\\LoyaltyProgram";

            using (var context = new LoyaltyApiContextFactory().CreateDbContext(defaultPath))
            {
                context.Database.EnsureCreated();

                var venue = context.Venues.FirstOrDefault(v => v.Name == "Venue1");
                if (venue == null)
                    context.Venues.Add(new Venue
                    {
                        CategoryType = VenueCategoryType.CoffeeShop,
                        Created = DateTime.Now,
                        CreatedBy = Guid.Parse("0ABE336D-021C-40B5-BA95-909DAEB7CA40"),
                        Modified = DateTime.Now,
                        LogoUrl = "http://clipart-library.com/images/8cEb974Xi.jpg",
                        Description = "Venue without details and programs.",
                        IsArchived = false,
                        IsPublished = false,
                        Name = "Venue1",
                        Type = VenueType.Single,
                        OwnerId = Guid.Parse("0ABE336D-021C-40B5-BA95-909DAEB7CA40")
                    });

                context.SaveChanges();
            }
        }
    }
}