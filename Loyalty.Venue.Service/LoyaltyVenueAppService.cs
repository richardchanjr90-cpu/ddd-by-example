using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Loyalty.Core.ViewModels.Venue;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Venue.Service
{
    public class LoyaltyVenueAppService : BaseAppService
    {
        public LoyaltyVenueAppService(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<VenueViewModel> Get(int id)
        {
            var model = new Fixture()
                .Create<VenueViewModel>();

            return model;
        }

        public async Task<List<VenueViewModel>> Get()
        {
            return new List<VenueViewModel>
            {
                new Fixture()
                    .Create<VenueViewModel>()
            };
        }

        public async Task<List<VenueViewModel>> Get(Guid userGuid)
        {
            return new List<VenueViewModel>
            {
                new Fixture()
                    .Create<VenueViewModel>()
            };
        }

        public ICommandResult Create(VenueViewModel model)
        {
            var item = new Fixture()
                .Create<CommandResult>();

            return item;
        }

        public ICommandResult Update(VenueViewModel model)
        {
            var item = new Fixture()
                .Create<CommandResult>();

            return item;
        }

        public ICommandResult Delete(int id)
        {
            var item = new Fixture()
                .Create<CommandResult>();

            return item;
        }
    }
}
