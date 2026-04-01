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
using MediatR.Extensions.UnitOfWork.Interface;

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

        public async Task<GetProductViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetProductByIdQuery
            {
                Id = id
            });

            return mapper.Map<GetProductViewModel>(result);
        }

        public async Task<List<GetProductViewModel>> GetAll(long groupId)
        {
            var result = await Mediator.Send(new GetProductsQuery
            {
                ProductGroupId = groupId
            });

            return mapper.Map<List<GetProductViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(CreateProductViewModel model, long groupId)
        {
            new CreateProductValidator().ValidateAndThrow(model);
            var command = mapper.Map<CreateProductCommand>(model);
            command.ProductGroupId = groupId;

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(UpdateProductViewModel model, long groupId)
        {
            new UpdateProductValidator().ValidateAndThrow(model);
            var command = mapper.Map<UpdateProductCommand>(model);
            command.ProductGroupId = groupId;

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Patch(PatchProductViewModel model, long groupId)
        {
            var command = mapper.Map<PatchProductCommand>(model);
            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Archive(long id, string userId)
        {
            var command = new ArchiveProductCommand
            {
                Id = id,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}