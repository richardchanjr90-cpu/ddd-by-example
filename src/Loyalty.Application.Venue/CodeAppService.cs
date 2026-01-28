
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.Purchase;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Queries.Code;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Code;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class CodeAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public CodeAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<List<ActivePurchasesViewModel>> GetByCode(string code, long venueId)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var item = await Mediator.Send(new GetUserInfoByCodeQuery
            {
                Code = code
            });

            var result = await Mediator.Send(new GetClientActivePurchasesQuery
            {
                UserId = item.UserId,
                VenueId = venueId
            });

            return mapper.Map<List<ActivePurchasesViewModel>>(result.Result);
        }
    }
}