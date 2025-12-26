using System;
using Bogus;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Shared.Contracts.Enums;

namespace LoyaltyProgram.Tests.Setup.Data
{
    public class WorkerFactory
    {
        public static WorkerViewModel GetWorker(VenueUserRole role)
        {
            var stringRole = "Бариста";

            if (role == VenueUserRole.Director)
            {
                stringRole = "Директор";
            }
            if (role == VenueUserRole.Owner)
            {
                stringRole = "Владелец";
            }
            if (role == VenueUserRole.Manager)
            {
                stringRole = "Менеджер";
            }

            var faker = new Faker<WorkerViewModel>();
            faker.RuleFor(x => x.Name, f => f.Person.FirstName);
            faker.RuleFor(x => x.LastName, f => f.Person.LastName);
            faker.RuleFor(x => x.Email, f => f.Person.Email);
            faker.RuleFor(x => x.PositionName, f => stringRole);
            faker.RuleFor(x => x.Phone, f => f.Phone.PhoneNumber("+37629#######"));
            faker.RuleFor(x => x.Role, f => (int)role);

            return faker.Generate();
        }


        public static InviteViewModel GetInvite(VenueUserRole role)
        {
            var stringRole = "Бариста";

            if (role == VenueUserRole.Director)
            {
                stringRole = "Директор";
            }
            if (role == VenueUserRole.Owner)
            {
                stringRole = "Владелец";
            }
            if (role == VenueUserRole.Manager)
            {
                stringRole = "Менеджер";
            }

            var faker = new Faker<InviteViewModel>();
            faker.RuleFor(x => x.Name, f => f.Person.FirstName);
            faker.RuleFor(x => x.PositionName, f => stringRole);
            faker.RuleFor(x => x.Phone, f => f.Phone.PhoneNumber("+37629#######"));
            faker.RuleFor(x => x.Role, f => (int)role);

            return faker.Generate();
        }
    }
}
