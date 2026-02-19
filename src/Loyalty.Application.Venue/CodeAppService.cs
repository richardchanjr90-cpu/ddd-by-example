using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Queries.Code;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class CodeAppService : BaseAppService
    {
        private readonly IMapper mapper;
        private readonly ClientInfoAppService clientService;

        public CodeAppService(IMediator mediator, IMapper mapper, ClientInfoAppService clientService)
            : base(mediator)
        {
            this.mapper = mapper;
            this.clientService = clientService;
        }

        public async Task<ClientInfoPurchasesViewModel> GetByCode(string code, long venueId)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var clientPurchases = new ClientInfoPurchasesViewModel();

            var item = await Mediator.Send(new GetUserInfoByCodeQuery
            {
                Code = code
            });

            if (!String.IsNullOrEmpty(item.UserId))
            {
                var purchases = await Mediator.Send(new GetClientActivePurchasesQuery
                {
                    UserId = item.UserId,
                    VenueId = venueId
                });

                var clientInfo = await clientService.Get(item.UserId);
                var purchasesModels = mapper.Map<List<ActivePurchasesViewModel>>(purchases.Result);

                clientPurchases = new ClientInfoPurchasesViewModel()
                {
                    ActivePurchases = purchasesModels,
                    ClientInfo = clientInfo
                };
            }


            return clientPurchases;
        }
    }
}