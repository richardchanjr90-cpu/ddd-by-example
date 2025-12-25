using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.Extensions.UnitedStates;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyProgram.Tests.Setup.Data
{
    public class UserFactory
    {
        public static SignupViewModel GetSignup()
        {
            var faker = new Faker<SignupViewModel>();
            
            faker.RuleFor(x => x.Name, f => f.Name.FirstName());
            faker.RuleFor(x => x.Surname, f => f.Name.LastName());
            faker.RuleFor(x => x.City, f => f.Address.City());
            faker.RuleFor(x => x.Email, f => f.Internet.Email());

            return faker.Generate();
        }
    }
}
