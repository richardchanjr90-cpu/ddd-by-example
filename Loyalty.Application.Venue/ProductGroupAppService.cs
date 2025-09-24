using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class ProductGroupAppService: BaseAppService
    {
        private readonly IMapper mapper;

        public ProductGroupAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<ProductGroupViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetProductGroupByIdQuery
            {
                Id = id
            });

            return mapper.Map<ProductGroupViewModel>(result);
        }

        public async Task<List<ProductGroupViewModel>> Get()
        {
            var result = await Mediator.Send(new GetProductGroupsQuery());
            return mapper.Map<List<ProductGroupViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(ProductGroupViewModel model)
        {
            //new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateProductGroupCommand>(model);
            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(ProductGroupViewModel model)
        {
            //new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateProductGroupCommand>(model);
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id)
        {
            var command = new ArchiveProductGroupCommand
            {
                Id = id,
                UserId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40")
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}
