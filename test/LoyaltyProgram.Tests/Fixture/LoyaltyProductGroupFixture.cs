using System;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Domain.Contracts;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Data.Loyalty;
using Xunit;

namespace LoyaltyProgram.Tests.Fixture
{
    public class LoyaltyProductGroupFixture : IDisposable
    {
        private readonly long groupId;
        private readonly long programId;

        public LoyaltyProductGroupGetViewModel LoyaltyProductGroup { get; }

        public SignedUpUserFixture SignupFixture { get; }

        public LoyaltyProductGroupFixture(long groupId, long programId, SignedUpUserFixture fixture)
        {
            this.groupId = groupId;
            this.programId = programId;
            SignupFixture = fixture;
            LoyaltyProductGroup = CreateGroupAsync().GetAwaiter().GetResult();
        }

        private async Task<LoyaltyProductGroupGetViewModel> CreateGroupAsync()
        {
            var group1 = LoyaltyGroupFactory.Get(groupId, programId);

            var groupContent = ModelHelper.Convert(group1);
            var response = await SignupFixture.Client.PostAsync($"api/programs/{programId}/loyaltygroups", groupContent);
            var result = await response.DeserializeAsync<CommandResult>();

            var response3 = await SignupFixture.Client.GetAsync($"api/programs/{programId}/loyaltygroups/{result.Result}");
            var result3 = await response3.DeserializeAsync<LoyaltyProductGroupGetViewModel>();
            return result3;
        }

        private async Task Delete(long id)
        {
            var response = await SignupFixture.Client.DeleteAsync($"api/programs/{programId}/loyaltygroups/{id}/");
            var result = await response.DeserializeAsync<CommandResult>();
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            if (LoyaltyProductGroup != null)
            {
                Delete(LoyaltyProductGroup.Id).GetAwaiter().GetResult();
            }
        }
    }
}
