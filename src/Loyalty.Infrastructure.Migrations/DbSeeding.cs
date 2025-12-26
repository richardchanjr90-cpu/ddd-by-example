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
                        //CreatedBy = "0abe336d-021c-40b5-ba95-909daeb7ca40",
                        Modified = DateTime.Now,
                        LogoUrl = "http://clipart-library.com/images/8cEb974Xi.jpg",
                        Description = "Venue without details and programs.",
                        IsArchived = false,
                        IsPublished = false,
                        Name = "Venue1",
                        Type = VenueType.Single,
                        //OwnerId = "0abe336d-021c-40b5-ba95-909daeb7ca40"
                    });

                context.SaveChanges();
            }
        }
    }
}