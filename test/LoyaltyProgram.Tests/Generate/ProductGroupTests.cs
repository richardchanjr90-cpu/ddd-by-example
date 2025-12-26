using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Auth;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Generate
{
    [Collection(nameof(FunctionTestCollection))]
    public class GenerateTests : IClassFixture<SignedUpUserFixture>
    {
        private readonly TestFixture fixture;

        private readonly SignedUpUserFixture signedUpUserFixture;

        public GenerateTests(TestFixture fixture, SignedUpUserFixture signedUpUserFixture)
        {
            this.fixture = fixture;
            this.signedUpUserFixture = signedUpUserFixture;
        }

        public class ValidDateTestData : TheoryData<DateTime, DateTime>
        {
            public ValidDateTestData()
            {
                Add(DateTime.Now.AddDays(2), DateTime.Now.AddYears(1));
            }
        }

        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldGetProductGroups(DateTime start, DateTime finish)
        {
            var venue1 = new VenueFixture(signedUpUserFixture);
            var venue2 = new VenueFixture(signedUpUserFixture);
            var venue3 = new VenueFixture(signedUpUserFixture);

            var invitedUser1 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Director, signedUpUserFixture);
            var createdUser1 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser11 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Manager, signedUpUserFixture);
            var createdUser11 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser12 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Manager, signedUpUserFixture);
            var createdUser12 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser13 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser13 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser14 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser14 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser2 = new InviteFixture(venue2.Venue.Id, VenueUserRole.Director, signedUpUserFixture);
            var createdUser2 = new InvitedUserFixture(fixture, new AuthUser(invitedUser2.InvitedUser.Phone), signedUpUserFixture);

            var program = new LoyaltyProgramFixture(start, finish, venue1.Venue.Id, signedUpUserFixture);


            var group = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var lpgGet =
                new LoyaltyProductGroupFixture(group.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture);

            var product11 = ProductFactory.Get();
            var product12 = ProductFactory.Get();
            var product13 = ProductFactory.Get();
            var product14 = ProductFactory.Get();

            var product15 = ProductFactory.Get();
            var product16 = ProductFactory.Get();
            var product17 = ProductFactory.Get();

            var productContent1 = ModelHelper.Convert(product12);
            var productContent2 = ModelHelper.Convert(product11);
            var productContent3 = ModelHelper.Convert(product13);
            var productContent4 = ModelHelper.Convert(product14);
            var productContent5 = ModelHelper.Convert(product15);
            var productContent6 = ModelHelper.Convert(product16);
            var productContent7 = ModelHelper.Convert(product17);

            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent1);
            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent2);
            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent3);
            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent4);
            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent5);
            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent6);
            await signedUpUserFixture.Client.PostAsync($"api/productGroups/{group.ProductGroup.Id}/products", productContent7);

            new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);

            new ProductGroupFixture(venue2.Venue.Id, signedUpUserFixture);
            new ProductGroupFixture(venue2.Venue.Id, signedUpUserFixture);
            new ProductGroupFixture(venue2.Venue.Id, signedUpUserFixture);
        }
    }
}