using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.Orders;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using Loyalty.Domain.Handlers.Queries.Queries.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class OrderAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public OrderAppService(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<GetOrderByVenueIdQueryResult> Get(long id)
        {
            var result = await Mediator.Send(new GetOrderByIdQuery()
            {
                Id = id
            });

            return result;
        }

        public async Task<List<GetOrderByVenueIdQueryResult>> GetAll(long venueId)
        {
            var result = await Mediator.Send(new GetAllOrdersByVenueIdQuery
            {
                VenueId = venueId
            });

            return result.Orders;
        }

        public async Task<ICommandResult> PatchStatus(PatchOrderStatusViewModel model)
        {
            return await Mediator.Send(new PatchOrderCommand()
            {
                Status = (OrderStatus)model.Status,
                VenueComment = model.VenueComment,
                OrderId = model.OrderId,
            });
        }
    }
}
