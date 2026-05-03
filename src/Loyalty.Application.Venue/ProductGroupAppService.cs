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
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class ProductGroupAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public ProductGroupAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<GetProductGroupByIdQueryResult> Get(long id)
        {
            var result = await Mediator.Send(new GetProductGroupByIdQuery
            {
                Id = id
            });

            return result;
        }

        public async Task<List<GetProductGroupByIdQueryResult>> Get(string userId)
        {
            var result = await Mediator.Send(new GetProductGroupsByUserIdQuery
            {
                UserId = userId
            });

            return result.Result;
        }

        public async Task<ICommandResult> Create(ProductGroupViewModel model)
        {
            new ProductGroupValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateProductGroupCommand>(model);

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(ProductGroupViewModel model)
        {
            new ProductGroupValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateProductGroupCommand>(model);
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Patch(ProductGroupPatchViewModel model)
        {
            var command = mapper.Map<PatchProductGroupCommand>(model);
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id, string userId)
        {
            var command = new ArchiveProductGroupCommand
            {
                Id = id,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}