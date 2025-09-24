using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class PurchaseAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public PurchaseAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<List<ClientActivePurchasesViewModel>> GetActivePurchases(Guid userId, long venueId)
        {
            //todo: convert code to guid
            //todo: validate worker belongs to venue

            var result = await Mediator.Send(new GetClientActivePurchasesQuery
            {
                WorkerId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40"),
                UserId = userId,
                VenueId = venueId,    
            });

            return mapper.Map<List<ClientActivePurchasesViewModel>>(result.Result);
        }

        public async Task<List<ClientActivePurchasesViewModel>> Purchase(PurchaseViewModel model, Guid userId, long venueId)
        {
            //todo: validation
            //todo: validate worker belongs to venue

            var result = await Mediator.Send(new CreatePurchaseCommand
            {
                WorkerId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40"),
                UserId = userId,
                VenueId = venueId,
                Value = model.Value,
                LoyaltyGroupId = model.LoyaltyGroupId
            });

            return mapper.Map<List<ClientActivePurchasesViewModel>>(result.Result);
        }

    }
}
