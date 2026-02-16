using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Tests.Fixture;
using LoyaltyProgram.Tests.Setup.Auth;
using LoyaltyProgram.Tests.Setup.Data;
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

        //[Theory(Skip = "This is not a test")]
        [Theory]
        [ClassData(typeof(ValidDateTestData))]
        public async Task ShouldCreateFullyOperableVenue(DateTime start, DateTime finish)
        {
            var venue1 = new VenueFixture(signedUpUserFixture);
            var venue2 = new VenueFixture(signedUpUserFixture);
            var venue3 = new VenueFixture(signedUpUserFixture);
            var venue4 = new VenueFixture(signedUpUserFixture);
            var venue5 = new VenueFixture(signedUpUserFixture);
            var venue6 = new VenueFixture(signedUpUserFixture);
            var venue7 = new VenueFixture(signedUpUserFixture);
            var venue8 = new VenueFixture(signedUpUserFixture);
            var venue9 = new VenueFixture(signedUpUserFixture);
            var venue10 = new VenueFixture(signedUpUserFixture);

            var venues = new List<VenueFixture>();
            venues.Add(venue1);
            venues.Add(venue2);
            venues.Add(venue3);
            venues.Add(venue4);
            venues.Add(venue5);
            venues.Add(venue6);
            venues.Add(venue7);
            venues.Add(venue8);
            venues.Add(venue9);
            venues.Add(venue10);

            foreach (var venue in venues)
            {
                for (int i = 0; i < 10; i++)
                {
                    var imageContent = ImageFactory.GetImageContent();
                    await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/details/images", imageContent);
                }
            }

            foreach (var venue in venues)
            {
                var imageContent = ImageFactory.GetImageContent();
                var response = await signedUpUserFixture.Client.PostAsync($"api/venues/{venue.Venue.Id}/logo", imageContent);
            }

            var invitedUser1 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Director, signedUpUserFixture);
            var createdUser1 = new InvitedUserFixture(fixture, new AuthUser(invitedUser1.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser11 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Manager, signedUpUserFixture);
            var createdUser11 = new InvitedUserFixture(fixture, new AuthUser(invitedUser11.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser12 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Manager, signedUpUserFixture);
            var createdUser12 = new InvitedUserFixture(fixture, new AuthUser(invitedUser12.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser13 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser13 = new InvitedUserFixture(fixture, new AuthUser(invitedUser13.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser14 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser14 = new InvitedUserFixture(fixture, new AuthUser(invitedUser14.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser2 = new InviteFixture(venue2.Venue.Id, VenueUserRole.Director, signedUpUserFixture);
            var createdUser2 = new InvitedUserFixture(fixture, new AuthUser(invitedUser2.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser15 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser15 = new InvitedUserFixture(fixture, new AuthUser(invitedUser15.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser16 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser16 = new InvitedUserFixture(fixture, new AuthUser(invitedUser16.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser17 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser17 = new InvitedUserFixture(fixture, new AuthUser(invitedUser17.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser18 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser18 = new InvitedUserFixture(fixture, new AuthUser(invitedUser18.InvitedUser.Phone), signedUpUserFixture);

            var invitedUser19 = new InviteFixture(venue1.Venue.Id, VenueUserRole.Worker, signedUpUserFixture);
            var createdUser19 = new InvitedUserFixture(fixture, new AuthUser(invitedUser19.InvitedUser.Phone), signedUpUserFixture);

            var program = new LoyaltyProgramFixture(start, finish, venue1.Venue.Id, signedUpUserFixture);
            var program1 = new LoyaltyProgramFixture(start, finish, venue1.Venue.Id, signedUpUserFixture);
            var program2 = new LoyaltyProgramFixture(start, finish, venue1.Venue.Id, signedUpUserFixture);

            var group = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group1 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group2 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group3 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group4 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group5 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group6 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group7 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group8 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);
            var group9 = new ProductGroupFixture(venue1.Venue.Id, signedUpUserFixture);


            var groups = new List<ProductGroupFixture>();
            groups.Add(group);
            groups.Add(group1);
            groups.Add(group2);
            groups.Add(group3);
            groups.Add(group4);
            groups.Add(group5);
            groups.Add(group6);
            groups.Add(group7);
            groups.Add(group8);
            groups.Add(group9);

            foreach (var gr in groups)
            {
                var lpgGet =
                    new LoyaltyProductGroupFixture(gr.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture);

                for (int i = 0; i < 20; i++)
                {
                    var product = ProductFactory.Get();
                    var productContent = ModelHelper.Convert(product);
                    await signedUpUserFixture.Client.PostAsync($"api/productGroups/{gr.ProductGroup.Id}/products", productContent);
                }
            }

            var lpgGet1 =
                new LoyaltyProductGroupFixture(group1.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture);

            var lpgGet2 =
                new LoyaltyProductGroupFixture(group2.ProductGroup.Id, program.LoyaltyProgram.Id, signedUpUserFixture);
        }
    }
}