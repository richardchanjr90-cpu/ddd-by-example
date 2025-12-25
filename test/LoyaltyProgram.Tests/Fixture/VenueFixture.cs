using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data;

namespace LoyaltyProgram.Tests.Fixture
{
    public class VenueFixture : IDisposable
    {
        public VenueViewModel Venue { get; }

        public SignedUpUserFixture SignupFixture { get; }

        public VenueFixture(SignedUpUserFixture fixture)
        {
            SignupFixture = fixture;
            Venue = CreateVenueAsync().GetAwaiter().GetResult();
        }

        private async Task<VenueViewModel> CreateVenueAsync()
        {
            var venue = VenueFactory.GetVenue();
            var content = ModelHelper.Convert(venue);
            var response = await SignupFixture.Client.PostAsync("api/venues", content);
            var result = await response.DeserializeAsync<CommandResult>();
            await SignupFixture.UpdateTokenAsync();

            var getResponseMessage = await SignupFixture.Client.GetAsync("api/venues/" + result.Result);
            var getResult = await getResponseMessage.DeserializeAsync<VenueViewModel>();
            return getResult;
        }

        private async Task DeleteVenueAsync(long id)
        {
            var response = await SignupFixture.Client.DeleteAsync("api/venues/" + id);
            var result = await response.DeserializeAsync<CommandResult>();
        }

        public void Dispose()
        {
            if (Venue != null)
            {
                DeleteVenueAsync(Venue.Id).GetAwaiter().GetResult();
            }
        }
    }
}
