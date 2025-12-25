using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.TestHelper;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
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

        public async Task<List<ActivePurchasesViewModel>> GetActivePurchases(string userId, long venueId)
        {
            var result = await Mediator.Send(new GetClientActivePurchasesQuery
            {
                UserId = userId,
                VenueId = venueId
            });

            return mapper.Map<List<ActivePurchasesViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Purchase(PurchaseViewModel model, long venueId, string userId)
        {
            var testResult = new PurchaseValidator()
                .TestValidate(model);

            var result = await Mediator.Send(new CreatePurchaseCommand
            {
                WorkerId = userId,
                UserId = model.UserId,
                ProductId = model.ProductId,
                VenueId = venueId,
                Value = model.Value,
                LoyaltyProductGroupId = model.LoyaltyProductGroupId
            });

            return result.CommandResult;
        }

        public async Task<ICommandResult> PurchaseAndCreate(PurchaseViewModel model, long venueId, string userId)
        {
            new PurchaseValidator()
                .ValidateAndThrow(model);

            var result = await Mediator.Send(new CreateAndBurnCommand
            {
            });

            return result.CommandResult;
        }

        public async Task<object> Burn(PurchaseViewModel model, long venueId, string userId)
        {
            new PurchaseValidator().ValidateAndThrow(model);

            var result = await Mediator.Send(new BurnPurchaseCommand
            {
                WorkerId = userId,
                UserId = model.UserId,
                VenueId = venueId,
                Amount = model.Value,
                LoyaltyProductGroupId = model.LoyaltyProductGroupId
            });

            return result;
        }
    }
}