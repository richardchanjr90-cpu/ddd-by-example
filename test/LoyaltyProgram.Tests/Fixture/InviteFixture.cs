using System;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Contracts;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Tests.Fixture.Extensions;
using LoyaltyProgram.Tests.Setup.Auth;
using LoyaltyProgram.Tests.Setup.Data;

namespace LoyaltyProgram.Tests.Fixture
{
    public class InviteFixture : IDisposable
    {
        public InviteViewModel InvitedUser { get; }

        public IUserCreateFixture CreatorsUserFixture { get; }

        public InviteFixture(long venueId, VenueUserRole role, IUserCreateFixture creatorsFixture, InviteViewModel model = null)
        {
            CreatorsUserFixture = creatorsFixture;
            InvitedUser = CreateWorkerAsync(venueId, role, model).GetAwaiter().GetResult();
        }

        private async Task<InviteViewModel> CreateWorkerAsync(long venueId, VenueUserRole role, InviteViewModel model = null)
        {
            var worker = model ?? WorkerFactory.GetInvite(role);
            worker.VenueId = venueId;

            var content = ModelHelper.Convert(worker);
            var response = await CreatorsUserFixture.Client.PostAsync("api/workers/invited", content);
            var result = await response.DeserializeAsync<CommandResult>();
            //await CreatorsUserFixture.UpdateTokenAsync();

            var getResponseMessage = await CreatorsUserFixture.Client.GetAsync("api/workers/" + result.Result);
            var getResult = await getResponseMessage.DeserializeAsync<WorkerViewModel>();

            var invite = new InviteViewModel();
            invite.Id = getResult.Id;
            invite.Role = getResult.Role;
            invite.Name = getResult.Name;
            invite.Phone = getResult.Phone;
            invite.PositionName = getResult.PositionName;
            invite.VenueId = venueId;

            return invite;
        }

        private async Task DeleteWorkerAsync(long id)
        {
            await CreatorsUserFixture.Client.DeleteAsync("api/workers/" + id);
        }

        public void Dispose()
        {
            if (InvitedUser != null)
            {
                DeleteWorkerAsync(InvitedUser.Id).GetAwaiter().GetResult();
            }
        }
    }
}
