using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Fixture
{
    public class LoyaltyProgramFixture : IDisposable
    {
        private readonly DateTime start;
        private readonly DateTime finish;
        private readonly long venueId;

        public LoyaltyProgramViewModel LoyaltyProgram { get; }

        public SignedUpUserFixture SignupFixture { get; }

        public LoyaltyProgramFixture(DateTime start, DateTime finish, long venueId, SignedUpUserFixture fixture)
        {
            this.start = start;
            this.finish = finish;
            this.venueId = venueId;
            SignupFixture = fixture;
            LoyaltyProgram = CreateProgramAsync().GetAwaiter().GetResult();
        }

        public async Task<LoyaltyProgramFixture> Publish()
        {
            return this;
        }

        private async Task<LoyaltyProgramViewModel> CreateProgramAsync()
        {
            var data = LoyaltyProgramFactory.Get(start, finish);

            var dataContent = ModelHelper.Convert(data);
            var response = await SignupFixture.Client.PostAsync($"api/venues/{venueId}/programs", dataContent);
            var result = await response.DeserializeAsync<CommandResult>();

            var response3 = await SignupFixture.Client.GetAsync($"api/venues/{venueId}/programs/{result.Result}/");
            var result3 = await response3.DeserializeAsync<LoyaltyProgramViewModel>();
            return result3;
        }

        private async Task DeleteAsync(long id)
        {
            var response = await SignupFixture.Client.DeleteAsync($"api/venues/{venueId}/programs/{id}");
            var result = await response.DeserializeAsync<CommandResult>();
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            if (LoyaltyProgram != null)
            {
                DeleteAsync(LoyaltyProgram.Id).GetAwaiter().GetResult();
            }
        }
    }
}
