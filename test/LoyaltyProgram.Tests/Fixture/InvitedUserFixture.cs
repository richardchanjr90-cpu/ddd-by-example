using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoyaltyProgram.Tests.Setup.Auth;
using LoyaltyProgram.Tests.Setup.Data;

namespace LoyaltyProgram.Tests.Fixture
{
    public class InvitedUserFixture : IDisposable, IUserCreateFixture
    {
        private readonly TestFixture fixture;

        public HttpClient Client { get; } = new HttpClient();

        public InvitedUserFixture(TestFixture fixture, AuthUser user)
        {
            this.fixture = fixture;
            User = user;

            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.GetAuthTokenAsync().GetAwaiter().GetResult());

            SignupAsync(User).GetAwaiter().GetResult();
            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.GetAuthTokenAsync().GetAwaiter().GetResult());
        }

        private async Task SignupAsync(AuthUser user)
        {
            var signup = UserFactory.GetSignup();
            var content = ModelHelper.Convert(signup);
            var response = await fixture.ConfigureClient(Client).PostAsync("api/signup", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Could not sign up.");
            }
        }

        public async Task UpdateTokenAsync()
        {
            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await User.GetAuthTokenAsync());
        }

        public void Dispose()
        {
            DeleteWorkerAsync().GetAwaiter().GetResult();
            User.Dispose();
        }

        private async Task DeleteWorkerAsync()
        {
            await Client.DeleteAsync("api/workers/draft/" + User.Uid);
        }

        public AuthUser User { get; }
    }
}
