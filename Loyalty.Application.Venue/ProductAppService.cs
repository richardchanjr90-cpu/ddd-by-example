using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Domain.Handlers.Queries.Queries.Product;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class ProductAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public ProductAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<ProductViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetProductByIdQuery
            {
                Id = id
            });

            return mapper.Map<ProductViewModel>(result);
        }

        public async Task<List<ProductViewModel>> GetAll(long groupId)
        {
            var result = await Mediator.Send(new GetProductsQuery
            {
                ProductGroupId = groupId
            });

            return mapper.Map<List<ProductViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(ProductViewModel model, long groupId)
        {
            new ProductValidator().ValidateAndThrow(model);
            var command = mapper.Map<CreateProductCommand>(model);
            command.ProductGroupId = groupId;
            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(ProductViewModel model, long groupId)
        {
            new ProductValidator().ValidateAndThrow(model);
            var command = mapper.Map<UpdateProductCommand>(model);
            command.ProductGroupId = groupId;
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id)
        {
            var command = new ArchiveProductCommand
            {
                Id = id,
                UserId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40")
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}