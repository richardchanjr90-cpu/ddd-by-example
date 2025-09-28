using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
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

        public async Task<List<ProductGroupViewModel>> GetAll(long venueId)
        {
            var result = await Mediator.Send(new GetProductGroupsQuery
            {
                VenueId = venueId
            });

            return mapper.Map<List<ProductGroupViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(ProductGroupViewModel model, long venueId)
        {
            new ProductGroupValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateProductGroupCommand>(model);
            command.VenueId = venueId;

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(ProductGroupViewModel model, long venueId)
        {
            new ProductGroupValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateProductGroupCommand>(model);
            command.VenueId = venueId;
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
