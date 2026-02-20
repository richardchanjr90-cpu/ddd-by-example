using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.TestHelper;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using MediatR;
using MediatR.Extensions.UnitOfWork;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class PurchaseAppService : BaseAppService
    {
        private readonly IMapper mapper;
        private readonly ClientInfoAppService clientService;

        public PurchaseAppService(IMediator mediator, IMapper mapper, ClientInfoAppService clientService)
            : base(mediator)
        {
            this.mapper = mapper;
            this.clientService = clientService;
        }

        public async Task<ClientInfoPurchasesViewModel> GetActivePurchases(string userId, long venueId)
        {
            var result = await Mediator.Send(new GetClientActivePurchasesQuery
            {
                UserId = userId,
                VenueId = venueId
            });

            var purchases = mapper.Map<List<ActivePurchasesViewModel>>(result.Result);

            var clientInfo = await clientService.Get(userId);
            var purchasesModels = mapper.Map<List<ActivePurchasesViewModel>>(purchases);

            return new ClientInfoPurchasesViewModel()
            {
                ActivePurchases = purchasesModels,
                ClientInfo = clientInfo
            };
        }

        public async Task<ICommandResult> Purchase(PurchaseViewModel model, long venueId, string userId)
        {
            new PurchaseValidator()
                .ValidateAndThrow(model);

            var result = await Mediator.SendThenPublish(new CreatePurchaseCommand
            {
                WorkerId = userId,
                UserId = model.UserId,
                ProductId = model.ProductId,
                VenueId = venueId,
                Value = model.Value,
                LoyaltyProductGroupId = model.LoyaltyProductGroupId
            });

            return result;
        }

        public async Task<ICommandResult> CreateAndBurn(PurchaseAndBurnViewModel model, long venueId, string userId)
        {
            new PurchaseAndBurnValidator()
                .ValidateAndThrow(model);

            var command1 = new CreatePurchaseCommand
            {
                WorkerId = userId,
                UserId = model.UserId,
                ProductId = model.ProductId,
                VenueId = venueId,
                Value = model.Purchase,
                LoyaltyProductGroupId = model.LoyaltyProductGroupId
            };

            var command2 = new BurnPurchaseCommand
            {
                WorkerId = userId,
                UserId = model.UserId,
                VenueId = venueId,
                Amount = model.Burn,
                LoyaltyProductGroupId = model.LoyaltyProductGroupId
            };

            var result = await Mediator.RunAllScopedThenPublish(command1, command2);
            return result;
        }

        public async Task<object> Burn(PurchaseViewModel model, long venueId, string userId)
        {
            new PurchaseValidator().ValidateAndThrow(model);

            var result = await Mediator.SendThenPublish(new BurnPurchaseCommand
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