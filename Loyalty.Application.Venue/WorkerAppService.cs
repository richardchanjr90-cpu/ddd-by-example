using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class WorkerAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public WorkerAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<WorkerViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetWorkerByIdQuery
            {
                Id = id
            });

            return mapper.Map<WorkerViewModel>(result);
        }

        public async Task<List<WorkerViewModel>> GetAll(long venueId)
        {
            var result = await Mediator.Send(new GetWorkersQuery
            {
                VenueId = venueId
            });

            return mapper.Map<List<WorkerViewModel>>(result.Result);
        }

        public async Task<List<WorkerViewModel>> Get()
        {
            var result = await Mediator.Send(new GetWorkersByUserIdQuery()
            {
                UserId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40")
            });

            return mapper.Map<List<WorkerViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(WorkerViewModel model)
        {
            new WorkerCreateValidator().ValidateAndThrow(model);
            var command = mapper.Map<CreateWorkerCommand>(model);

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(WorkerViewModel model)
        {
            new WorkerUpdateValidator().ValidateAndThrow(model);
            var command = mapper.Map<UpdateWorkerCommand>(model);

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id)
        {
            var command = new ArchiveWorkerCommand
            {
                Id = id,
                UserId = Guid.Parse("0abe336d-021c-40b5-ba95-909daeb7ca40")
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}
