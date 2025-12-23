using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.Extensions.UnitedStates;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyProgram.Tests.Setup.Data
{
    public class VenueFactory
    {
        public static VenueViewModel GetVenue()
        {
            var faker = new Faker<VenueViewModel>();

            faker.RuleFor(x => x.Description, f => f.Lorem.Sentence());
            faker.RuleFor(x => x.CategoryType, f => new Random().Next(1,3));
            faker.RuleFor(x => x.FullDescription, f => f.Lorem.Text());
            faker.RuleFor(x => x.Name, f => f.Company.CompanyName());
            faker.RuleFor(x => x.Type, f => (int)VenueType.Single);
            faker.RuleFor(x => x.WebSites, f=> new List<string>()
            {
                f.Person.Website,
                f.Person.Website,
                f.Person.Website,
            });

            faker.RuleFor(x => x.Phones, f => new List<string>()
            {
                "+" + f.Phone.PhoneNumber("37629#######"),
                "+" + f.Phone.PhoneNumber("37629#######"),
                "+" + f.Phone.PhoneNumber("37629#######")
            });

            faker.RuleFor(x => x.WorkingHours, new List<WorkingHoursViewModel>()
            {
                new WorkingHoursViewModel
                {
                    Day = "Monday",
                    To = new Random().Next(1,100),
                    From = new Random().Next(101,1000)
                }
            });
            faker.RuleFor(x => x.Location, f => new LocationViewModel
            {
                Address = f.Address.StreetAddress(),
                City = f.Address.City(),
                Longitude = (float)f.Address.Longitude(),
                Latitude = (float)f.Address.Latitude()
            });

            return faker.Generate();
        }
    }
}
