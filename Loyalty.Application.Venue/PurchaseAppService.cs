using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class PurchaseAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public PurchaseAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }


        public async Task<ICommandResult> Create(PurchaseViewModel model)
        {
            //todo: validation

            var command = mapper.Map<CreateVenueCommand>(model);
            return await Mediator.Send(command);
        }

    }
}
